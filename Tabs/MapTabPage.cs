using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IdleLandsGUI.Model;
using IdleLandsGUI.Model.Map;
using SharpGL;
using System.Drawing.Imaging;

namespace IdleLandsGUI.Tabs
{
    public partial class MapTabPage : UserControl
    {
        private IdleLandsComms _comms { get; set; }
        private string _mapName { get; set; }
        private List<Tile> _tiles { get; set; }
        private List<uint> _textures { get; set; }
        private Bitmap _textureImage;
        private Point _playerPos { get; set; }
        private int _playerGid { get; set; }
        private Tile? _playerTile { get; set; }

        public MapTabPage(PlayerInfo playerInfo, IdleLandsComms comms)
        {
            InitializeComponent();
            Anchor = (AnchorStyles.Bottom | AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            _comms = comms;
            _comms.AddPlayerUpdateDelegate(UpdatePlayerInfo);
            _comms.AddMapUpdateDelegate(UpdateMapInfo);

            _tiles = new List<Tile>();
            _textures = new List<uint>();

            UpdatePlayerInfo(playerInfo);
        }

        private void UpdatePlayerInfo(PlayerInfo info)
        {
            _playerGid = 11;
            if(info.gender.ToLower() == "male")
                _playerGid = 12;
            if(info.gender.ToLower() == "female")
                _playerGid = 13;

            _playerPos = new Point(info.x, info.y);

            if(_playerTile.HasValue)
            {
                _playerTile = new Tile(_playerTile.Value, info.x * 16.0f, info.y * 16.0f);
            }

            if (_mapName != info.map && !string.IsNullOrEmpty(info.map))
            {
                _mapName = info.map;
                _comms.SendRequestMap(info.map, null, (string msg, int code) =>
                {
                    MessageBox.Show(code + ": " + msg);
                    return true;
                });
            }
        }

        private void UpdateMapInfo(MapInfo info)
        {
            //setup
            _tiles.Clear();
            int curWidth = 0;
            int curHeight = 0;
            //int imgWidth = info.map.tilesets[0].imagewidth;
            int imgWidth = _textureImage.Width;
            //int imgHeight = info.map.tilesets[0].imageheight;
            int imgHeight = _textureImage.Height;
            int firstGid = info.map.tilesets[0].firstgid;

            //Generate textures
            SharpGL.OpenGL gl = this.openGLControl1.OpenGL;
            gl.DeleteTextures(_textures.Count(), _textures.ToArray());
            _textures.Clear();

            uint[] temp = new uint[1];
            var bitmapData = _textureImage.LockBits(new Rectangle(0, 0, _textureImage.Width, _textureImage.Height),
                ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
            gl.GenTextures(1, temp);
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, temp[0]);

            gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_WRAP_S, OpenGL.GL_CLAMP_TO_EDGE);
            gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_WRAP_R, OpenGL.GL_CLAMP_TO_EDGE);
            gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_WRAP_T, OpenGL.GL_CLAMP_TO_EDGE);

