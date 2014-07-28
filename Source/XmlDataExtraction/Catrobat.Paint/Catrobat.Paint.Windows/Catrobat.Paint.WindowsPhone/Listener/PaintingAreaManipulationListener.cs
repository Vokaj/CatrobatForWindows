﻿using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Catrobat.Paint.Phone.Tool;
using Catrobat.Paint.Phone.Ui;

namespace Catrobat.Paint.Phone.Listener
{
    class PaintingAreaManipulationListener 
    {
        public void ManipulationStarted(object sender, ManipulationStartedEventArgs e)
        {
            var point = new Point(Convert.ToInt32(e.ManipulationOrigin.X), Convert.ToInt32(e.ManipulationOrigin.Y));

            // TODO some bubbling? issue here, fast multiple applicationbartop undos result in triggering this event
            if (point.X < 0 || point.Y < 0 || Spinner.SpinnerActive || e.Handled)
            {
                return;
            }
            
            PocketPaintApplication.GetInstance().ToolCurrent.HandleDown(point);
        }

        public void ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            var point = new Point(Convert.ToInt32(e.ManipulationOrigin.X), Convert.ToInt32(e.ManipulationOrigin.Y));

            // TODO some bubbling? issue here, fast multiple applicationbartop undos result in triggering this event
            if (point.X < 0 || point.Y < 0 || Spinner.SpinnerActive || e.Handled)
            {
                return;
            }


            object movezoom;
            if (e.PinchManipulation != null)
            {

                movezoom = new ScaleTransform();
                if (e.DeltaManipulation.Scale.X > 0 && e.DeltaManipulation.Scale.Y > 0)
                {
                    System.Diagnostics.Debug.WriteLine("Scale.X " + e.DeltaManipulation.Scale.X + " Scale.Y " +
                                                       e.DeltaManipulation.Scale.Y);
                    ((ScaleTransform)movezoom).ScaleX *= e.DeltaManipulation.Scale.X;
                    ((ScaleTransform)movezoom).ScaleY *= e.DeltaManipulation.Scale.Y;
                }
            }
            else
            {
                movezoom = new TranslateTransform();
                int right_left = PocketPaintApplication.GetInstance().PaintData.max_right_left;
                int difference = right_left + Convert.ToInt32(e.DeltaManipulation.Translation.X);

                bool move_allowed = false;
                int move_x = 0;



                    ((TranslateTransform)movezoom).X += Convert.ToInt32(e.DeltaManipulation.Translation.X);
                    ((TranslateTransform)movezoom).Y += e.DeltaManipulation.Translation.Y;
                    PocketPaintApplication.GetInstance().PaintData.max_right_left = PocketPaintApplication.GetInstance().PaintData.max_right_left + Convert.ToInt32(e.DeltaManipulation.Translation.X);
            }

            switch (PocketPaintApplication.GetInstance().ToolCurrent.GetToolType())
            {
                case ToolType.Brush:
                case ToolType.Eraser:
                    PocketPaintApplication.GetInstance().ToolCurrent.HandleMove(point);
                    break;
               case ToolType.Move:
               case ToolType.Zoom:
                    PocketPaintApplication.GetInstance().ToolCurrent.HandleMove(movezoom);
                    break;
               case ToolType.Line:
                    PocketPaintApplication.GetInstance().ToolCurrent.HandleMove(point);
                    break;

            }
        }
       

        public void ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            var point = new Point(Convert.ToInt32(e.ManipulationOrigin.X), Convert.ToInt32(e.ManipulationOrigin.Y));

            // TODO some bubbling? issue here, fast multiple applicationbartop undos result in triggering this event
            if (point.X < 0 || point.Y < 0 || Spinner.SpinnerActive || e.Handled)
            {
                return;
            }

            PocketPaintApplication.GetInstance().ToolCurrent.HandleUp(point);           
        }
    }
}