﻿using System.ComponentModel;
using System.Diagnostics;

namespace ITLDG.MauiLed
{
    public class LedView : GraphicsView
    {
        /// <summary>
        /// 存放绘画数据 每个Bit位,代表一个Led灯
        /// </summary>
        public static readonly BindableProperty DataProperty = BindableProperty.Create(nameof(Data), typeof(byte), typeof(LedDrawable), (byte)0, propertyChanged: OnMyParameterChanged);
        public byte Data
        {
            get => (byte)GetValue(DataProperty);
            set => SetValue(DataProperty, value);
        }

        ///// <summary>
        ///// 点亮时LED颜色
        ///// </summary>
        public static readonly BindableProperty LightColorProperty = BindableProperty.Create(nameof(LightColor), typeof(Color), typeof(LedDrawable), Colors.Red, propertyChanged: OnMyParameterChanged);
        public Color LightColor
        {
            get => (Color)GetValue(LightColorProperty);
            set => SetValue(LightColorProperty, value);
        }
        ///// <summary>
        ///// 熄灭时LED颜色
        ///// </summary>
        public static readonly BindableProperty NightColorProperty = BindableProperty.Create(nameof(NightColor), typeof(Color), typeof(LedDrawable), new Color(0, 0, 0, 0.2f), propertyChanged: OnMyParameterChanged);
        public Color NightColor
        {
            get => (Color)GetValue(NightColorProperty);
            set => SetValue(NightColorProperty, value);
        }

        private static void OnMyParameterChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var ledView = bindable as LedView;
            ledView.ledDrawable.Data = ledView.Data;
            ledView.ledDrawable.NightColor = ledView.NightColor;
            ledView.ledDrawable.LightColor = ledView.LightColor;
            ledView.Invalidate();
        }

