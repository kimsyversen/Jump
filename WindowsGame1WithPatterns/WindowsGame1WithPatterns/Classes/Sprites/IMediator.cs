using System.Collections.Generic;

namespace WindowsGame1WithPatterns
{
    //Mediator - defines the interface for communication between Colleague objects
    interface IMediator<T>
    {
        List<IColleague<T>> ColleagueList { get; }
        void DistributeMessage(IColleague<T> sender, T message);
        void Register(IColleague<T> colleague);
    }
}
