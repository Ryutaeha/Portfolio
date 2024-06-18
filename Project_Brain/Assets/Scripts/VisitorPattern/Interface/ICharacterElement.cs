public interface ICharacterElement
{
    #region Methods
    // 이 메서드는 방문자(Visitor) 패턴을 구현할 때 사용됩니다.
    // 방문자 객체를 파라미터로 받아서, 해당 객체가 이 인터페이스를 구현하는 클래스의 내부 상태에 접근할 수 있도록 합니다.
    public void Accept(IVisitor visitor);
    #endregion
}