        readonly LedDrawable ledDrawable = null;
        public LedView()
        {
            ledDrawable = new LedDrawable();
            Drawable = ledDrawable;
        }
    }

    /// <summary>
    /// 绘画类
    /// </summary>
    public class LedDrawable : IDrawable
    {
        /// <summary>
        /// 点亮时LED颜色
        /// </summary>
        public Color LightColor = Colors.Red;

        /// <summary>
        /// 熄灭时LED颜色
        /// </summary>
        public Color NightColor = new(0, 0, 0, 0.2f);

        /// <summary>
        /// 存放绘画数据 每个Bit位,代表一个Led灯
        /// </summary>
        public byte Data = 0;
        /// <summary>
        /// 所有路径
        /// </summary>
        readonly PathF[] ledPaths = new PathF[8];
        /// <summary>
        /// 初始化路径
        /// </summary>
        /// <param name="dirtyRect">可绘画区域</param>
        public void Init(RectF dirtyRect)
        {
            float lw = dirtyRect.Height / 10; //线宽
            float ll = lw * 3; //线长
            if (lw % 2 > 0)
            {
                lw--; //偶数
            }
            float lw1 = lw - 1; //奇数  线宽
            float p = 0;
            for (int i = 0; i < lw1; i++)
            {
                if (i * 2 + 1 >= lw1)
                {
                    p = i + 1;
                    break;
                }
            }
            float ll2 = ll + p * 2;
            float x = lw / 2; //X偏移
            float x1 = x / 2;
            float x2 = x1 / 2;
            float y = 0; //Y偏移


            List<PointF> a = new();
            List<PointF> b = new();
            List<PointF> c = new();
            List<PointF> d = new();
            List<PointF> e = new();
            List<PointF> f = new();
            List<PointF> g = new();
            List<PointF> h = new();

            a.Add(new PointF(x + x1, y + x));
            a.Add(new PointF(a[0].X + p, y));
            a.Add(new PointF(a[1].X + ll, a[1].Y));
            a.Add(new PointF(a[0].X + ll2, a[0].Y));
            a.Add(new PointF(a[2].X, a[2].Y + lw));
            a.Add(new PointF(a[1].X, a[1].Y + lw));
            float fb = a[3].X - a[0].X + x1; //f段到b段的偏移量
            g.Add(new PointF(a[0].X, a[0].Y + fb));
            g.Add(new PointF(a[1].X, a[1].Y + fb));
            g.Add(new PointF(a[2].X, a[2].Y + fb));
            g.Add(new PointF(a[3].X, a[3].Y + fb));
            g.Add(new PointF(a[4].X, a[4].Y + fb));
            g.Add(new PointF(a[5].X, a[5].Y + fb));

            d.Add(new PointF(a[0].X, g[0].Y + fb));
            d.Add(new PointF(a[1].X, g[1].Y + fb));
            d.Add(new PointF(a[2].X, g[2].Y + fb));
            d.Add(new PointF(a[3].X, g[3].Y + fb));
            d.Add(new PointF(a[4].X, g[4].Y + fb));
            d.Add(new PointF(a[5].X, g[5].Y + fb));

            f.Add(new PointF(a[0].X - x2, a[0].Y + x2));
            f.Add(new PointF(a[5].X - x2, a[5].Y + x2));
            f.Add(new PointF(f[1].X, f[1].Y + ll));
            f.Add(new PointF(f[0].X, f[0].Y + ll2));
            f.Add(new PointF(f[0].X - p, f[2].Y));
            f.Add(new PointF(f[1].X - lw, f[1].Y));

            b.Add(new PointF(f[0].X + fb, f[0].Y));
            b.Add(new PointF(f[1].X + fb, f[1].Y));
            b.Add(new PointF(f[2].X + fb, f[2].Y));
            b.Add(new PointF(f[3].X + fb, f[3].Y));
            b.Add(new PointF(f[4].X + fb, f[4].Y));
            b.Add(new PointF(f[5].X + fb, f[5].Y));

            c.Add(new PointF(f[0].X + fb, f[0].Y + fb));
            c.Add(new PointF(f[1].X + fb, f[1].Y + fb));
            c.Add(new PointF(f[2].X + fb, f[2].Y + fb));
            c.Add(new PointF(f[3].X + fb, f[3].Y + fb));
            c.Add(new PointF(f[4].X + fb, f[4].Y + fb));
            c.Add(new PointF(f[5].X + fb, f[5].Y + fb));

            e.Add(new PointF(f[0].X, f[0].Y + fb));
            e.Add(new PointF(f[1].X, f[1].Y + fb));
            e.Add(new PointF(f[2].X, f[2].Y + fb));
            e.Add(new PointF(f[3].X, f[3].Y + fb));
            e.Add(new PointF(f[4].X, f[4].Y + fb));
            e.Add(new PointF(f[5].X, f[5].Y + fb));

            h.Add(new PointF(c[2].X + lw / 4, c[2].Y));
            h.Add(new PointF(h[0].X + lw, h[0].Y));
            h.Add(new PointF(h[0].X + lw, h[0].Y + lw));
            h.Add(new PointF(h[0].X, h[0].Y + lw));

            ledPaths[0] = GetPathF(a);
            ledPaths[1] = GetPathF(b);
            ledPaths[2] = GetPathF(c);
            ledPaths[3] = GetPathF(d);
            ledPaths[4] = GetPathF(e);
            ledPaths[5] = GetPathF(f);
            ledPaths[6] = GetPathF(g);
            ledPaths[7] = GetPathF(h);
        }
        static PathF GetPathF(List<PointF> points)
        {
            PathF path = new();
            path.MoveTo(points[0]);
            for (int i = 1; i < points.Count; i++)
            {
                path.LineTo(points[i]);
            }
            path.Close();
            return path;
        }
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            if (ledPaths[0] == null)
            {
                //首次进入,初始化路径
                Init(dirtyRect);
            }
            //Debug.WriteLine("正在绘制", Data.ToString());
            for (int i = 0; i < 8; i++)
            {
                canvas.FillColor = (Data >> i & 1) == 1 ? LightColor : NightColor;
                canvas.FillPath(ledPaths[i]);
            }

        }
    }
}
