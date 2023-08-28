using UnityEngine;

public class EnemyWalk : BaseState
{
    private Vector2 initScale;
    private bool movingLeft;

    public override void EnterState(Enemy enemy, StateMachine stateMachine)
    {
    //   enemy.animator.SetInteger("AnimState", 2);
      initScale = enemy.transform.localScale;
    }

    public override void UpdateState(Enemy enemy, StateMachine stateMachine)
    {
        if(movingLeft) {
            if (enemy.transform.position.x >= enemy.leftEdge.position.x)
            {
                moveInDirection(enemy,-1);
            } else {
                movingLeft = !movingLeft;
                stateMachine.SwitchState(enemy, stateMachine.idleState);
            }
            
        } else {
            if (enemy.transform.position.x <= enemy.rightEdge.position.x) {
                moveInDirection(enemy,1);
            } else {
                movingLeft = !movingLeft;
                stateMachine.SwitchState(enemy, stateMachine.idleState);
            }
        }
    }
    private void moveInDirection(Enemy enemy, int _direction) {
        enemy.transform.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, initScale.y);
        enemy.transform.position = new Vector2(enemy.transform.position.x + Time.deltaTime * enemy.movementSpeed * _direction,
        enemy.transform.position.y);
    }
}
