using UnityEngine;
using System.Collections;

public enum ActionEventType: int{Started, Completed}

public interface ActionCallBack1 {

	void ActionEvent(Action source, ActionEventType events = ActionEventType.Completed, int intParam = 0, string strParam = null, Object objectParam = null);
}
