using System;
using Universology.Properties;

namespace Universology.Puple {
    public class Zodiac {

        public enum ZodiacSigns
        {
            Aries, Taurus, Gemini, Cancer, Leo, Virgo, Libra, Scorpio, Sagittarius, Capricorn, Aquarius, Pisces
        }

        public static readonly int[][] DigitsOfZodiac = {
            new []{3},      //Aries
            new []{9,12},   //Taurus
            new []{7},      //Gemini
            new []{2},      //Cancer
            new []{1},      //Leo
            new []{7,11},      //Virgo
            new []{9},      //Libra
            new []{10,3},   //Scorpio
            new []{5},      //Sagittarius
            new []{8},      //Capricorn
            new []{4},      //Aquarius
            new []{6}       //Pisces

        };

        public readonly (DateTime from, DateTime to)[] ZodiacRanges = {
            (DateTime.Parse("March 21, 0"), DateTime.Parse("April 20, 0")),         //Aries
            (DateTime.Parse("April 21, 0"), DateTime.Parse("May 21, 0")),           //Taurus
            (DateTime.Parse("May 22, 0"), DateTime.Parse("June 21, 0")),            //Gemini
            (DateTime.Parse("June 22, 0"), DateTime.Parse("July 23, 0")),           //Cancer
            (DateTime.Parse("July 24, 0"), DateTime.Parse("August 23, 0")),         //Leo
            (DateTime.Parse("August 24, 0"), DateTime.Parse("September 23, 0")),    //Virgo
            (DateTime.Parse("September 24, 0"), DateTime.Parse("October 23, 0")),   //Libra
            (DateTime.Parse("October 24, 0"), DateTime.Parse("November 22, 0")),    //Scorpio
            (DateTime.Parse("November 23, 0"), DateTime.Parse("December 21, 0")),   //Sagittarius
            (DateTime.Parse("December 22, 0"), DateTime.Parse("January 20, 0")),    //Capricorn
            (DateTime.Parse("January 21, 0"), DateTime.Parse("February 19, 0")),    //Aquarius
            (DateTime.Parse("February 20, 0"), DateTime.Parse("March 20, 0"))       //Pisces
        };

        private ZodiacSigns _zodiac;

        public ZodiacSigns ZodiacSign => _zodiac;

        public Zodiac(DateTime date) {

            for(int i = 0;i < 7;i++)
                if (IsIn(date, ZodiacRanges[i])) {
                    _zodiac = (ZodiacSigns)i;
                    return;
                }
            throw new ArgumentException(Resources.ZodiacDateException, nameof(date));

        }

        private bool IsIn(DateTime date, (DateTime from, DateTime to) range) {
            int year = 0;
            
            range.from = new DateTime(year, range.from.Month, range.from.Day);
            range.to = new DateTime(range.from.Month > range.to.Month? year+1 : year, range.to.Month, range.to.Day);

            DateTime 
                date0 = new DateTime(year, date.Month, date.Day),
                date1 = new DateTime(year + 1, date.Month, date.Day);


            return (range.from <= date0 && date0 <= range.to || range.from <= date1 && date1 <= range.to);
        }

    }
}