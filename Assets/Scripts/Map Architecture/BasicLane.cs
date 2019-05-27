using UnityEngine;

public class BasicLane : Lane {
  [SerializeField]
  private Transform end;

  [SerializeField]
  private Lane next;

  public override Transform End {
    get {
      return end;
    }
  }

  public override Lane Next {
    get {
      return next;
    }

    set {
      next = value;
    }
  }
}