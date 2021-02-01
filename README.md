# DTAnimation

BaseAnimation						创建和修改Tween

	MoveAnimation					创建和修改移动tween

	ScaleAnimation					创建和修改缩放tween

	RotateAnimation					创建和修改旋转tween

	FadeAnimation					创建和修改消失tween，包括CanvasGroup，Image，Text，Material，Light，SpriteRender

	PunchAnimation

		PunchScaleAnimation

		PunchPositionAnimation

		PunchRotateAnimation

	ShakeAnimation

		ShakeScaleAnimation

		ShakePositionAnimation

		ShakeRoatateAnimation

BaseCombine							创建和管理所有的BaseAnimation
	
	ConcurrenceCombine				并行播放所有Enable的Animation
	
	SequenceCombine					顺序播放所有Enable的Animation

ITrigger							可在DTAnimation的播放等时机添加触发回调
	
	AudioTrigger					触发播放音效

	LogTrigger						触发打印日志
	
DTAnimation							创建和管理所有的BaseCombine
	
	ConcurrenceDTAnimation			并行播放所有Enable的Combine里的Enable的Animation
	
	SequenceDTAnimation				顺序播放所有Enable的Combine里的Enable的Animation
	

支持预览功能
支持自定义动画
支持触发回调

使用Unity2019.3以上版本
使用方式参考Example.prefab