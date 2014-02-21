using System.Collections.Generic;

namespace WindowsGame1WithPatterns.Classes.Sprites
{
    //ConcreteMediator - implements the Mediator interface and coordinates communication between Colleague objects. 
    // It is aware of all the Colleagues and their purpose with regards to inter communication.
    class ConcreteMediator<T> : IMediator<T>
    {
        private  List<IColleague<T>> _colleagueList = new List<IColleague<T>>();

        List<IColleague<T>> IMediator<T>.ColleagueList
        {
            get { return _colleagueList; }
        }

        void IMediator<T>.Register(IColleague<T> colleague)
        {
            _colleagueList.Add(colleague);
        }

        void IMediator<T>.DistributeMessage(IColleague<T> sender, T message)
        {
            foreach (IColleague<T> c in _colleagueList)
                if (c != sender)    //don't need to send message to sender
                    c.ReceiveMessage(message);
        }
    }
}
