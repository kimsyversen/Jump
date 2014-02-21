using System;
using WindowsGame1WithPatterns.Classes.Sprites;
using WindowsGame1WithPatterns.Classes.Sprites.Factories.Player;

namespace WindowsGame1WithPatterns
{
    class ConcreteColleague<T> : IColleague<T>
    {
        //ConcreteColleague - communicates with other Colleagues through its Mediator
        //This will be the sprite classes
        private  string _name;
        private ISprite _sprite;

        public ConcreteColleague(ISprite sprite)
        {
            _sprite = sprite;
        }

        void IColleague<T>.SendMessage(IMediator<T> mediator, T message)
        {
            mediator.DistributeMessage(this, message);
        }

        void IColleague<T>.ReceiveMessage(T message)
        {
            Console.WriteLine(this._name + " received " + message.ToString());
        }
    }
}
