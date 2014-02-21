namespace WindowsGame1WithPatterns.Classes.Sprites
{
    interface IColleague<T>
    {
        void SendMessage(IMediator<T> mediator, T message);
        void ReceiveMessage(T message);
    }
}
