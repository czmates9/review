using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace KeyboardClassLibrary
{
    public partial class Keyboardcontrol : UserControl
    {
        public Keyboardcontrol()
        {
            InitializeComponent();
        }

        private Boolean shiftindicator = false;
        private Boolean capslockindicator = false;
        private string pvtKeyboardKeyPressed = "";

        private BoW pvtKeyboardType = BoW.Standard;
        public BoW KeyboardType
        {
            get
            {
                return pvtKeyboardType;
            }
            set
            {
                pvtKeyboardType = value;
                if (shiftindicator) HandleShiftClick();
                if (capslockindicator) HandleCapsLock();

                if (pvtKeyboardType == BoW.Standard)
                {
                    pictureBoxKeyboard.Image = KeyboardClassLibrary.Properties.Resources.keyboard_white;
                    pictureBoxCapsLockDown.Image = KeyboardClassLibrary.Properties.Resources.caps_down_white;
                    pictureBoxLeftShiftDown.Image = KeyboardClassLibrary.Properties.Resources.shift_down_white;
                    pictureBoxRightShiftDown.Image = KeyboardClassLibrary.Properties.Resources.shift_down_white;
                }
                else if (pvtKeyboardType==BoW.Alphabetical)
                {
                    pictureBoxKeyboard.Image = KeyboardClassLibrary.Properties.Resources.keyboard_alphabetical;
                    pictureBoxCapsLockDown.Image = KeyboardClassLibrary.Properties.Resources.caps_down_white;
                    pictureBoxLeftShiftDown.Image = KeyboardClassLibrary.Properties.Resources.shift_down_white;
                    pictureBoxRightShiftDown.Image = KeyboardClassLibrary.Properties.Resources.shift_down_white;
                }
                else if(pvtKeyboardType == BoW.Numeric)
                {
                    pictureBoxKeyboard.Image = KeyboardClassLibrary.Properties.Resources.keyboard_white_numeric;
                }
                else if (pvtKeyboardType == BoW.Standard_without_arrows)
                {
                    pictureBoxKeyboard.Image = KeyboardClassLibrary.Properties.Resources.keyboard_white_without_arrows;
                    pictureBoxCapsLockDown.Image = KeyboardClassLibrary.Properties.Resources.caps_down_white;
                    pictureBoxLeftShiftDown.Image = KeyboardClassLibrary.Properties.Resources.shift_down_white;
                    pictureBoxRightShiftDown.Image = KeyboardClassLibrary.Properties.Resources.shift_down_white;
                } else pictureBoxKeyboard.Image = KeyboardClassLibrary.Properties.Resources.keyboard_kids_lower;
            }
        }

        //private double _aspectRatio;
        //private bool _aspectRationKeep = false;
        //[Category("Keyboard"), Description("Keep aspect ration of control")]
        //public bool AspectRationKeep
        //{
        //    get { return _aspectRationKeep; }
        //    set
        //    {
        //        _aspectRationKeep = value;
        //        if (_aspectRationKeep)
        //        {
        //            _aspectBoundsOld = this.Bounds;
        //        }
        //    }
        //}

        [Category("Keyboard"), Description("Return value of mouseclicked key")]
        public event KeyboardDelegate UserKeyPressed;
        protected virtual void OnUserKeyPressed(KeyboardEventArgs e)
        {
            if (UserKeyPressed != null)
                UserKeyPressed(this, e);
        }

        private void pictureBoxKeyboard_MouseClick(object sender, MouseEventArgs e)
        {
            Single xpos = e.X;
            Single ypos = e.Y;

            if (pvtKeyboardType == BoW.Kids)
            {
                xpos = 993 * (xpos / pictureBoxKeyboard.Width);
                ypos = 282 * (ypos / pictureBoxKeyboard.Height);
                pvtKeyboardKeyPressed = HandleKidsMouseClick(xpos, ypos);
            }
            else if (pvtKeyboardType == BoW.Standard)   // white keyboard
            {
                xpos = 993 * (xpos / pictureBoxKeyboard.Width);
                ypos = 282 * (ypos / pictureBoxKeyboard.Height);
                pvtKeyboardKeyPressed = HandleTheMouseClick(xpos, ypos);
            }
            else if (pvtKeyboardType == BoW.Numeric)
            {
                xpos = 457 * (xpos / pictureBoxKeyboard.Width);
                ypos = 226 * (ypos / pictureBoxKeyboard.Height);
                pvtKeyboardKeyPressed = HandleNumericTheMouseClick(xpos, ypos);
            }
            else if (pvtKeyboardType == BoW.Standard_without_arrows)
            {
                xpos = 819 * (xpos / pictureBoxKeyboard.Width);
                ypos = 282 * (ypos / pictureBoxKeyboard.Height);
                pvtKeyboardKeyPressed = HandleTheMouseClick(xpos, ypos);
            }

            KeyboardEventArgs dea = new KeyboardEventArgs(pvtKeyboardKeyPressed);

            OnUserKeyPressed(dea);
        }

        private void pictureBoxLeftShiftState_MouseClick(object sender, MouseEventArgs e)
        {
            HandleShiftClick();
        }

        private void pictureBoxRightShiftState_MouseClick(object sender, MouseEventArgs e)
        {
            HandleShiftClick();
        }

        private void pictureBoxCapsLockState_MouseClick(object sender, MouseEventArgs e)
        {
            HandleCapsLock();
        }

        private string HandleKidsMouseClick(Single x, Single y)
        {
            string Keypressed = null;
            if (y < 73)
            {
                if (x < 79) Keypressed = "a";
                else if (x >= 79 && x < 147) Keypressed = "b";
                else if (x >= 147 && x < 214) Keypressed = "c";
                else if (x >= 214 && x < 281) Keypressed = "d";
                else if (x >= 281 && x < 348) Keypressed = "e";
                else if (x >= 348 && x < 416) Keypressed = "f";
                else if (x >= 416 && x < 491) Keypressed = "g";
                else if (x >= 491 && x < 672) Keypressed = "{BACKSPACE}";
                else if (x >= 672 && x < 764) Keypressed = ".";
                else if (x >= 764 && x < 845) Keypressed = "1";
                else if (x >= 845 && x < 912) Keypressed = "2";
                else if (x >= 912 && x < 989) Keypressed = "3";
            }
            else if (y >= 73 && y < 141)
            {
                if (x < 79) Keypressed = "h";
                else if (x >= 79 && x < 147) Keypressed = "i";
                else if (x >= 147 && x < 214) Keypressed = "j";
                else if (x >= 214 && x < 281) Keypressed = "k";
                else if (x >= 281 && x < 348) Keypressed = "l";
                else if (x >= 348 && x < 416) Keypressed = "m";
                else if (x >= 416 && x < 491) Keypressed = "n";
                else if (x >= 491 && x < 672) HandleCapsLock();
                else if (x >= 672 && x < 764) Keypressed = ",";
                else if (x >= 764 && x < 845) Keypressed = "4";
                else if (x >= 845 && x < 912) Keypressed = "5";
                else if (x >= 912 && x < 989) Keypressed = "6";
            }
            else if (y >= 141 && y < 209)
            {
                if (x < 79) Keypressed = "o";
                else if (x >= 79 && x < 147) Keypressed = "p";
                else if (x >= 147 && x < 214) Keypressed = "q";
                else if (x >= 214 && x < 281) Keypressed = "r";
                else if (x >= 281 && x < 348) Keypressed = "s";
                else if (x >= 348 && x < 416) Keypressed = "t";
                else if (x >= 416 && x < 491) Keypressed = "u";
                else if (x >= 491 && x < 672) Keypressed = "{ENTER}";
                else if (x >= 672 && x < 764) Keypressed = "?";
                else if (x >= 764 && x < 845) Keypressed = "7";
                else if (x >= 845 && x < 912) Keypressed = "8";
                else if (x >= 912 && x < 989) Keypressed = "9";
            }
            else if (y >= 209 && y < 277)
            {
                if (x >= 79 && x < 147) Keypressed = "v";
                else if (x >= 147 && x < 214) Keypressed = "w";
                else if (x >= 214 && x < 281) Keypressed = "x";
                else if (x >= 281 && x < 348) Keypressed = "y";
                else if (x >= 348 && x < 416) Keypressed = "z";
                else if (x >= 491 && x < 672) Keypressed = " ";
                else if (x >= 672 && x < 764) Keypressed = "!";
                else if (x >= 764 && x < 845) Keypressed = "{+}";
                else if (x >= 845 && x < 912) Keypressed = "0";
                else if (x >= 912 && x < 989) Keypressed = "-";
            }
            if (capslockindicator && x < 491) return "+" + Keypressed;
            else return Keypressed;
        }

        /// <summary>
        /// Kliknutí myší na klasické klávesnici.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private string HandleTheMouseClick(Single x, Single y)
        {
            string Keypressed = null;
            if (x >= 4 && x < 815 && y >= 3 && y < 277)         //  keyboard section
            {
                if (y < 58)
                {
                    if (x >= 4 && x < 59) Keypressed = HandleShiftableKey("`");
                    else if (x >= 67 && x < 112) Keypressed = HandleShiftableKey("1");
                    else if (x >= 112 && x < 165) Keypressed = HandleShiftableKey("2");
                    else if (x >= 165 && x < 220) Keypressed = HandleShiftableKey("3");
                    else if (x >= 220 && x < 275) Keypressed = HandleShiftableKey("4");
                    else if (x >= 275 && x < 328) Keypressed = HandleShiftableKey("5");
                    else if (x >= 328 && x < 380) Keypressed = HandleShiftableKey("6");
                    else if (x >= 380 && x < 435) Keypressed = HandleShiftableKey("7");
                    else if (x >= 435 && x < 490) Keypressed = HandleShiftableKey("8");
                    else if (x >= 490 && x < 545) Keypressed = HandleShiftableKey("9");
                    else if (x >= 545 && x < 600) Keypressed = HandleShiftableKey("0");
                    else if (x >= 600 && x < 655) Keypressed = HandleShiftableKey("-");
                    else if (x >= 655 && x < 705) Keypressed = HandleShiftableKey("=");
                    else if (x >= 705 && x < 815) Keypressed = "{BACKSPACE}";
                    else Keypressed = null;
                }
                else if (y >= 58 && y < 114)
                {
                    if (x >= 85 && x < 140) Keypressed = HandleShiftableCaplockableKey("q");
                    else if (x >= 140 && x < 193) Keypressed = HandleShiftableCaplockableKey("w");
                    else if (x >= 193 && x < 247) Keypressed = HandleShiftableCaplockableKey("e");
                    else if (x >= 247 && x < 300) Keypressed = HandleShiftableCaplockableKey("r");
                    else if (x >= 300 && x < 355) Keypressed = HandleShiftableCaplockableKey("t");
                    else if (x >= 355 && x < 409) Keypressed = HandleShiftableCaplockableKey("y");
                    else if (x >= 409 && x < 463) Keypressed = HandleShiftableCaplockableKey("u");
                    else if (x >= 463 && x < 517) Keypressed = HandleShiftableCaplockableKey("i");
                    else if (x >= 517 && x < 571) Keypressed = HandleShiftableCaplockableKey("o");
                    else if (x >= 571 && x < 625) Keypressed = HandleShiftableCaplockableKey("p");
                    else if (x >= 625 && x < 680) Keypressed = HandleShiftableKey("{[}");
                    else if (x >= 680 && x < 733) Keypressed = HandleShiftableKey("{]}");
                    else Keypressed = null;
                }
                else if (y >= 114 && y < 168)
                {
                    if (x >= 4 && x < 113) HandleCapsLock();
                    else if (x >= 113 && x < 167) Keypressed = HandleShiftableCaplockableKey("a");
                    else if (x >= 167 && x < 221) Keypressed = HandleShiftableCaplockableKey("s");
                    else if (x >= 221 && x < 275) Keypressed = HandleShiftableCaplockableKey("d");
                    else if (x >= 275 && x < 330) Keypressed = HandleShiftableCaplockableKey("f");
                    else if (x >= 330 && x < 383) Keypressed = HandleShiftableCaplockableKey("g");
                    else if (x >= 383 && x < 437) Keypressed = HandleShiftableCaplockableKey("h");
                    else if (x >= 437 && x < 491) Keypressed = HandleShiftableCaplockableKey("j");
                    else if (x >= 491 && x < 545) Keypressed = HandleShiftableCaplockableKey("k");
                    else if (x >= 545 && x < 599) Keypressed = HandleShiftableCaplockableKey("l");
                    else if (x >= 599 && x < 653) Keypressed = HandleShiftableKey(";");
                    else if (x >= 653 && x < 706) Keypressed = HandleShiftableKey("'");
                    else if (x >= 706 && x < 815) Keypressed = "{ENTER}";
                    else Keypressed = null;
                }
                else if (y >= 168 && y < 221)
                {
                    if (x >= 4 && x < 140) HandleShiftClick();
                    else if (x >= 140 && x < 194) Keypressed = HandleShiftableCaplockableKey("z");
                    else if (x >= 194 && x < 248) Keypressed = HandleShiftableCaplockableKey("x");
                    else if (x >= 248 && x < 302) Keypressed = HandleShiftableCaplockableKey("c");
                    else if (x >= 302 && x < 356) Keypressed = HandleShiftableCaplockableKey("v");
                    else if (x >= 356 && x < 410) Keypressed = HandleShiftableCaplockableKey("b");
                    else if (x >= 410 && x < 464) Keypressed = HandleShiftableCaplockableKey("n");
                    else if (x >= 464 && x < 518) Keypressed = HandleShiftableCaplockableKey("m");
                    else if (x >= 518 && x < 572) Keypressed = HandleShiftableKey(",");
                    else if (x >= 572 && x < 626) Keypressed = HandleShiftableKey(".");
                    else if (x >= 626 && x < 680) Keypressed = HandleShiftableKey("/");
                    else if (x >= 680 && x < 815) HandleShiftClick();
                    else Keypressed = null;
                }
                else if (y >= 221 && y < 277)
                {
                    if (x >= 218 && x < 597) Keypressed = " ";
                    else Keypressed = null;
                }
            }
            else if (x >= 827 && x < 989 && y >= 27 && y < 193)   //  cursor keys
            {
                if (y < 83)
                {
                    if (x < 880) Keypressed = "{INSERT}";
                    else if (x >= 880 && x < 934) Keypressed = "{UP}";
                    else if (x >= 934) Keypressed = HandleShiftableKey("{HOME}");
                    else Keypressed = null;
                }
                else if (y >= 83 && y < 137)
                {
                    if (x < 880) Keypressed = "{LEFT}";
                    else if (x >= 934) Keypressed = "{RIGHT}";
                    else Keypressed = null;
                }
                else if (y >= 137)
                {
                    if (x < 880) Keypressed = "{DELETE}";
                    else if (x >= 880 && x < 934) Keypressed = "{DOWN}";
                    else if (x >= 934) Keypressed = HandleShiftableKey("{END}");
                    else Keypressed = null;
                }
                else Keypressed = null;
            }
            if (ClassicWithoutArrowsRegions.SwitchFromClassicWithoutArrows.Contains((int)x, (int)y))
            {
                this.KeyboardType = BoW.Numeric;
            }
            if (Keypressed != null)
            {
                if (shiftindicator) HandleShiftClick();
                return Keypressed;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Kliknutí myší na numerické klávesnici s šipkami.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private string HandleNumericTheMouseClick(Single x, Single y)
        {

            if (NumericRegions.Right.Contains((int)x, (int)y))
                return "{RIGHT}";
            if (NumericRegions.End.Contains((int)x, (int)y))
                return "{END}";
            if (NumericRegions.Ins.Contains((int)x, (int)y))
                return "{INSERT}";
            if (NumericRegions.Left.Contains((int)x, (int)y))
                return "{LEFT}";
            if (NumericRegions.Del.Contains((int)x, (int)y))
                return "{DELETE}";
            if (NumericRegions.Up.Contains((int)x, (int)y))
                return "{UP}";
            if (NumericRegions.Down.Contains((int)x, (int)y))
                return "{DOWN}";
            if (NumericRegions.Home.Contains((int)x, (int)y))
                return "{HOME}";
            if (NumericRegions.SwitchFromNumeric.Contains((int)x, (int)y))
            {
                this.KeyboardType = BoW.Standard_without_arrows;
            }

            if (NumericRegions.Sedm.Contains((int)x, (int)y))
                return "7";
            if (NumericRegions.Osm.Contains((int)x, (int)y))
                return "8";
            if (NumericRegions.Devet.Contains((int)x, (int)y))
                return "9";

            if (NumericRegions.Ctyri.Contains((int)x, (int)y))
                return "4";
            if (NumericRegions.Pet.Contains((int)x, (int)y))
                return "5";
            if (NumericRegions.Sest.Contains((int)x, (int)y))
                return "6";
        
            if (NumericRegions.Jedna.Contains((int)x, (int)y))
                return "1";
            if (NumericRegions.Dve.Contains((int)x, (int)y))
                return "2";
            if (NumericRegions.Tri.Contains((int)x, (int)y))
                return "3";

            if (NumericRegions.Nula.Contains((int)x, (int)y))
                return "0";
            if (NumericRegions.Carka.Contains((int)x, (int)y))
                return ",";

            if (NumericRegions.Backspace.Contains((int)x, (int)y))
                return "{BACKSPACE}";
       
            return null;
        
        }

        private string HandleShiftableKey(string theKey)
        {
            if (shiftindicator)
            {
                return "+" + theKey;
            }
            else
            {
                return theKey;
            }
        }

        private string HandleShiftableCaplockableKey(string theKey)
        {
            if (pvtKeyboardType == BoW.Alphabetical)
            {
                switch (theKey)
                {
                    case ("q"):
                        theKey = "a";
                        break;
                    case ("w"):
                        theKey = "b";
                        break;
                    case ("e"):
                        theKey = "c";
                        break;
                    case ("r"):
                        theKey = "d";
                        break;
                    case ("t"):
                        theKey = "e";
                        break;
                    case ("y"):
                        theKey = "f";
                        break;
                    case ("u"):
                        theKey = "g";
                        break;
                    case ("i"):
                        theKey = "h";
                        break;
                    case ("o"):
                        theKey = "i";
                        break;
                    case ("p"):
                        theKey = "j";
                        break;
                    case ("a"):
                        theKey = "k";
                        break;
                    case ("s"):
                        theKey = "l";
                        break;
                    case ("d"):
                        theKey = "m";
                        break;
                    case ("f"):
                        theKey = "n";
                        break;
                    case ("g"):
                        theKey = "o";
                        break;
                    case ("h"):
                        theKey = "p";
                        break;
                    case ("j"):
                        theKey = "q";
                        break;
                    case ("k"):
                        theKey = "r";
                        break;
                    case ("l"):
                        theKey = "s";
                        break;
                    case ("z"):
                        theKey = "t";
                        break;
                    case ("x"):
                        theKey = "u";
                        break;
                    case ("c"):
                        theKey = "v";
                        break;
                    case ("v"):
                        theKey = "w";
                        break;
                    case ("b"):
                        theKey = "x";
                        break;
                    case ("n"):
                        theKey = "y";
                        break;
                    case ("m"):
                        theKey = "z";
                        break;
                }
            }
            if (capslockindicator)
            {
                return "+" + theKey;
            }
            else if (shiftindicator)
            {
                return "+" + theKey;
            }
            else
            {
                return theKey;
            }
        }

        private void HandleShiftClick()
        {
            if (shiftindicator)
            {
                shiftindicator = false;
                pictureBoxLeftShiftDown.Visible = false;
                pictureBoxRightShiftDown.Visible = false;
            }
            else
            {
                shiftindicator = true;
                pictureBoxLeftShiftDown.Visible = true;
                pictureBoxRightShiftDown.Visible = true;
            }
        }

        private void HandleCapsLock()
        {
            if (capslockindicator)
            {
                capslockindicator = false;
                pictureBoxCapsLockDown.Visible = false;
                if (pvtKeyboardType == BoW.Kids) pictureBoxKeyboard.Image = KeyboardClassLibrary.Properties.Resources.keyboard_kids_lower;
            }
            else
            {
                capslockindicator = true;
                if (pvtKeyboardType == BoW.Kids) pictureBoxKeyboard.Image = KeyboardClassLibrary.Properties.Resources.keyboard_kids_upper;
                else pictureBoxCapsLockDown.Visible = true;
            }
        }

        private void pictureBoxKeyboard_SizeChanged(object sender, EventArgs e)
        {
            // standartní klávesnice (white)
            if (pvtKeyboardType == BoW.Standard)
            {
                // position the capslock and shift down overlays
                pictureBoxCapsLockDown.Left = Convert.ToInt16(pictureBoxKeyboard.Width * 5 / 993);
                pictureBoxCapsLockDown.Top = Convert.ToInt16(pictureBoxKeyboard.Height * 115 / 282);
                pictureBoxLeftShiftDown.Left = Convert.ToInt16(pictureBoxKeyboard.Width * 5 / 993);
                pictureBoxLeftShiftDown.Top = Convert.ToInt16(pictureBoxKeyboard.Height * 169 / 282);
                pictureBoxRightShiftDown.Left = Convert.ToInt16(pictureBoxKeyboard.Width * 681 / 993);
                pictureBoxRightShiftDown.Top = pictureBoxLeftShiftDown.Top;


                // size the capslock and shift down overlays

                pictureBoxCapsLockDown.Width = Convert.ToInt16(pictureBoxKeyboard.Width * 110 / 993);
                pictureBoxCapsLockDown.Height = Convert.ToInt16(pictureBoxKeyboard.Height * 55 / 282);
                pictureBoxLeftShiftDown.Width = Convert.ToInt16(pictureBoxKeyboard.Width * 136 / 993);
                pictureBoxLeftShiftDown.Height = Convert.ToInt16(pictureBoxKeyboard.Height * 55 / 282);
                pictureBoxRightShiftDown.Width = Convert.ToInt16(pictureBoxKeyboard.Width * 135 / 993);
                pictureBoxRightShiftDown.Height = pictureBoxLeftShiftDown.Height;
            }
            else
            {
                // position the capslock and shift down overlays
                pictureBoxCapsLockDown.Left = Convert.ToInt16(pictureBoxKeyboard.Width * 5 / 819);
                pictureBoxCapsLockDown.Top = Convert.ToInt16(pictureBoxKeyboard.Height * 115 / 282);
                pictureBoxLeftShiftDown.Left = Convert.ToInt16(pictureBoxKeyboard.Width * 5 / 819);
                pictureBoxLeftShiftDown.Top = Convert.ToInt16(pictureBoxKeyboard.Height * 169 / 282);
                pictureBoxRightShiftDown.Left = Convert.ToInt16(pictureBoxKeyboard.Width * 681 / 819);
                pictureBoxRightShiftDown.Top = pictureBoxLeftShiftDown.Top;


                // size the capslock and shift down overlays

                pictureBoxCapsLockDown.Width = Convert.ToInt16(pictureBoxKeyboard.Width * 110 / 819);
                pictureBoxCapsLockDown.Height = Convert.ToInt16(pictureBoxKeyboard.Height * 55 / 282);
                pictureBoxLeftShiftDown.Width = Convert.ToInt16(pictureBoxKeyboard.Width * 136 / 819);
                pictureBoxLeftShiftDown.Height = Convert.ToInt16(pictureBoxKeyboard.Height * 55 / 282);
                pictureBoxRightShiftDown.Width = Convert.ToInt16(pictureBoxKeyboard.Width * 135 / 819);
                pictureBoxRightShiftDown.Height = pictureBoxLeftShiftDown.Height;
            }
        }

        //Rectangle _aspectBoundsOld = Rectangle.Empty;
        //Keep aspect ration
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            //if (_aspectBoundsOld == Rectangle.Empty || !AspectRationKeep)
            //    this._aspectRatio = (float)this.Height / (float)this.Width;
            //else
            //{
            //    int height = (int)(this.Width * this._aspectRatio);
            //    if (this.Dock == DockStyle.Bottom || (this.Anchor & AnchorStyles.Bottom) == AnchorStyles.Bottom)
            //    { // je dokovany dolu => posunout TOP
            //        this.Top = _aspectBoundsOld.Bottom + height;
            //    }
            //    else
            //    {
            //        this.Height = height;
            //    }
            //}

            //_aspectBoundsOld = this.Bounds;

            base.OnSizeChanged(e);
        }

    }

    public delegate void KeyboardDelegate(object sender, KeyboardEventArgs e);

    public class KeyboardEventArgs : EventArgs
    {
        private readonly string pvtKeyboardKeyPressed;

        public KeyboardEventArgs(string KeyboardKeyPressed)
        {
            this.pvtKeyboardKeyPressed = KeyboardKeyPressed;
        }

        public string KeyboardKeyPressed
        {
            get
            {
                return pvtKeyboardKeyPressed;
            }
        }
    }    

    [Category("Keyboard"),Description("Type of keyboard to use")]
    public enum BoW { Standard, Alphabetical, Kids, Numeric, Standard_without_arrows };

    public class NumericRegions
    {
        public static Rectangle Ins = new Rectangle(4, 4, 54, 54);
        public static Rectangle Left = new Rectangle(4, 59, 54, 54);
        public static Rectangle Del = new Rectangle(4, 113, 54, 54);

        public static Rectangle Up = new Rectangle(59, 4, 54, 54);
        public static Rectangle Down = new Rectangle(58, 112, 54, 54);

        public static Rectangle Home = new Rectangle(113, 4, 54, 54);
        public static Rectangle Right = new Rectangle(112, 58, 54, 54);
        public static Rectangle End = new Rectangle(112, 112, 54, 54);

        public static Rectangle SwitchFromNumeric = new Rectangle(400, 170, 58, 58);

        public static Rectangle Sedm = new Rectangle(195, 4, 54, 54);
        public static Rectangle Osm = new Rectangle(250, 4, 54, 54);
        public static Rectangle Devet = new Rectangle(303, 4, 54, 54);

        public static Rectangle Ctyri = new Rectangle(195, 59, 54, 54);
        public static Rectangle Pet = new Rectangle(250, 59, 54, 54);
        public static Rectangle Sest = new Rectangle(303, 59, 54, 54);

        public static Rectangle Jedna = new Rectangle(195, 114, 54, 54);
        public static Rectangle Dve = new Rectangle(250, 114, 54, 54);
        public static Rectangle Tri = new Rectangle(303, 114, 54, 54);

        public static Rectangle Nula = new Rectangle(195, 169, 110, 54);
        public static Rectangle Carka = new Rectangle(303, 169, 54, 54);

        public static Rectangle Backspace = new Rectangle(361, 4, 94, 54);

    }

    public class ClassicWithoutArrowsRegions
    {
        public static Rectangle SwitchFromClassicWithoutArrows = new Rectangle(761, 225, 54, 54);
    }
}

