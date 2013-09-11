using UnityEngine;
using System.Collections;

// This script is part of the tutorial series "Making a 2D game with Unity3D using only free tools"
// http://www.rocket5studios.com/tutorials/make-a-2d-game-in-unity3d-using-only-free-tools-part-1

public class PlayerAnims : MonoBehaviour {

	OTAnimatingSprite mySprite;
	xa.anim currentAnim;
	
	// Use this for initialization
	void Start () 
	{
		mySprite = GetComponent<OTAnimatingSprite>();
	}
	
	void Update() 
	{
		//go left
		if(xa.isLeft && currentAnim != xa.anim.WalkLeft && !xa.isUpLeft && !xa.isDownLeft)
		{	
			currentAnim = xa.anim.WalkLeft;
			mySprite.Play("runLeft");
			xa.facingDir = 7;
		}
		if(!xa.isLeft && currentAnim != xa.anim.StandLeft && xa.facingDir == 7)
		{
			currentAnim = xa.anim.StandLeft;
			mySprite.ShowFrame(0);//13);
		}
		
		//go right
		if(xa.isRight && currentAnim != xa.anim.WalkRight && !xa.isUpRight && !xa.isDownRight)
		{
			currentAnim = xa.anim.WalkRight;
			mySprite.Play("runRight");
			xa.facingDir = 3;
		}
		if(!xa.isRight && currentAnim != xa.anim.StandRight && xa.facingDir == 3)
		{
			currentAnim = xa.anim.StandRight;
			mySprite.ShowFrame(0);//16);
		}
		
		//go up
		if(xa.isUp && currentAnim != xa.anim.WalkUp && !xa.isUpLeft && !xa.isUpRight)
		{
			currentAnim = xa.anim.WalkUp;
			mySprite.Play("climb");
			xa.facingDir = 1;
		}
		if(!xa.isUp && currentAnim != xa.anim.StandUp && xa.facingDir == 1)
		{
			currentAnim = xa.anim.StandUp;
			mySprite.ShowFrame(0);
		}
		
		//go down
		if(xa.isDown && currentAnim != xa.anim.WalkDown && !xa.isDownLeft && !xa.isDownRight)
		{
			currentAnim = xa.anim.WalkDown;
			mySprite.Play ("climb");
			xa.facingDir = 5;
		}
		if(!xa.isDown && currentAnim != xa.anim.StandDown && xa.facingDir == 5)
		{
			currentAnim = xa.anim.StandDown;
			mySprite.ShowFrame(1);
		}
		
		//go upleft
		if(xa.isUpLeft && currentAnim != xa.anim.WalkUpLeft)
		{
			currentAnim = xa.anim.WalkUpLeft;
			mySprite.Play("ropeLeft");
			xa.facingDir = 8;
		}
		if(!xa.isUpLeft && currentAnim != xa.anim.StandUpLeft && xa.facingDir == 8)
		{
			currentAnim = xa.anim.StandUpLeft;
			mySprite.ShowFrame(0);
		}
		
		//go upright
		if(xa.isUpRight && currentAnim != xa.anim.WalkUpRight)
		{
			currentAnim = xa.anim.WalkUpRight;
			mySprite.Play("ropeRight");
			xa.facingDir = 2;
		}
		if(!xa.isUpRight && currentAnim != xa.anim.StandUpRight && xa.facingDir == 2)
		{
			currentAnim = xa.anim.StandUpRight;
			mySprite.ShowFrame(0);
		}
		
		//go downleft
		if(xa.isDownLeft && currentAnim != xa.anim.WalkDownLeft)
		{
			currentAnim = xa.anim.WalkDownLeft;
			mySprite.Play("ropeLeft");
			xa.facingDir = 6;
		}
		if(!xa.isDownLeft && currentAnim != xa.anim.StandDownLeft && xa.facingDir == 6)
		{
			currentAnim = xa.anim.StandDownLeft;
			mySprite.ShowFrame(1);
		}
		
		//go downright
		if(xa.isDownRight && currentAnim != xa.anim.WalkDownRight)
		{
			currentAnim = xa.anim.WalkDownRight;
			mySprite.Play("ropeRight");
			xa.facingDir = 4;
		}
		if(!xa.isDownRight && currentAnim != xa.anim.StandDownRight && xa.facingDir == 4)
		{
			currentAnim = xa.anim.StandDownRight;
			mySprite.ShowFrame(1);
		}
		
		/*
		// run left
		if(xa.isLeft && !xa.onRope && !xa.onLadder && !xa.falling && currentAnim != xa.anim.WalkLeft)
		{
			currentAnim = xa.anim.WalkLeft;
			mySprite.Play("runLeft");
		}
		
		if(!xa.isLeft && !xa.onRope && !xa.falling && currentAnim != xa.anim.StandLeft && xa.facingDir == 1)
		{
			currentAnim = xa.anim.StandLeft;
			mySprite.ShowFrame(13); // stand left
		}
		
		
		// run right
		if(xa.isRight && !xa.onRope && !xa.onLadder && !xa.falling && currentAnim != xa.anim.WalkRight)
		{
			currentAnim = xa.anim.WalkRight;
			mySprite.Play("runRight");
		}
		if(!xa.isRight && !xa.onRope && !xa.falling && currentAnim != xa.anim.StandRight && xa.facingDir == 2)
		{
			currentAnim = xa.anim.StandRight;
			mySprite.ShowFrame(16); // stand left
		}
		
		// climb
		if(xa.isUp && xa.onLadder && currentAnim != xa.anim.Climb)
		{
			currentAnim = xa.anim.Climb;
			mySprite.Play("climb");
		}
		if(!xa.isUp && xa.onLadder && currentAnim != xa.anim.ClimbStop && xa.facingDir == 3)
		{
			currentAnim = xa.anim.ClimbStop;
			mySprite.ShowFrame(1); // climb left
		}
		
		if(xa.isDown && xa.onLadder && currentAnim != xa.anim.Climb)
		{
			currentAnim = xa.anim.Climb;
			mySprite.Play("climb");
		}
		if(!xa.isDown && xa.onLadder && currentAnim != xa.anim.ClimbStop && xa.facingDir == 4)
		{
			currentAnim = xa.anim.ClimbStop;
			mySprite.ShowFrame(1); // climb left
		}
		
		// rope
		if(xa.isLeft && xa.onRope && currentAnim != xa.anim.RopeLeft)
		{
			currentAnim = xa.anim.RopeLeft;
			mySprite.Play("ropeLeft");
		}
		if(!xa.isLeft && xa.onRope && currentAnim != xa.anim.HangLeft && xa.facingDir == 1)
		{
			currentAnim = xa.anim.HangLeft;
			mySprite.ShowFrame(6); // hang left
		}
		
		if(xa.isRight && xa.onRope && currentAnim != xa.anim.RopeRight)
		{
			currentAnim = xa.anim.RopeRight;
			mySprite.Play("ropeRight");
		}
		if(!xa.isRight && xa.onRope && currentAnim != xa.anim.HangRight && xa.facingDir == 2)
		{
			currentAnim = xa.anim.HangRight;
			mySprite.ShowFrame(9); // hang right
		}
		
		// falling
		if(xa.falling && currentAnim != xa.anim.FallLeft && xa.facingDir == 1)
		{
			currentAnim = xa.anim.FallLeft;
			mySprite.ShowFrame(2); // fall left
		}
		if(xa.falling && currentAnim != xa.anim.FallRight && xa.facingDir == 2)
		{
			currentAnim = xa.anim.FallRight;
			mySprite.ShowFrame(3); // fall right
		}
		
		// shooting
		if(xa.shooting && currentAnim != xa.anim.ShootLeft && xa.facingDir == 1)
		{
			currentAnim = xa.anim.ShootLeft;
			mySprite.ShowFrame(10); // shoot left
		}
		if(xa.shooting && currentAnim != xa.anim.ShootRight && xa.facingDir == 2)
		{
			currentAnim = xa.anim.ShootRight;
			mySprite.ShowFrame(11); // shoot right
		}
		*/
	}
}
