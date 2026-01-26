using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Text;

namespace Fask.Vyroba_W.Forms
{
	public class FormLocation
	{
		private static Size _screenresolution = Size.Empty;
		private static Point _middlePoint = Point.Empty;
		private static Point _topleft = Point.Empty;
		private static Point _topright = Point.Empty;
		private static Point _bottomleft = Point.Empty;
		private static Point _bottomright = Point.Empty;

		static FormLocation()
		{
			InitializeParams();
		}

		/// <summary>
		/// Vraci pracovni oblast na zarizeni na primarni obrazovce
		/// </summary>
		public static Size ScreenResolution
		{
			get { return _screenresolution; }
		}

		/// <summary>
		/// Vraci pozici na stredu obrazovky zarizeni
		/// </summary>
		public static Point MiddlePoint
		{
			get { return _middlePoint; }
		}

		/// <summary>
		/// Top left point
		/// </summary>
		public static Point TopLeft
		{
			get { return _topleft; }
		}

		/// <summary>
		/// Top right point
		/// </summary>
		public static Point TopRight
		{
			get { return _topright; }
		}

		/// <summary>
		/// Bottom left point
		/// </summary>
		public static Point BottomLeft
		{
			get { return _bottomleft; }
		}

		/// <summary>
		/// Bottom right point
		/// </summary>
		public static Point BottomRight
		{
			get { return _bottomright; }
		}

		/// <summary>
		/// Slouzi k urceni pozice okna na stred obrazovky
		/// </summary>
		/// <param name="s">Velikost okna</param>
		/// <returns>Location okna pro umisteni na stred obrazovky</returns>
		public static Point GetFormLocation(Size s)
		{
			return new Point(_middlePoint.X - s.Width / 2, _middlePoint.Y - s.Height / 2);
		}

		public static void InitializeParams()
		{
			if (Settings.UIHideTaskBar)
				_screenresolution = Screen.PrimaryScreen.Bounds.Size;
			else
				_screenresolution = Screen.PrimaryScreen.WorkingArea.Size;
			_middlePoint = new Point(_screenresolution.Width / 2, _screenresolution.Height / 2);
			_topleft = new Point(0, 0);
			_topright = new Point(_screenresolution.Width, 0);
			_bottomleft = new Point(0, _screenresolution.Height);
			_bottomright = new Point(_screenresolution.Width, _screenresolution.Height);
		}
	}
}
