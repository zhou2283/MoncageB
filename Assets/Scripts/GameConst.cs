using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConst : MonoBehaviour
{
	public const int INTERACTION_STATE_INVALID = 0;
	public const int INTERACTION_STATE_VALID = 1;
	public const int INTERACTION_STATE_NEED = 2;
	public const int INTERACTION_STATE_ALREADY_NEED = 3;
	public const int INTERACTION_STATE_FIND = 4;
	public const int INTERACTION_STATE_USE = 5;
	public const int INTERACTION_STATE_ALREADY_USED = 6;

	public const float CAMERA_SNAP_DURATION = 0.5f;
}
