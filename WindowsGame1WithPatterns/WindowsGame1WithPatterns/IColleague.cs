namespace WindowsGame1WithPatterns
{
    interface IColleague<T>
    {
        void SendMessage(IMediator<T> mediator, T message);
        void ReceiveMessage(T message);
    }
}
