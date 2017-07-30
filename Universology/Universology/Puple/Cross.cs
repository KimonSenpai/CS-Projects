using System.Security.Cryptography;

namespace Universology.Puple {
    public partial class Puple {
        private static readonly int[][] _digitsOfZodiac = {
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

        public enum ZodiacSigns {
            Aries, Taurus, Gemini, Cancer, Leo, Virgo, Libra, Scorpio, Sagittarius, Capricorn, Aquarius, Pisces
        }
        
        public class Cross {
            
        }
    }
}