using Microsoft.Maui.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bonsai_api_client.Models.Graphics
{
    public abstract class DrawnTransform
    {
        protected PointF OriginPoint;
        protected Color FillColor;
        protected PointF LastClickedPoint;
        public virtual PointF Origin => OriginPoint;
        public virtual PointF CenterOrigin => OriginPoint;

        public abstract void Draw(ICanvas canvas, RectF dirtyRect);

        public abstract bool ContainsPoint(PointF point);
        public abstract void SetPosition(PointF point);
        public abstract void OnSelect(PointF clickPosition);
        public abstract void OnDeselect();
        public abstract void OnDrag(PointF dragePosition);
    }

    public class DrawnRectangle : DrawnTransform
    {
        protected float Width;
        protected float Height;
        public override PointF CenterOrigin => new PointF(OriginPoint.X + (Width / 2), OriginPoint.Y + (Height / 2));
        public DrawnRectangle(PointF originPoint, float width, float height, Color fillColor)
        {
            OriginPoint = originPoint;
            Width = width;
            Height = height;
            FillColor = fillColor;
        }
        public override void Draw(ICanvas canvas, RectF dirtyRect)
        {
            canvas.FillColor = FillColor;
            canvas.FillRectangle(OriginPoint.X, OriginPoint.Y, Width, Height);
        }
        public override bool ContainsPoint(PointF point)
        {
            RectF boundingRect = new RectF(OriginPoint.X, OriginPoint.Y, Width, Height);
            return boundingRect.Contains(point);
        }
        public override void OnDeselect()
        {
            
        }

        public override void OnDrag(PointF dragePosition)
        {
            
        }

        public override void OnSelect(PointF clickPosition)
        {
            
        }

        public override void SetPosition(PointF point)
        {
            OriginPoint = point;
        }
    }
}
