namespace Extend.DTAnimation.Editor
{
    [DotweenTemplate(DotweenTemplateType.Button)]
    public class ButtonDotweenTemplate : BaseTemplate
    {
        public ButtonDotweenTemplate()
        {
            CombineTemplate temp = new CombineTemplate();
            temp.editorName = "PointerDown";
            temp.animations.Add(typeof(MoveAnimation));
            temp.animations.Add(typeof(PunchPositionAnimation));
            temp.animations.Add(typeof(ScaleAnimation));
            temp.animations.Add(typeof(PunchScaleAnimation));

            combines.Add(temp);

            temp = new CombineTemplate();
            temp.editorName = "PointerClick";
            temp.animations.Add(typeof(MoveAnimation));
            temp.animations.Add(typeof(PunchPositionAnimation));
            temp.animations.Add(typeof(ScaleAnimation));
            temp.animations.Add(typeof(PunchScaleAnimation));

            combines.Add(temp);

            temp = new CombineTemplate();
            temp.editorName = "PointerUp";
            temp.animations.Add(typeof(MoveAnimation));
            temp.animations.Add(typeof(PunchPositionAnimation));
            temp.animations.Add(typeof(ScaleAnimation));
            temp.animations.Add(typeof(PunchScaleAnimation));

            combines.Add(temp);
        }
    }

    [DotweenTemplate(DotweenTemplateType.UIView)]
    public class UIViewDotweenTemplate : BaseTemplate
    {
        public UIViewDotweenTemplate()
        {
            CombineTemplate temp = new CombineTemplate();
            temp.editorName = "ShowAnimation";
            temp.animations.Add(typeof(MoveAnimation));
            temp.animations.Add(typeof(RotateAnimation));
            temp.animations.Add(typeof(ScaleAnimation));
            temp.animations.Add(typeof(FadeAnimation));

            combines.Add(temp);

            temp = new CombineTemplate();
            temp.editorName = "HideAnimation";
            temp.animations.Add(typeof(MoveAnimation));
            temp.animations.Add(typeof(RotateAnimation));
            temp.animations.Add(typeof(ScaleAnimation));
            temp.animations.Add(typeof(FadeAnimation));

            combines.Add(temp);

            temp = new CombineTemplate();
            temp.editorName = "LoopAnimation";
            temp.animations.Add(typeof(MoveAnimation));
            temp.animations.Add(typeof(RotateAnimation));
            temp.animations.Add(typeof(ScaleAnimation));
            temp.animations.Add(typeof(FadeAnimation));

            combines.Add(temp);
        }
    }

    [DotweenTemplate(DotweenTemplateType.UIFly)]
    public class UIFlyDotweenTemplate : BaseTemplate
    {
        public UIFlyDotweenTemplate()
        {
            CombineTemplate temp = new CombineTemplate();
            temp.editorName = "FlyFade";
            temp.animations.Add(typeof(MoveAnimation));
            temp.animations.Add(typeof(FadeAnimation));
            combines.Add(temp);

            temp = new CombineTemplate();
            temp.editorName = "FlyPath";
            temp.animations.Add(typeof(MovePathAnimation));
            combines.Add(temp);

            temp = new CombineTemplate();
            temp.editorName = "FlyDelayFly";
            temp.combineType = CombineType.Sequence;
            temp.animations.Add(typeof(MoveAnimation));
            temp.animations.Add(typeof(MoveAnimation));
            combines.Add(temp);
        }
    }
}