            gl.TexImage2D(OpenGL.GL_TEXTURE_2D, 0, OpenGL.GL_RGBA, _textureImage.Width, _textureImage.Height, 0, OpenGL.GL_BGRA, OpenGL.GL_UNSIGNED_BYTE,
                bitmapData.Scan0);
            gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MIN_FILTER, OpenGL.GL_NEAREST);
            gl.TexParameter(OpenGL.GL_TEXTURE_2D, OpenGL.GL_TEXTURE_MAG_FILTER, OpenGL.GL_NEAREST);
            _textureImage.UnlockBits(bitmapData);
            _textures.Add(temp[0]);

            //Generate tiles
            double widthPercent = (double)info.tileWidth / (double)imgWidth;
            double heightPercent = (double)info.tileHeight / (double)imgHeight;
            foreach (var layer in info.map.layers)
            {
                if (!layer.visible)
                    continue;

                curWidth = 0;
                curHeight = 0;

                if (layer.data != null)
                {
                    foreach (var tiledata in layer.data)
                    {
                        if (tiledata >= firstGid)
                        {

                            _tiles.Add(new Tile
                            {
                                x = curWidth,
                                y = curHeight,
                                w = info.tileWidth,
                                h = info.tileHeight,
                                tx = ((tiledata - firstGid) % (imgWidth / info.tileWidth)) * widthPercent,
                                ty = ((tiledata - firstGid) / (imgWidth / info.tileWidth)) * heightPercent,
                                tw = widthPercent,
                                th = heightPercent,
                                texture = 0
                            });
                        }

                        curWidth += info.tileWidth;
                        if (curWidth >= layer.width * info.tileWidth)
                        {
                            curWidth = 0;
                            curHeight += info.tileHeight;
                        }
                    }
                }

                if (layer.objects != null)
                {
                    foreach (var obj in layer.objects)
                    {
                        _tiles.Add(new Tile
                        {
                            x = obj.x,
                            y = obj.y - info.tileHeight,
                            w = info.tileWidth,
                            h = info.tileHeight,
                            tx = ((obj.gid - firstGid) % (imgWidth / info.tileWidth)) * widthPercent,
                            ty = ((obj.gid - firstGid) / (imgWidth / info.tileWidth)) * heightPercent,
                            tw = widthPercent,
                            th = heightPercent,
                            texture = 0
                        });
                    }
                }
            }

            _playerTile = new Tile
            {
                x = _playerPos.X * info.tileWidth,
                y = _playerPos.Y * info.tileHeight,
                w = 16,
                h = 16,
                tx = (_playerGid % (imgWidth / info.tileWidth)) * widthPercent,
                ty = (_playerGid / (imgWidth / info.tileWidth)) * heightPercent,
                tw = widthPercent,
                th = heightPercent,
                texture = 0
            };
        }

        private void openGLControl1_OpenGLDraw(object sender, RenderEventArgs e)
        {
            if (!_textures.Any())
                return;

            // Get the OpenGL object, for quick access.
            SharpGL.OpenGL gl = this.openGLControl1.OpenGL;
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT);
            foreach (Tile tile in _tiles)
            {
                gl.BindTexture(OpenGL.GL_TEXTURE_2D, _textures[tile.texture]);
                gl.Begin(OpenGL.GL_QUADS);
                gl.TexCoord(tile.tx, tile.ty); gl.Vertex(tile.x, tile.y, 0.0f); // Bottom Left Of The Texture and Quad
                gl.TexCoord(tile.tx + tile.tw, tile.ty); gl.Vertex(tile.x + tile.w, tile.y, 0.0f); // Bottom Right Of The Texture and Quad
                gl.TexCoord(tile.tx + tile.tw, tile.ty + tile.th); gl.Vertex(tile.x + tile.w, tile.y + tile.h, 0.0f); // Top Right Of The Texture and Quad
                gl.TexCoord(tile.tx, tile.ty + tile.th); gl.Vertex(tile.x, tile.y + tile.h, 0.0f); // Top Left Of The Texture and Quad
                gl.End();
            }

            Tile playerTile = _playerTile.Value;
            gl.BindTexture(OpenGL.GL_TEXTURE_2D, _textures[playerTile.texture]);
            gl.Begin(OpenGL.GL_QUADS);
            gl.TexCoord(playerTile.tx, playerTile.ty); gl.Vertex(playerTile.x, playerTile.y, 0.0f); // Bottom Left Of The Texture and Quad
            gl.TexCoord(playerTile.tx + playerTile.tw, playerTile.ty); gl.Vertex(playerTile.x + playerTile.w, playerTile.y, 0.0f); // Bottom Right Of The Texture and Quad
            gl.TexCoord(playerTile.tx + playerTile.tw, playerTile.ty + playerTile.th); gl.Vertex(playerTile.x + playerTile.w, playerTile.y + playerTile.h, 0.0f); // Top Right Of The Texture and Quad
            gl.TexCoord(playerTile.tx, playerTile.ty + playerTile.th); gl.Vertex(playerTile.x, playerTile.y + playerTile.h, 0.0f); // Top Left Of The Texture and Quad
            gl.End();
        }

        private void openGLControl1_OpenGLResized(object sender, EventArgs e)
        {
            SharpGL.OpenGL gl = this.openGLControl1.OpenGL;
            gl.Viewport(0, 0, this.Width, this.Height);
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();
            gl.Ortho(0, this.Width, this.Height, 0, -1, 1);
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.Hint(OpenGL.GL_PERSPECTIVE_CORRECTION_HINT, OpenGL.GL_NICEST);
        }

        private void openGLControl1_OpenGLInitialized(object sender, EventArgs e)
        {
            // Get the OpenGL object, for quick access.
            SharpGL.OpenGL gl = this.openGLControl1.OpenGL;
            // We need to load the texture from file.
            _textureImage = new Bitmap("Assets/tiles.png");
            // A bit of extra initialisation here, we have to enable textures.
            gl.Enable(OpenGL.GL_TEXTURE_2D);
            gl.Disable(OpenGL.GL_DEPTH_TEST);
            gl.BlendFunc(OpenGL.GL_SRC_ALPHA, OpenGL.GL_ONE_MINUS_SRC_ALPHA);
            gl.Enable(OpenGL.GL_BLEND);
            gl.ClearColor(0.0f, 0.0f, 0.0f, 0.0f);
            
            gl.Viewport(0, 0, this.Width, this.Height);
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            gl.LoadIdentity();
            gl.Ortho(0, this.Width, this.Height, 0, -1, 1);
            gl.MatrixMode(OpenGL.GL_MODELVIEW);
            gl.Hint(OpenGL.GL_PERSPECTIVE_CORRECTION_HINT, OpenGL.GL_NICEST);
        }

        private struct Tile
        {
            public double x, y, w, h;
            public double tx, ty, tw, th;
            public int texture;

            public Tile(Tile otherTile, double _x, double _y)
            {
                x = _x;
                y = _y;
                w = otherTile.w;
                h = otherTile.h;
                tx = otherTile.tx;
                ty = otherTile.ty;
                th = otherTile.th;
                tw = otherTile.tw;
                texture = otherTile.texture;
            }
        }
    }
}
