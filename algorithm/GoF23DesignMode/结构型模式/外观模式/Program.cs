using System;
using System.Collections.Generic;
using System.Text;

namespace algorithm.GoF23DesignMode.结构型模式.外观模式
{
    public class Program
    {
    }

    public class Television
    {
        public void TurnOn()
        {
            Console.WriteLine("Turning on the television");
        }

        public void TurnOff()
        {
            Console.WriteLine("Turning off the television");
        }
    }

    public class SoundSystem
    {
        public void TurnOn()
        {
            Console.WriteLine("Turning on the sound system");
        }

        public void TurnOff()
        {
            Console.WriteLine("Turning off the sound system");
        }
    }

    public class DVDPlayer
    {
        public void TurnOn()
        {
            Console.WriteLine("Turning on the DVD player");
        }

        public void TurnOff()
        {
            Console.WriteLine("Turning off the DVD player");
        }
    }

    public class HomeTheaterFacade
    {
        private Television tv;
        private SoundSystem ss;
        private DVDPlayer dvd;

        public HomeTheaterFacade(Television television, SoundSystem soundSystem, DVDPlayer dvdPlayer)
        {
            tv = television;
            ss = soundSystem;
            dvd = dvdPlayer;
        }

        public void WatchMovie()
        {
            Console.WriteLine("Get ready to watch a movie");
            tv.TurnOn();
            ss.TurnOn();
            dvd.TurnOn();
        }

        public void EndMovie()
        {
            Console.WriteLine("Ending the movie");
            tv.TurnOff();
            ss.TurnOff();
            dvd.TurnOff();
        }
    }
}