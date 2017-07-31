using System;

namespace Universology.Puple {
    public class Zodiac {

        public enum ZodiacSigns
        {
            Aries, Taurus, Gemini, Cancer, Leo, Virgo, Libra, Scorpio, Sagittarius, Capricorn, Aquarius, Pisces
        }

        public static readonly int[][] DigitsOfZodiac = {
            new []{3},      //Aries
            new []{9,11},   //Taurus
            new []{7},      //Gemini
            new []{2},      //Cancer
            new []{1},      //Leo
            new []{7},      //Virgo
            new []{9},      //Libra
            new []{10,3},   //Scorpio
            new []{5},      //Sagittarius
            new []{8},      //Capricorn
            new []{4},      //Aquarius
            new []{6}       //Pisces

        };

        public readonly (DateTime from, DateTime to)[] _zodiacRanges = {
            (new DateTime(0, 3, 21), new DateTime(0, 4, 20)),   //Aries
            (new DateTime(0, 4, 21), new DateTime(0, 5, 21)),   //Taurus
            (new DateTime(0, 5, 22), new DateTime(0, 6, 21)),   //Gemini
            (new DateTime(0, 3, 22), new DateTime(0, 4, 20)),   
            (new DateTime(0, 4, 21), new DateTime(0, 5, 21)),   
            (new DateTime(0, 4, 21), new DateTime(0, 5, 21)),
            (new DateTime(0, 3, 21), new DateTime(0, 4, 20)),   
            (new DateTime(0, 4, 21), new DateTime(0, 5, 21)),   
            (new DateTime(0, 4, 21), new DateTime(0, 5, 21)),
            (new DateTime(0, 3, 21), new DateTime(0, 4, 20)),   
            (new DateTime(0, 4, 21), new DateTime(0, 5, 21)),   
            (new DateTime(0, 4, 21), new DateTime(0, 5, 21)),

        };

        private ZodiacSigns _zodiac;

        

        public Zodiac(DateTime date) {

            if()
            
        }

    }
}