public class Boss : Enemy
{
    protected override void OnStateEntered_Dead()
    {
        base.OnStateEntered_Dead();

        //TODO 아이템 상자 스폰 추가
        //Main.ObjectManager.Spawn<아이템 상자>("", this.transform.position);
    }
}
