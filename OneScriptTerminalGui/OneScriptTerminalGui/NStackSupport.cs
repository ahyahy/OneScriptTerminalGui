﻿//=======================================================================
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Linq;
using NStack;

namespace NStack
{
    public partial class Unicode
    {
        /// <summary>
        /// Special casing rules for Turkish.
        /// </summary>
        public static SpecialCase TurkishCase = new SpecialCase(
            new CaseRange[] {
                new CaseRange (0x0049, 0x0049, 0, 0x131 - 0x49, 0),
                new CaseRange (0x0069, 0x0069, 0x130 - 0x69, 0, 0x130 - 0x69),
                new CaseRange (0x0130, 0x0130, 0, 0x69 - 0x130, 0),
                new CaseRange (0x0131, 0x0131, 0x49 - 0x131, 0, 0x49 - 0x131),
            });
    }
    //=======================================================================
    public partial class Unicode
    {
        /// <summary>
        /// IsDigit reports whether the rune is a decimal digit.
        /// </summary>
        /// <returns><c>true</c>, if the rune is a mark, <c>false</c> otherwise.</returns>
        /// <param name="rune">The rune to test for.</param>
        public static bool IsDigit(uint rune)
        {
            if (rune < MaxLatin1)
                return '0' <= rune && rune <= '9';
            return Category.Digit.IsExcludingLatin(rune);
        }
    }
    //=======================================================================
    public partial class Unicode
    {
        [Flags]
        internal enum CharClass : byte
        {
            pC = 1 << 0, // a control character.
            pP = 1 << 1, // a punctuation character.
            pN = 1 << 2, // a numeral.
            pS = 1 << 3, // a symbolic character.
            pZ = 1 << 4, // a spacing character.
            pLu = 1 << 5, // an upper-case letter.
            pLl = 1 << 6, // a lower-case letter.
            pp = 1 << 7, // a printable character according to Go's definition.
            pg = pp | pZ,   // a graphical character according to the Unicode definition.
            pLo = pLl | pLu, // a letter that is neither upper nor lower case.
            pLmask = pLo
        }

        /// <summary>
        /// The range tables for graphics
        /// </summary>
        public static RangeTable[] GraphicRanges = new[] {
            Category._L, Category._M, Category._N, Category._P, Category._S, Category._Zs
        };

        /// <summary>
        /// The range tables for print
        /// </summary>
        public static RangeTable[] PrintRanges = new[] {
            Category._L, Category._M, Category._N, Category._P, Category._S
        };

        /// <summary>
        /// Determines if a rune is on a set of ranges.
        /// </summary>
        /// <returns><c>true</c>, if rune in ranges was used, <c>false</c> otherwise.</returns>
        /// <param name="rune">Rune.</param>
        /// <param name="inRanges">In ranges.</param>
        public static bool IsRuneInRanges(uint rune, params RangeTable[] inRanges)
        {
            foreach (var range in inRanges)
                if (range.InRange(rune))
                    return true;
            return false;
        }

        /// <summary>
        /// IsGraphic reports whether the rune is defined as a Graphic by Unicode.
        /// </summary>
        /// <returns><c>true</c>, if the rune is a lower case letter, <c>false</c> otherwise.</returns>
        /// <param name="rune">The rune to test for.</param>
        /// <remarks>
        /// Such characters include letters, marks, numbers, punctuation, symbols, and
        /// spaces, from categories L, M, N, P, S, Zs.
        /// </remarks>
        public static bool IsGraphic(uint rune)
        {
            if (rune < MaxLatin1)
                return (properties[rune] & CharClass.pg) != 0;
            return IsRuneInRanges(rune, GraphicRanges);
        }

        /// <summary>
        /// IsPrint reports whether the rune is defined as printable.
        /// </summary>
        /// <returns><c>true</c>, if the rune is a lower case letter, <c>false</c> otherwise.</returns>
        /// <param name="rune">The rune to test for.</param>
        /// <remarks>
        /// Such characters include letters, marks, numbers, punctuation, symbols, and the
        /// ASCII space character, from categories L, M, N, P, S and the ASCII space
        /// character. This categorization is the same as IsGraphic except that the
        /// only spacing character is ASCII space, U+0020.
        /// </remarks>
        public static bool IsPrint(uint rune)
        {
            if (rune < MaxLatin1)
                return (properties[rune] & CharClass.pp) != 0;
            return IsRuneInRanges(rune, PrintRanges);
        }

        /// <summary>
        /// IsControl reports whether the rune is a control character.
        /// </summary>
        /// <returns><c>true</c>, if the rune is a lower case letter, <c>false</c> otherwise.</returns>
        /// <param name="rune">The rune to test for.</param>
        /// <remarks>
        /// The C (Other) Unicode category includes more code points such as surrogates; use C.InRange (r) to test for them.
        /// </remarks>
        public static bool IsControl(uint rune)
        {
            if (rune < MaxLatin1)
                return (properties[rune] & CharClass.pC) != 0;
            // All control characters are < MaxLatin1.
            return false;
        }

        /// <summary>
        /// IsLetter reports whether the rune is a letter (category L).
        /// </summary>
        /// <returns><c>true</c>, if the rune is a letter, <c>false</c> otherwise.</returns>
        /// <param name="rune">The rune to test for.</param>
        /// <remarks>
        /// </remarks>
        public static bool IsLetter(uint rune)
        {
            if (rune < MaxLatin1)
                return (properties[rune] & CharClass.pLmask) != 0;
            return Category.L.IsExcludingLatin(rune);
        }

        /// <summary>
        /// IsMark reports whether the rune is a letter (category M).
        /// </summary>
        /// <returns><c>true</c>, if the rune is a mark, <c>false</c> otherwise.</returns>
        /// <param name="rune">The rune to test for.</param>
        /// <remarks>
        /// Reports whether the rune is a mark character (category M).
        /// </remarks>
        public static bool IsMark(uint rune)
        {
            // There are no mark characters in Latin-1.
            return Category.M.IsExcludingLatin(rune);
        }

        /// <summary>
        /// IsNumber reports whether the rune is a letter (category N).
        /// </summary>
        /// <returns><c>true</c>, if the rune is a mark, <c>false</c> otherwise.</returns>
        /// <param name="rune">The rune to test for.</param>
        /// <remarks>
        /// Reports whether the rune is a mark character (category N).
        /// </remarks>
        public static bool IsNumber(uint rune)
        {
            if (rune < MaxLatin1)
                return (properties[rune] & CharClass.pN) != 0;
            return Category.N.IsExcludingLatin(rune);
        }

        /// <summary>
        /// IsPunct reports whether the rune is a letter (category P).
        /// </summary>
        /// <returns><c>true</c>, if the rune is a mark, <c>false</c> otherwise.</returns>
        /// <param name="rune">The rune to test for.</param>
        /// <remarks>
        /// Reports whether the rune is a mark character (category P).
        /// </remarks>
        public static bool IsPunct(uint rune)
        {
            if (rune < MaxLatin1)
                return (properties[rune] & CharClass.pP) != 0;
            return Category.P.IsExcludingLatin(rune);
        }

        /// <summary>
        /// IsSpace reports whether the rune is a space character as defined by Unicode's White Space property.
        /// </summary>
        /// <returns><c>true</c>, if the rune is a mark, <c>false</c> otherwise.</returns>
        /// <param name="rune">The rune to test for.</param>
        /// <remarks>
        /// In the Latin-1 space, white space includes '\t', '\n', '\v', '\f', '\r', ' ', 
        /// U+0085 (NEL), U+00A0 (NBSP).
        /// Other definitions of spacing characters are set by category  Z and property Pattern_White_Space.
        /// </remarks>
        public static bool IsSpace(uint rune)
        {
            if (rune < MaxLatin1)
            {
                if (rune == '\t' || rune == '\n' || rune == '\v' || rune == '\f' || rune == '\r' || rune == ' ' || rune == 0x85 || rune == 0xa0)
                    return true;
                return false;
            }
            return Property.White_Space.IsExcludingLatin(rune);
        }

        /// <summary>
        /// IsSymbol reports whether the rune is a symbolic character.
        /// </summary>
        /// <returns><c>true</c>, if the rune is a mark, <c>false</c> otherwise.</returns>
        /// <param name="rune">The rune to test for.</param>
        public static bool IsSymbol(uint rune)
        {
            if (rune < MaxLatin1)
                return (properties[rune] & CharClass.pS) != 0;
            return Category.S.IsExcludingLatin(rune);
        }


    }
    //=======================================================================
    /// <summary>
    /// Unicode class contains helper methods to support Unicode encoding.
    /// </summary>
    /// <remarks>
    /// <para>
    ///    Generally the Unicode class provided methods that can help you classify and
    ///    convert Unicode code points.  The word codepoint is considered a mouthful so in
    ///    this class, the word "rune" is used instead and is represented by the
    ///    uint value type.  
    /// </para>
    /// <para>
    ///    Unicode code points can be produced by combining independent characters,
    ///    so the rune for a character can be produced by combining one character and
    ///    other elements of it.  Runes on the other hand correspond to a specific
    ///    character.
    /// </para>
    /// <para>
    ///    This class surfaces various methods to classify case of a Rune, like
    ///    <see cref="M:NStack.Unicode.IsUpper"/>, <see cref="M:NStack.Unicode.IsLower"/>, <see cref="M:NStack.Unicode.IsDigit"/>,
    ///    <see cref="M:NStack.Unicode.IsGraphic"/> to convert runes from one case to another using the <see cref="M:NStack.Unicode.ToUpper"/>,
    ///    <see cref="M:NStack.Unicode.ToLower"/>, <see cref="M:NStack.Unicode.ToTitle"/> as well as various constants
    ///    that are useful when working with Unicode runes.
    /// </para>  
    /// <para>
    ///    Unicode defines various character classes which are surfaced as RangeTables
    ///    as static properties in this class.   You can probe whether a rune belongs
    ///    to a specific range table
    /// </para>
    /// </remarks>
    public partial class Unicode
    {
        /// <summary>
        /// Maximum valid Unicode code point.
        /// </summary> 
        public const int MaxRune = 0x0010FFFF;

        /// <summary>
        /// Represents invalid code points.
        /// </summary>
        public const uint ReplacementChar = 0xfffd;     // 

        /// <summary>
        /// The maximum ASCII value.
        /// </summary>
        public const uint MaxAscii = 0x7f;

        /// <summary>
        /// The maximum latin1 value.
        /// </summary>
        public const uint MaxLatin1 = 0xff;

        // Range16 represents of a range of 16-bit Unicode code points. The range runs from Lo to Hi
        // inclusive and has the specified stride.
        internal struct Range16
        {
            public ushort Lo, Hi, Stride;

            public Range16(ushort lo, ushort hi, ushort stride)
            {
                Lo = lo;
                Hi = hi;
                Stride = stride;
            }
        }

        // Range32 represents of a range of Unicode code points and is used when one or
        // more of the values will not fit in 16 bits. The range runs from Lo to Hi
        // inclusive and has the specified stride. Lo and Hi must always be >= 1<<16.
        internal struct Range32
        {
            public int Lo, Hi, Stride;

            public Range32(int lo, int hi, int stride)
            {
                Lo = lo;
                Hi = hi;
                Stride = stride;
            }

        }

        /// <summary>
        /// Range tables describe classes of unicode code points.
        /// </summary>
        /// 
        // RangeTable defines a set of Unicode code points by listing the ranges of
        // code points within the set. The ranges are listed in two slices
        // to save space: a slice of 16-bit ranges and a slice of 32-bit ranges.
        // The two slices must be in sorted order and non-overlapping.
        // Also, R32 should contain only values >= 0x10000 (1<<16).
        public struct RangeTable
        {
            readonly Range16[] R16;
            readonly Range32[] R32;

            /// <summary>
            /// The number of entries in the short range table (R16) with Hi being less than MaxLatin1
            /// </summary>
            public readonly int LatinOffset;

            internal RangeTable(Range16[] r16 = null, Range32[] r32 = null, int latinOffset = 0)
            {
                R16 = r16;
                R32 = r32;
                LatinOffset = latinOffset;
            }

            /// <summary>
            /// Used to determine if a given rune is in the range of this RangeTable.
            /// </summary>
            /// <returns><c>true</c>, if the rune is in this RangeTable, <c>false</c> otherwise.</returns>
            /// <param name="rune">Rune.</param>
            public bool InRange(uint rune)
            {
                if (R16 != null)
                {
                    var r16l = R16.Length;

                    if (rune <= R16[r16l - 1].Hi)
                        return Is16(R16, (ushort)rune);
                }
                if (R32 != null)
                {
                    var r32l = R32.Length;
                    if (rune >= R32[0].Lo)
                        return Is32(R32, rune);
                }
                return false;
            }

            /// <summary>
            /// Used to determine if a given rune is in the range of this RangeTable, excluding latin1 characters.
            /// </summary>
            /// <returns><c>true</c>, if the rune is part of the range (not including latin), <c>false</c> otherwise.</returns>
            /// <param name="rune">Rune.</param>
            public bool IsExcludingLatin(uint rune)
            {
                var off = LatinOffset;

                if (R16 != null)
                {
                    var r16l = R16.Length;

                    if (r16l > off && rune <= R16[r16l - 1].Hi)
                        return Is16(R16, (ushort)rune, off);
                }
                if (R32 != null)
                {
                    if (R32.Length > 0 && rune >= R32[0].Lo)
                        return Is32(R32, rune);
                }
                return false;
            }
        }

        // CaseRange represents a range of Unicode code points for simple (one
        // code point to one code point) case conversion.
        // The range runs from Lo to Hi inclusive, with a fixed stride of 1.  Deltas
        // are the number to add to the code point to reach the code point for a
        // different case for that character. They may be negative. If zero, it
        // means the character is in the corresponding case. There is a special
        // case representing sequences of alternating corresponding Upper and Lower
        // pairs. It appears with a fixed Delta of
        //      {UpperLower, UpperLower, UpperLower}
        // The constant UpperLower has an otherwise impossible delta value.
        internal struct CaseRange
        {
            public int Lo, Hi;
            public unsafe fixed int Delta[3];

            public CaseRange(int lo, int hi, int d1, int d2, int d3)
            {
                Lo = lo;
                Hi = hi;
                unsafe
                {
                    fixed (int* p = Delta)
                    {
                        p[0] = d1;
                        p[1] = d2;
                        p[2] = d3;
                    }
                }
            }
        }

        /// <summary>
        /// The types of cases supported.
        /// </summary>
        public enum Case
        {
            /// <summary>
            /// Upper case
            /// </summary>
            Upper = 0,

            /// <summary>
            /// Lower case
            /// </summary>
            Lower = 1,

            /// <summary>
            /// Titlecase capitalizes the first letter, and keeps the rest in lowercase.
            /// Sometimes it is not as straight forward as the uppercase, some characters require special handling, like
            /// certain ligatures and greek characters.
            /// </summary>
            Title = 2
        };

        // If the Delta field of a CaseRange is UpperLower, it means
        // this CaseRange represents a sequence of the form (say)
        // Upper Lower Upper Lower.
        const int UpperLower = MaxRune + 1;

        // linearMax is the maximum size table for linear search for non-Latin1 rune.
        const int linearMax = 18;

        static bool Is16(Range16[] ranges, ushort r, int lo = 0)
        {
            if (ranges.Length - lo < linearMax || r <= MaxLatin1)
            {
                for (int i = lo; i < ranges.Length; i++)
                {
                    var range = ranges[i];

                    if (r < range.Lo)
                        return false;
                    if (r <= range.Hi)
                        return (r - range.Lo) % range.Stride == 0;
                }
                return false;
            }
            var hi = ranges.Length;
            // binary search over ranges
            while (lo < hi)
            {
                var m = lo + (hi - lo) / 2;
                var range = ranges[m];
                if (range.Lo <= r && r <= range.Hi)
                    return (r - range.Lo) % range.Stride == 0;
                if (r < range.Lo)
                    hi = m;
                else
                    lo = m + 1;
            }
            return false;
        }

        static bool Is32(Range32[] ranges, uint r)
        {
            var hi = ranges.Length;
            if (hi < linearMax || r <= MaxLatin1)
            {
                foreach (var range in ranges)
                {
                    if (r < range.Lo)
                        return false;
                    if (r <= range.Hi)
                        return (r - range.Lo) % range.Stride == 0;
                }
                return false;
            }
            // binary search over ranges
            var lo = 0;
            while (lo < hi)
            {
                var m = lo + (hi - lo) / 2;
                var range = ranges[m];
                if (range.Lo <= r && r <= range.Hi)
                    return (r - range.Lo) % range.Stride == 0;
                if (r < range.Lo)
                    hi = m;
                else
                    lo = m + 1;
            }
            return false;
        }

        /// <summary>
        /// Reports whether the rune is an upper case letter.
        /// </summary>
        /// <returns><c>true</c>, if the rune is an upper case lette, <c>false</c> otherwise.</returns>
        /// <param name="rune">The rune to test for.</param>
        public static bool IsUpper(uint rune)
        {
            if (rune <= MaxLatin1)
                return (properties[(byte)rune] & CharClass.pLmask) == CharClass.pLu;
            return Category.Upper.IsExcludingLatin(rune);
        }

        /// <summary>
        /// Reports whether the rune is a lower case letter.
        /// </summary>
        /// <returns><c>true</c>, if the rune is a lower case lette, <c>false</c> otherwise.</returns>
        /// <param name="rune">The rune to test for.</param>
        public static bool IsLower(uint rune)
        {
            if (rune <= MaxLatin1)
                return (properties[(byte)rune] & CharClass.pLmask) == CharClass.pLl;
            return Category.Lower.IsExcludingLatin(rune);
        }

        /// <summary>
        /// Reports whether the rune is a title case letter.
        /// </summary>
        /// <returns><c>true</c>, if the rune is a lower case lette, <c>false</c> otherwise.</returns>
        /// <param name="rune">The rune to test for.</param>
        public static bool IsTitle(uint rune)
        {
            if (rune <= MaxLatin1)
                return false;
            return Category.Title.IsExcludingLatin(rune);
        }

        // to maps the rune using the specified case mapping.
        static unsafe uint to(Case toCase, uint rune, CaseRange[] caseRange)
        {
            if (toCase < 0 || toCase > Case.Title)
                return ReplacementChar;

            // binary search over ranges
            var lo = 0;
            var hi = caseRange.Length;

            while (lo < hi)
            {
                var m = lo + (hi - lo) / 2;
                var cr = caseRange[m];
                if (cr.Lo <= rune && rune <= cr.Hi)
                {
                    var delta = cr.Delta[(int)toCase];
                    if (delta > MaxRune)
                    {
                        // In an Upper-Lower sequence, which always starts with
                        // an UpperCase letter, the real deltas always look like:
                        //      {0, 1, 0}    UpperCase (Lower is next)
                        //      {-1, 0, -1}  LowerCase (Upper, Title are previous)
                        // The characters at even offsets from the beginning of the
                        // sequence are upper case; the ones at odd offsets are lower.
                        // The correct mapping can be done by clearing or setting the low
                        // bit in the sequence offset.
                        // The constants UpperCase and TitleCase are even while LowerCase
                        // is odd so we take the low bit from _case.

                        return ((uint)cr.Lo) + (((rune - ((uint)(cr.Lo))) & 0xfffffffe) | ((uint)((uint)toCase) & 1));
                    }
                    return (uint)((int)rune + delta);
                }
                if (rune < cr.Lo)
                    hi = m;
                else
                    lo = m + 1;
            }
            return rune;
        }

        // To maps the rune to the specified case: Case.Upper, Case.Lower, or Case.Title
        /// <summary>
        /// To maps the rune to the specified case: Case.Upper, Case.Lower, or Case.Title
        /// </summary>
        /// <returns>The cased character.</returns>
        /// <param name="toCase">The destination case.</param>
        /// <param name="rune">Rune to convert.</param>
        public static uint To(Case toCase, uint rune)
        {
            return to(toCase, rune, CaseRanges);
        }

        /// <summary>
        /// ToUpper maps the rune to upper case.
        /// </summary>
        /// <returns>The upper cased rune if it can be.</returns>
        /// <param name="rune">Rune.</param>
        public static uint ToUpper(uint rune)
        {
            if (rune <= MaxAscii)
            {
                if ('a' <= rune && rune <= 'z')
                    rune -= 'a' - 'A';
                return rune;
            }
            return To(Case.Upper, rune);
        }

        /// <summary>
        /// ToLower maps the rune to lower case.
        /// </summary>
        /// <returns>The lower cased rune if it can be.</returns>
        /// <param name="rune">Rune.</param>
        public static uint ToLower(uint rune)
        {
            if (rune <= MaxAscii)
            {
                if ('A' <= rune && rune <= 'Z')
                    rune += 'a' - 'A';
                return rune;
            }
            return To(Case.Lower, rune);
        }

        /// <summary>
        /// ToLower maps the rune to title case.
        /// </summary>
        /// <returns>The lower cased rune if it can be.</returns>
        /// <param name="rune">Rune.</param>
        public static uint ToTitle(uint rune)
        {
            if (rune <= MaxAscii)
            {
                if ('a' <= rune && rune <= 'z')
                    rune -= 'a' - 'A';
                return rune;
            }
            return To(Case.Title, rune);
        }

        /// <summary>
        /// SpecialCase represents language-specific case mappings such as Turkish.
        /// </summary>
        /// <remarks>
        /// Methods of SpecialCase customize (by overriding) the standard mappings.
        /// </remarks>
        public struct SpecialCase
        {
            Unicode.CaseRange[] Special;
            internal SpecialCase(CaseRange[] special)
            {
                Special = special;
            }

            /// <summary>
            /// ToUpper maps the rune to upper case giving priority to the special mapping.
            /// </summary>
            /// <returns>The upper cased rune if it can be.</returns>
            /// <param name="rune">Rune.</param>
            public uint ToUpper(uint rune)
            {
                var result = to(Case.Upper, rune, Special);
                if (result == rune)
                    result = Unicode.ToUpper(rune);
                return result;
            }

            /// <summary>
            /// ToTitle maps the rune to title case giving priority to the special mapping.
            /// </summary>
            /// <returns>The title cased rune if it can be.</returns>
            /// <param name="rune">Rune.</param>
            public uint ToTitle(uint rune)
            {
                var result = to(Case.Title, rune, Special);
                if (result == rune)
                    result = Unicode.ToTitle(rune);
                return result;
            }

            /// <summary>
            /// ToLower maps the rune to lower case giving priority to the special mapping.
            /// </summary>
            /// <returns>The lower cased rune if it can be.</returns>
            /// <param name="rune">Rune.</param>
            public uint ToLower(uint rune)
            {
                var result = to(Case.Lower, rune, Special);
                if (result == rune)
                    result = Unicode.ToLower(rune);
                return result;
            }
        }

        // CaseOrbit is defined in tables.cs as foldPair []. Right now all the
        // entries fit in ushort, so use ushort.  If that changes, compilation
        // will fail (the constants in the composite literal will not fit in ushort)
        // and the types here can change to uint.
        struct FoldPair
        {
            public ushort From, To;

            public FoldPair(ushort from, ushort to)
            {
                From = from;
                To = to;
            }
        }

        /// <summary>
        /// SimpleFold iterates over Unicode code points equivalent under
        /// the Unicode-defined simple case folding.
        /// </summary>
        /// <returns>The simple-case folded rune.</returns>
        /// <param name="rune">Rune.</param>
        /// <remarks>
        /// SimpleFold iterates over Unicode code points equivalent under
        /// the Unicode-defined simple case folding. Among the code points
        /// equivalent to rune (including rune itself), SimpleFold returns the
        /// smallest rune > r if one exists, or else the smallest rune >= 0.
        /// If r is not a valid Unicode code point, SimpleFold(r) returns r.
        ///
        /// For example:
        /// <code>
        ///      SimpleFold('A') = 'a'
        ///      SimpleFold('a') = 'A'
        ///
        ///      SimpleFold('K') = 'k'
        ///      SimpleFold('k') = '\u212A' (Kelvin symbol, K)
        ///      SimpleFold('\u212A') = 'K'
        ///
        ///      SimpleFold('1') = '1'
        ///
        ///      SimpleFold(-2) = -2
        /// </code>
        /// </remarks>
        public static uint SimpleFold(uint rune)
        {
            if (rune >= MaxRune)
                return rune;
            if (rune < asciiFold.Length)
                return (uint)asciiFold[rune];
            // Consult caseOrbit table for special cases.
            var lo = 0;
            var hi = CaseOrbit.Length;
            while (lo < hi)
            {
                var m = lo + (hi - lo) / 2;
                if (CaseOrbit[m].From < rune)
                    lo = m + 1;
                else
                    hi = m;
            }
            if (lo < CaseOrbit.Length && CaseOrbit[lo].From == rune)
                return CaseOrbit[lo].To;
            // No folding specified. This is a one- or two-element
            // equivalence class containing rune and ToLower(rune)
            // and ToUpper(rune) if they are different from rune.
            var l = ToLower(rune);
            if (l != rune)
                return l;
            return ToUpper(rune);
        }
    }
    //=======================================================================
    // Copyright 2013 The Go Authors, Microsoft. All rights reserved.
    // Use of this source code is governed by a BSD-style
    // license that can be found in the LICENSE file.

    // Generated by running
    //	maketables --tables=all --data=https://www.unicode.org/Public/15.0.0/ucd/UnicodeData.txt --casefolding=https://www.unicode.org/Public/15.0.0/ucd/CaseFolding.txt
    // DO NOT EDIT

    public partial class Unicode
    {
        /// <summary>
        /// Version is the Unicode edition from which the tables are derived.
        /// </summary>
        public const string Version = "15.0.0";

        /// <summary>Static class containing the various Unicode category range tables</summary>
        /// <remarks><para>There are static properties that can be used to fetch a specific category, or you can use the <see cref="M:NStack.Unicode.Category.Get"/> method this class to retrieve the RangeTable by its Unicode category table name</para></remarks>
        public static class Category
        {
            /// <summary>Retrieves the specified RangeTable from the Unicode category name</summary>
            /// <param name="categoryName">The unicode character category name</param>
            public static RangeTable Get(string categoryName) => Categories[categoryName];
            // Categories is the set of Unicode category tables.
            static Dictionary<string, RangeTable> Categories = new Dictionary<string, RangeTable>() {
            { "C", C },
            { "Cc", Cc },
            { "Cf", Cf },
            { "Co", Co },
            { "Cs", Cs },
            { "L", L },
            { "Ll", Ll },
            { "Lm", Lm },
            { "Lo", Lo },
            { "Lt", Lt },
            { "Lu", Lu },
            { "M", M },
            { "Mc", Mc },
            { "Me", Me },
            { "Mn", Mn },
            { "N", N },
            { "Nd", Nd },
            { "Nl", Nl },
            { "No", No },
            { "P", P },
            { "Pc", Pc },
            { "Pd", Pd },
            { "Pe", Pe },
            { "Pf", Pf },
            { "Pi", Pi },
            { "Po", Po },
            { "Ps", Ps },
            { "S", S },
            { "Sc", Sc },
            { "Sk", Sk },
            { "Sm", Sm },
            { "So", So },
            { "Z", Z },
            { "Zl", Zl },
            { "Zp", Zp },
            { "Zs", Zs },
        };

            internal static RangeTable _C = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0000, 0x001f, 1),
            new Range16 (0x007f, 0x009f, 1),
            new Range16 (0x00ad, 0x0600, 1363),
            new Range16 (0x0601, 0x0605, 1),
            new Range16 (0x061c, 0x06dd, 193),
            new Range16 (0x070f, 0x0890, 385),
            new Range16 (0x0891, 0x08e2, 81),
            new Range16 (0x180e, 0x200b, 2045),
            new Range16 (0x200c, 0x200f, 1),
            new Range16 (0x202a, 0x202e, 1),
            new Range16 (0x2060, 0x2064, 1),
            new Range16 (0x2066, 0x206f, 1),
            new Range16 (0xd800, 0xf8ff, 1),
            new Range16 (0xfeff, 0xfff9, 250),
            new Range16 (0xfffa, 0xfffb, 1),
                },
                r32: new Range32[] {
            new Range32 (0x110bd, 0x110cd, 16),
            new Range32 (0x13430, 0x1343f, 1),
            new Range32 (0x1bca0, 0x1bca3, 1),
            new Range32 (0x1d173, 0x1d17a, 1),
            new Range32 (0xe0001, 0xe0020, 31),
            new Range32 (0xe0021, 0xe007f, 1),
            new Range32 (0xf0000, 0xffffd, 1),
            new Range32 (0x100000, 0x10fffd, 1),
                },
                latinOffset: 2
            );

            internal static RangeTable _Cc = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0000, 0x001f, 1),
            new Range16 (0x007f, 0x009f, 1),
                },
                latinOffset: 2
            );

            internal static RangeTable _Cf = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x00ad, 0x0600, 1363),
            new Range16 (0x0601, 0x0605, 1),
            new Range16 (0x061c, 0x06dd, 193),
            new Range16 (0x070f, 0x0890, 385),
            new Range16 (0x0891, 0x08e2, 81),
            new Range16 (0x180e, 0x200b, 2045),
            new Range16 (0x200c, 0x200f, 1),
            new Range16 (0x202a, 0x202e, 1),
            new Range16 (0x2060, 0x2064, 1),
            new Range16 (0x2066, 0x206f, 1),
            new Range16 (0xfeff, 0xfff9, 250),
            new Range16 (0xfffa, 0xfffb, 1),
                },
                r32: new Range32[] {
            new Range32 (0x110bd, 0x110cd, 16),
            new Range32 (0x13430, 0x1343f, 1),
            new Range32 (0x1bca0, 0x1bca3, 1),
            new Range32 (0x1d173, 0x1d17a, 1),
            new Range32 (0xe0001, 0xe0020, 31),
            new Range32 (0xe0021, 0xe007f, 1),
                }
            );

            internal static RangeTable _Co = new RangeTable(
                r16: new Range16[] {
            new Range16 (0xe000, 0xf8ff, 1),
                },
                r32: new Range32[] {
            new Range32 (0xf0000, 0xffffd, 1),
            new Range32 (0x100000, 0x10fffd, 1),
                }
            );

            internal static RangeTable _Cs = new RangeTable(
                r16: new Range16[] {
            new Range16 (0xd800, 0xdfff, 1),
                }
            );

            internal static RangeTable _L = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0041, 0x005a, 1),
            new Range16 (0x0061, 0x007a, 1),
            new Range16 (0x00aa, 0x00b5, 11),
            new Range16 (0x00ba, 0x00c0, 6),
            new Range16 (0x00c1, 0x00d6, 1),
            new Range16 (0x00d8, 0x00f6, 1),
            new Range16 (0x00f8, 0x02c1, 1),
            new Range16 (0x02c6, 0x02d1, 1),
            new Range16 (0x02e0, 0x02e4, 1),
            new Range16 (0x02ec, 0x02ee, 2),
            new Range16 (0x0370, 0x0374, 1),
            new Range16 (0x0376, 0x0377, 1),
            new Range16 (0x037a, 0x037d, 1),
            new Range16 (0x037f, 0x0386, 7),
            new Range16 (0x0388, 0x038a, 1),
            new Range16 (0x038c, 0x038e, 2),
            new Range16 (0x038f, 0x03a1, 1),
            new Range16 (0x03a3, 0x03f5, 1),
            new Range16 (0x03f7, 0x0481, 1),
            new Range16 (0x048a, 0x052f, 1),
            new Range16 (0x0531, 0x0556, 1),
            new Range16 (0x0559, 0x0560, 7),
            new Range16 (0x0561, 0x0588, 1),
            new Range16 (0x05d0, 0x05ea, 1),
            new Range16 (0x05ef, 0x05f2, 1),
            new Range16 (0x0620, 0x064a, 1),
            new Range16 (0x066e, 0x066f, 1),
            new Range16 (0x0671, 0x06d3, 1),
            new Range16 (0x06d5, 0x06e5, 16),
            new Range16 (0x06e6, 0x06ee, 8),
            new Range16 (0x06ef, 0x06fa, 11),
            new Range16 (0x06fb, 0x06fc, 1),
            new Range16 (0x06ff, 0x0710, 17),
            new Range16 (0x0712, 0x072f, 1),
            new Range16 (0x074d, 0x07a5, 1),
            new Range16 (0x07b1, 0x07ca, 25),
            new Range16 (0x07cb, 0x07ea, 1),
            new Range16 (0x07f4, 0x07f5, 1),
            new Range16 (0x07fa, 0x0800, 6),
            new Range16 (0x0801, 0x0815, 1),
            new Range16 (0x081a, 0x0824, 10),
            new Range16 (0x0828, 0x0840, 24),
            new Range16 (0x0841, 0x0858, 1),
            new Range16 (0x0860, 0x086a, 1),
            new Range16 (0x0870, 0x0887, 1),
            new Range16 (0x0889, 0x088e, 1),
            new Range16 (0x08a0, 0x08c9, 1),
            new Range16 (0x0904, 0x0939, 1),
            new Range16 (0x093d, 0x0950, 19),
            new Range16 (0x0958, 0x0961, 1),
            new Range16 (0x0971, 0x0980, 1),
            new Range16 (0x0985, 0x098c, 1),
            new Range16 (0x098f, 0x0990, 1),
            new Range16 (0x0993, 0x09a8, 1),
            new Range16 (0x09aa, 0x09b0, 1),
            new Range16 (0x09b2, 0x09b6, 4),
            new Range16 (0x09b7, 0x09b9, 1),
            new Range16 (0x09bd, 0x09ce, 17),
            new Range16 (0x09dc, 0x09dd, 1),
            new Range16 (0x09df, 0x09e1, 1),
            new Range16 (0x09f0, 0x09f1, 1),
            new Range16 (0x09fc, 0x0a05, 9),
            new Range16 (0x0a06, 0x0a0a, 1),
            new Range16 (0x0a0f, 0x0a10, 1),
            new Range16 (0x0a13, 0x0a28, 1),
            new Range16 (0x0a2a, 0x0a30, 1),
            new Range16 (0x0a32, 0x0a33, 1),
            new Range16 (0x0a35, 0x0a36, 1),
            new Range16 (0x0a38, 0x0a39, 1),
            new Range16 (0x0a59, 0x0a5c, 1),
            new Range16 (0x0a5e, 0x0a72, 20),
            new Range16 (0x0a73, 0x0a74, 1),
            new Range16 (0x0a85, 0x0a8d, 1),
            new Range16 (0x0a8f, 0x0a91, 1),
            new Range16 (0x0a93, 0x0aa8, 1),
            new Range16 (0x0aaa, 0x0ab0, 1),
            new Range16 (0x0ab2, 0x0ab3, 1),
            new Range16 (0x0ab5, 0x0ab9, 1),
            new Range16 (0x0abd, 0x0ad0, 19),
            new Range16 (0x0ae0, 0x0ae1, 1),
            new Range16 (0x0af9, 0x0b05, 12),
            new Range16 (0x0b06, 0x0b0c, 1),
            new Range16 (0x0b0f, 0x0b10, 1),
            new Range16 (0x0b13, 0x0b28, 1),
            new Range16 (0x0b2a, 0x0b30, 1),
            new Range16 (0x0b32, 0x0b33, 1),
            new Range16 (0x0b35, 0x0b39, 1),
            new Range16 (0x0b3d, 0x0b5c, 31),
            new Range16 (0x0b5d, 0x0b5f, 2),
            new Range16 (0x0b60, 0x0b61, 1),
            new Range16 (0x0b71, 0x0b83, 18),
            new Range16 (0x0b85, 0x0b8a, 1),
            new Range16 (0x0b8e, 0x0b90, 1),
            new Range16 (0x0b92, 0x0b95, 1),
            new Range16 (0x0b99, 0x0b9a, 1),
            new Range16 (0x0b9c, 0x0b9e, 2),
            new Range16 (0x0b9f, 0x0ba3, 4),
            new Range16 (0x0ba4, 0x0ba8, 4),
            new Range16 (0x0ba9, 0x0baa, 1),
            new Range16 (0x0bae, 0x0bb9, 1),
            new Range16 (0x0bd0, 0x0c05, 53),
            new Range16 (0x0c06, 0x0c0c, 1),
            new Range16 (0x0c0e, 0x0c10, 1),
            new Range16 (0x0c12, 0x0c28, 1),
            new Range16 (0x0c2a, 0x0c39, 1),
            new Range16 (0x0c3d, 0x0c58, 27),
            new Range16 (0x0c59, 0x0c5a, 1),
            new Range16 (0x0c5d, 0x0c60, 3),
            new Range16 (0x0c61, 0x0c80, 31),
            new Range16 (0x0c85, 0x0c8c, 1),
            new Range16 (0x0c8e, 0x0c90, 1),
            new Range16 (0x0c92, 0x0ca8, 1),
            new Range16 (0x0caa, 0x0cb3, 1),
            new Range16 (0x0cb5, 0x0cb9, 1),
            new Range16 (0x0cbd, 0x0cdd, 32),
            new Range16 (0x0cde, 0x0ce0, 2),
            new Range16 (0x0ce1, 0x0cf1, 16),
            new Range16 (0x0cf2, 0x0d04, 18),
            new Range16 (0x0d05, 0x0d0c, 1),
            new Range16 (0x0d0e, 0x0d10, 1),
            new Range16 (0x0d12, 0x0d3a, 1),
            new Range16 (0x0d3d, 0x0d4e, 17),
            new Range16 (0x0d54, 0x0d56, 1),
            new Range16 (0x0d5f, 0x0d61, 1),
            new Range16 (0x0d7a, 0x0d7f, 1),
            new Range16 (0x0d85, 0x0d96, 1),
            new Range16 (0x0d9a, 0x0db1, 1),
            new Range16 (0x0db3, 0x0dbb, 1),
            new Range16 (0x0dbd, 0x0dc0, 3),
            new Range16 (0x0dc1, 0x0dc6, 1),
            new Range16 (0x0e01, 0x0e30, 1),
            new Range16 (0x0e32, 0x0e33, 1),
            new Range16 (0x0e40, 0x0e46, 1),
            new Range16 (0x0e81, 0x0e82, 1),
            new Range16 (0x0e84, 0x0e86, 2),
            new Range16 (0x0e87, 0x0e8a, 1),
            new Range16 (0x0e8c, 0x0ea3, 1),
            new Range16 (0x0ea5, 0x0ea7, 2),
            new Range16 (0x0ea8, 0x0eb0, 1),
            new Range16 (0x0eb2, 0x0eb3, 1),
            new Range16 (0x0ebd, 0x0ec0, 3),
            new Range16 (0x0ec1, 0x0ec4, 1),
            new Range16 (0x0ec6, 0x0edc, 22),
            new Range16 (0x0edd, 0x0edf, 1),
            new Range16 (0x0f00, 0x0f40, 64),
            new Range16 (0x0f41, 0x0f47, 1),
            new Range16 (0x0f49, 0x0f6c, 1),
            new Range16 (0x0f88, 0x0f8c, 1),
            new Range16 (0x1000, 0x102a, 1),
            new Range16 (0x103f, 0x1050, 17),
            new Range16 (0x1051, 0x1055, 1),
            new Range16 (0x105a, 0x105d, 1),
            new Range16 (0x1061, 0x1065, 4),
            new Range16 (0x1066, 0x106e, 8),
            new Range16 (0x106f, 0x1070, 1),
            new Range16 (0x1075, 0x1081, 1),
            new Range16 (0x108e, 0x10a0, 18),
            new Range16 (0x10a1, 0x10c5, 1),
            new Range16 (0x10c7, 0x10cd, 6),
            new Range16 (0x10d0, 0x10fa, 1),
            new Range16 (0x10fc, 0x1248, 1),
            new Range16 (0x124a, 0x124d, 1),
            new Range16 (0x1250, 0x1256, 1),
            new Range16 (0x1258, 0x125a, 2),
            new Range16 (0x125b, 0x125d, 1),
            new Range16 (0x1260, 0x1288, 1),
            new Range16 (0x128a, 0x128d, 1),
            new Range16 (0x1290, 0x12b0, 1),
            new Range16 (0x12b2, 0x12b5, 1),
            new Range16 (0x12b8, 0x12be, 1),
            new Range16 (0x12c0, 0x12c2, 2),
            new Range16 (0x12c3, 0x12c5, 1),
            new Range16 (0x12c8, 0x12d6, 1),
            new Range16 (0x12d8, 0x1310, 1),
            new Range16 (0x1312, 0x1315, 1),
            new Range16 (0x1318, 0x135a, 1),
            new Range16 (0x1380, 0x138f, 1),
            new Range16 (0x13a0, 0x13f5, 1),
            new Range16 (0x13f8, 0x13fd, 1),
            new Range16 (0x1401, 0x166c, 1),
            new Range16 (0x166f, 0x167f, 1),
            new Range16 (0x1681, 0x169a, 1),
            new Range16 (0x16a0, 0x16ea, 1),
            new Range16 (0x16f1, 0x16f8, 1),
            new Range16 (0x1700, 0x1711, 1),
            new Range16 (0x171f, 0x1731, 1),
            new Range16 (0x1740, 0x1751, 1),
            new Range16 (0x1760, 0x176c, 1),
            new Range16 (0x176e, 0x1770, 1),
            new Range16 (0x1780, 0x17b3, 1),
            new Range16 (0x17d7, 0x17dc, 5),
            new Range16 (0x1820, 0x1878, 1),
            new Range16 (0x1880, 0x1884, 1),
            new Range16 (0x1887, 0x18a8, 1),
            new Range16 (0x18aa, 0x18b0, 6),
            new Range16 (0x18b1, 0x18f5, 1),
            new Range16 (0x1900, 0x191e, 1),
            new Range16 (0x1950, 0x196d, 1),
            new Range16 (0x1970, 0x1974, 1),
            new Range16 (0x1980, 0x19ab, 1),
            new Range16 (0x19b0, 0x19c9, 1),
            new Range16 (0x1a00, 0x1a16, 1),
            new Range16 (0x1a20, 0x1a54, 1),
            new Range16 (0x1aa7, 0x1b05, 94),
            new Range16 (0x1b06, 0x1b33, 1),
            new Range16 (0x1b45, 0x1b4c, 1),
            new Range16 (0x1b83, 0x1ba0, 1),
            new Range16 (0x1bae, 0x1baf, 1),
            new Range16 (0x1bba, 0x1be5, 1),
            new Range16 (0x1c00, 0x1c23, 1),
            new Range16 (0x1c4d, 0x1c4f, 1),
            new Range16 (0x1c5a, 0x1c7d, 1),
            new Range16 (0x1c80, 0x1c88, 1),
            new Range16 (0x1c90, 0x1cba, 1),
            new Range16 (0x1cbd, 0x1cbf, 1),
            new Range16 (0x1ce9, 0x1cec, 1),
            new Range16 (0x1cee, 0x1cf3, 1),
            new Range16 (0x1cf5, 0x1cf6, 1),
            new Range16 (0x1cfa, 0x1d00, 6),
            new Range16 (0x1d01, 0x1dbf, 1),
            new Range16 (0x1e00, 0x1f15, 1),
            new Range16 (0x1f18, 0x1f1d, 1),
            new Range16 (0x1f20, 0x1f45, 1),
            new Range16 (0x1f48, 0x1f4d, 1),
            new Range16 (0x1f50, 0x1f57, 1),
            new Range16 (0x1f59, 0x1f5f, 2),
            new Range16 (0x1f60, 0x1f7d, 1),
            new Range16 (0x1f80, 0x1fb4, 1),
            new Range16 (0x1fb6, 0x1fbc, 1),
            new Range16 (0x1fbe, 0x1fc2, 4),
            new Range16 (0x1fc3, 0x1fc4, 1),
            new Range16 (0x1fc6, 0x1fcc, 1),
            new Range16 (0x1fd0, 0x1fd3, 1),
            new Range16 (0x1fd6, 0x1fdb, 1),
            new Range16 (0x1fe0, 0x1fec, 1),
            new Range16 (0x1ff2, 0x1ff4, 1),
            new Range16 (0x1ff6, 0x1ffc, 1),
            new Range16 (0x2071, 0x207f, 14),
            new Range16 (0x2090, 0x209c, 1),
            new Range16 (0x2102, 0x2107, 5),
            new Range16 (0x210a, 0x2113, 1),
            new Range16 (0x2115, 0x2119, 4),
            new Range16 (0x211a, 0x211d, 1),
            new Range16 (0x2124, 0x212a, 2),
            new Range16 (0x212b, 0x212d, 1),
            new Range16 (0x212f, 0x2139, 1),
            new Range16 (0x213c, 0x213f, 1),
            new Range16 (0x2145, 0x2149, 1),
            new Range16 (0x214e, 0x2183, 53),
            new Range16 (0x2184, 0x2c00, 2684),
            new Range16 (0x2c01, 0x2ce4, 1),
            new Range16 (0x2ceb, 0x2cee, 1),
            new Range16 (0x2cf2, 0x2cf3, 1),
            new Range16 (0x2d00, 0x2d25, 1),
            new Range16 (0x2d27, 0x2d2d, 6),
            new Range16 (0x2d30, 0x2d67, 1),
            new Range16 (0x2d6f, 0x2d80, 17),
            new Range16 (0x2d81, 0x2d96, 1),
            new Range16 (0x2da0, 0x2da6, 1),
            new Range16 (0x2da8, 0x2dae, 1),
            new Range16 (0x2db0, 0x2db6, 1),
            new Range16 (0x2db8, 0x2dbe, 1),
            new Range16 (0x2dc0, 0x2dc6, 1),
            new Range16 (0x2dc8, 0x2dce, 1),
            new Range16 (0x2dd0, 0x2dd6, 1),
            new Range16 (0x2dd8, 0x2dde, 1),
            new Range16 (0x2e2f, 0x3005, 470),
            new Range16 (0x3006, 0x3031, 43),
            new Range16 (0x3032, 0x3035, 1),
            new Range16 (0x303b, 0x303c, 1),
            new Range16 (0x3041, 0x3096, 1),
            new Range16 (0x309d, 0x309f, 1),
            new Range16 (0x30a1, 0x30fa, 1),
            new Range16 (0x30fc, 0x30ff, 1),
            new Range16 (0x3105, 0x312f, 1),
            new Range16 (0x3131, 0x318e, 1),
            new Range16 (0x31a0, 0x31bf, 1),
            new Range16 (0x31f0, 0x31ff, 1),
            new Range16 (0x3400, 0x4dbf, 1),
            new Range16 (0x4e00, 0xa48c, 1),
            new Range16 (0xa4d0, 0xa4fd, 1),
            new Range16 (0xa500, 0xa60c, 1),
            new Range16 (0xa610, 0xa61f, 1),
            new Range16 (0xa62a, 0xa62b, 1),
            new Range16 (0xa640, 0xa66e, 1),
            new Range16 (0xa67f, 0xa69d, 1),
            new Range16 (0xa6a0, 0xa6e5, 1),
            new Range16 (0xa717, 0xa71f, 1),
            new Range16 (0xa722, 0xa788, 1),
            new Range16 (0xa78b, 0xa7ca, 1),
            new Range16 (0xa7d0, 0xa7d1, 1),
            new Range16 (0xa7d3, 0xa7d5, 2),
            new Range16 (0xa7d6, 0xa7d9, 1),
            new Range16 (0xa7f2, 0xa801, 1),
            new Range16 (0xa803, 0xa805, 1),
            new Range16 (0xa807, 0xa80a, 1),
            new Range16 (0xa80c, 0xa822, 1),
            new Range16 (0xa840, 0xa873, 1),
            new Range16 (0xa882, 0xa8b3, 1),
            new Range16 (0xa8f2, 0xa8f7, 1),
            new Range16 (0xa8fb, 0xa8fd, 2),
            new Range16 (0xa8fe, 0xa90a, 12),
            new Range16 (0xa90b, 0xa925, 1),
            new Range16 (0xa930, 0xa946, 1),
            new Range16 (0xa960, 0xa97c, 1),
            new Range16 (0xa984, 0xa9b2, 1),
            new Range16 (0xa9cf, 0xa9e0, 17),
            new Range16 (0xa9e1, 0xa9e4, 1),
            new Range16 (0xa9e6, 0xa9ef, 1),
            new Range16 (0xa9fa, 0xa9fe, 1),
            new Range16 (0xaa00, 0xaa28, 1),
            new Range16 (0xaa40, 0xaa42, 1),
            new Range16 (0xaa44, 0xaa4b, 1),
            new Range16 (0xaa60, 0xaa76, 1),
            new Range16 (0xaa7a, 0xaa7e, 4),
            new Range16 (0xaa7f, 0xaaaf, 1),
            new Range16 (0xaab1, 0xaab5, 4),
            new Range16 (0xaab6, 0xaab9, 3),
            new Range16 (0xaaba, 0xaabd, 1),
            new Range16 (0xaac0, 0xaac2, 2),
            new Range16 (0xaadb, 0xaadd, 1),
            new Range16 (0xaae0, 0xaaea, 1),
            new Range16 (0xaaf2, 0xaaf4, 1),
            new Range16 (0xab01, 0xab06, 1),
            new Range16 (0xab09, 0xab0e, 1),
            new Range16 (0xab11, 0xab16, 1),
            new Range16 (0xab20, 0xab26, 1),
            new Range16 (0xab28, 0xab2e, 1),
            new Range16 (0xab30, 0xab5a, 1),
            new Range16 (0xab5c, 0xab69, 1),
            new Range16 (0xab70, 0xabe2, 1),
            new Range16 (0xac00, 0xd7a3, 1),
            new Range16 (0xd7b0, 0xd7c6, 1),
            new Range16 (0xd7cb, 0xd7fb, 1),
            new Range16 (0xf900, 0xfa6d, 1),
            new Range16 (0xfa70, 0xfad9, 1),
            new Range16 (0xfb00, 0xfb06, 1),
            new Range16 (0xfb13, 0xfb17, 1),
            new Range16 (0xfb1d, 0xfb1f, 2),
            new Range16 (0xfb20, 0xfb28, 1),
            new Range16 (0xfb2a, 0xfb36, 1),
            new Range16 (0xfb38, 0xfb3c, 1),
            new Range16 (0xfb3e, 0xfb40, 2),
            new Range16 (0xfb41, 0xfb43, 2),
            new Range16 (0xfb44, 0xfb46, 2),
            new Range16 (0xfb47, 0xfbb1, 1),
            new Range16 (0xfbd3, 0xfd3d, 1),
            new Range16 (0xfd50, 0xfd8f, 1),
            new Range16 (0xfd92, 0xfdc7, 1),
            new Range16 (0xfdf0, 0xfdfb, 1),
            new Range16 (0xfe70, 0xfe74, 1),
            new Range16 (0xfe76, 0xfefc, 1),
            new Range16 (0xff21, 0xff3a, 1),
            new Range16 (0xff41, 0xff5a, 1),
            new Range16 (0xff66, 0xffbe, 1),
            new Range16 (0xffc2, 0xffc7, 1),
            new Range16 (0xffca, 0xffcf, 1),
            new Range16 (0xffd2, 0xffd7, 1),
            new Range16 (0xffda, 0xffdc, 1),
                },
                r32: new Range32[] {
            new Range32 (0x10000, 0x1000b, 1),
            new Range32 (0x1000d, 0x10026, 1),
            new Range32 (0x10028, 0x1003a, 1),
            new Range32 (0x1003c, 0x1003d, 1),
            new Range32 (0x1003f, 0x1004d, 1),
            new Range32 (0x10050, 0x1005d, 1),
            new Range32 (0x10080, 0x100fa, 1),
            new Range32 (0x10280, 0x1029c, 1),
            new Range32 (0x102a0, 0x102d0, 1),
            new Range32 (0x10300, 0x1031f, 1),
            new Range32 (0x1032d, 0x10340, 1),
            new Range32 (0x10342, 0x10349, 1),
            new Range32 (0x10350, 0x10375, 1),
            new Range32 (0x10380, 0x1039d, 1),
            new Range32 (0x103a0, 0x103c3, 1),
            new Range32 (0x103c8, 0x103cf, 1),
            new Range32 (0x10400, 0x1049d, 1),
            new Range32 (0x104b0, 0x104d3, 1),
            new Range32 (0x104d8, 0x104fb, 1),
            new Range32 (0x10500, 0x10527, 1),
            new Range32 (0x10530, 0x10563, 1),
            new Range32 (0x10570, 0x1057a, 1),
            new Range32 (0x1057c, 0x1058a, 1),
            new Range32 (0x1058c, 0x10592, 1),
            new Range32 (0x10594, 0x10595, 1),
            new Range32 (0x10597, 0x105a1, 1),
            new Range32 (0x105a3, 0x105b1, 1),
            new Range32 (0x105b3, 0x105b9, 1),
            new Range32 (0x105bb, 0x105bc, 1),
            new Range32 (0x10600, 0x10736, 1),
            new Range32 (0x10740, 0x10755, 1),
            new Range32 (0x10760, 0x10767, 1),
            new Range32 (0x10780, 0x10785, 1),
            new Range32 (0x10787, 0x107b0, 1),
            new Range32 (0x107b2, 0x107ba, 1),
            new Range32 (0x10800, 0x10805, 1),
            new Range32 (0x10808, 0x1080a, 2),
            new Range32 (0x1080b, 0x10835, 1),
            new Range32 (0x10837, 0x10838, 1),
            new Range32 (0x1083c, 0x1083f, 3),
            new Range32 (0x10840, 0x10855, 1),
            new Range32 (0x10860, 0x10876, 1),
            new Range32 (0x10880, 0x1089e, 1),
            new Range32 (0x108e0, 0x108f2, 1),
            new Range32 (0x108f4, 0x108f5, 1),
            new Range32 (0x10900, 0x10915, 1),
            new Range32 (0x10920, 0x10939, 1),
            new Range32 (0x10980, 0x109b7, 1),
            new Range32 (0x109be, 0x109bf, 1),
            new Range32 (0x10a00, 0x10a10, 16),
            new Range32 (0x10a11, 0x10a13, 1),
            new Range32 (0x10a15, 0x10a17, 1),
            new Range32 (0x10a19, 0x10a35, 1),
            new Range32 (0x10a60, 0x10a7c, 1),
            new Range32 (0x10a80, 0x10a9c, 1),
            new Range32 (0x10ac0, 0x10ac7, 1),
            new Range32 (0x10ac9, 0x10ae4, 1),
            new Range32 (0x10b00, 0x10b35, 1),
            new Range32 (0x10b40, 0x10b55, 1),
            new Range32 (0x10b60, 0x10b72, 1),
            new Range32 (0x10b80, 0x10b91, 1),
            new Range32 (0x10c00, 0x10c48, 1),
            new Range32 (0x10c80, 0x10cb2, 1),
            new Range32 (0x10cc0, 0x10cf2, 1),
            new Range32 (0x10d00, 0x10d23, 1),
            new Range32 (0x10e80, 0x10ea9, 1),
            new Range32 (0x10eb0, 0x10eb1, 1),
            new Range32 (0x10f00, 0x10f1c, 1),
            new Range32 (0x10f27, 0x10f30, 9),
            new Range32 (0x10f31, 0x10f45, 1),
            new Range32 (0x10f70, 0x10f81, 1),
            new Range32 (0x10fb0, 0x10fc4, 1),
            new Range32 (0x10fe0, 0x10ff6, 1),
            new Range32 (0x11003, 0x11037, 1),
            new Range32 (0x11071, 0x11072, 1),
            new Range32 (0x11075, 0x11083, 14),
            new Range32 (0x11084, 0x110af, 1),
            new Range32 (0x110d0, 0x110e8, 1),
            new Range32 (0x11103, 0x11126, 1),
            new Range32 (0x11144, 0x11147, 3),
            new Range32 (0x11150, 0x11172, 1),
            new Range32 (0x11176, 0x11183, 13),
            new Range32 (0x11184, 0x111b2, 1),
            new Range32 (0x111c1, 0x111c4, 1),
            new Range32 (0x111da, 0x111dc, 2),
            new Range32 (0x11200, 0x11211, 1),
            new Range32 (0x11213, 0x1122b, 1),
            new Range32 (0x1123f, 0x11240, 1),
            new Range32 (0x11280, 0x11286, 1),
            new Range32 (0x11288, 0x1128a, 2),
            new Range32 (0x1128b, 0x1128d, 1),
            new Range32 (0x1128f, 0x1129d, 1),
            new Range32 (0x1129f, 0x112a8, 1),
            new Range32 (0x112b0, 0x112de, 1),
            new Range32 (0x11305, 0x1130c, 1),
            new Range32 (0x1130f, 0x11310, 1),
            new Range32 (0x11313, 0x11328, 1),
            new Range32 (0x1132a, 0x11330, 1),
            new Range32 (0x11332, 0x11333, 1),
            new Range32 (0x11335, 0x11339, 1),
            new Range32 (0x1133d, 0x11350, 19),
            new Range32 (0x1135d, 0x11361, 1),
            new Range32 (0x11400, 0x11434, 1),
            new Range32 (0x11447, 0x1144a, 1),
            new Range32 (0x1145f, 0x11461, 1),
            new Range32 (0x11480, 0x114af, 1),
            new Range32 (0x114c4, 0x114c5, 1),
            new Range32 (0x114c7, 0x11580, 185),
            new Range32 (0x11581, 0x115ae, 1),
            new Range32 (0x115d8, 0x115db, 1),
            new Range32 (0x11600, 0x1162f, 1),
            new Range32 (0x11644, 0x11680, 60),
            new Range32 (0x11681, 0x116aa, 1),
            new Range32 (0x116b8, 0x11700, 72),
            new Range32 (0x11701, 0x1171a, 1),
            new Range32 (0x11740, 0x11746, 1),
            new Range32 (0x11800, 0x1182b, 1),
            new Range32 (0x118a0, 0x118df, 1),
            new Range32 (0x118ff, 0x11906, 1),
            new Range32 (0x11909, 0x1190c, 3),
            new Range32 (0x1190d, 0x11913, 1),
            new Range32 (0x11915, 0x11916, 1),
            new Range32 (0x11918, 0x1192f, 1),
            new Range32 (0x1193f, 0x11941, 2),
            new Range32 (0x119a0, 0x119a7, 1),
            new Range32 (0x119aa, 0x119d0, 1),
            new Range32 (0x119e1, 0x119e3, 2),
            new Range32 (0x11a00, 0x11a0b, 11),
            new Range32 (0x11a0c, 0x11a32, 1),
            new Range32 (0x11a3a, 0x11a50, 22),
            new Range32 (0x11a5c, 0x11a89, 1),
            new Range32 (0x11a9d, 0x11ab0, 19),
            new Range32 (0x11ab1, 0x11af8, 1),
            new Range32 (0x11c00, 0x11c08, 1),
            new Range32 (0x11c0a, 0x11c2e, 1),
            new Range32 (0x11c40, 0x11c72, 50),
            new Range32 (0x11c73, 0x11c8f, 1),
            new Range32 (0x11d00, 0x11d06, 1),
            new Range32 (0x11d08, 0x11d09, 1),
            new Range32 (0x11d0b, 0x11d30, 1),
            new Range32 (0x11d46, 0x11d60, 26),
            new Range32 (0x11d61, 0x11d65, 1),
            new Range32 (0x11d67, 0x11d68, 1),
            new Range32 (0x11d6a, 0x11d89, 1),
            new Range32 (0x11d98, 0x11ee0, 328),
            new Range32 (0x11ee1, 0x11ef2, 1),
            new Range32 (0x11f02, 0x11f04, 2),
            new Range32 (0x11f05, 0x11f10, 1),
            new Range32 (0x11f12, 0x11f33, 1),
            new Range32 (0x11fb0, 0x12000, 80),
            new Range32 (0x12001, 0x12399, 1),
            new Range32 (0x12480, 0x12543, 1),
            new Range32 (0x12f90, 0x12ff0, 1),
            new Range32 (0x13000, 0x1342f, 1),
            new Range32 (0x13441, 0x13446, 1),
            new Range32 (0x14400, 0x14646, 1),
            new Range32 (0x16800, 0x16a38, 1),
            new Range32 (0x16a40, 0x16a5e, 1),
            new Range32 (0x16a70, 0x16abe, 1),
            new Range32 (0x16ad0, 0x16aed, 1),
            new Range32 (0x16b00, 0x16b2f, 1),
            new Range32 (0x16b40, 0x16b43, 1),
            new Range32 (0x16b63, 0x16b77, 1),
            new Range32 (0x16b7d, 0x16b8f, 1),
            new Range32 (0x16e40, 0x16e7f, 1),
            new Range32 (0x16f00, 0x16f4a, 1),
            new Range32 (0x16f50, 0x16f93, 67),
            new Range32 (0x16f94, 0x16f9f, 1),
            new Range32 (0x16fe0, 0x16fe1, 1),
            new Range32 (0x16fe3, 0x17000, 29),
            new Range32 (0x17001, 0x187f7, 1),
            new Range32 (0x18800, 0x18cd5, 1),
            new Range32 (0x18d00, 0x18d08, 1),
            new Range32 (0x1aff0, 0x1aff3, 1),
            new Range32 (0x1aff5, 0x1affb, 1),
            new Range32 (0x1affd, 0x1affe, 1),
            new Range32 (0x1b000, 0x1b122, 1),
            new Range32 (0x1b132, 0x1b150, 30),
            new Range32 (0x1b151, 0x1b152, 1),
            new Range32 (0x1b155, 0x1b164, 15),
            new Range32 (0x1b165, 0x1b167, 1),
            new Range32 (0x1b170, 0x1b2fb, 1),
            new Range32 (0x1bc00, 0x1bc6a, 1),
            new Range32 (0x1bc70, 0x1bc7c, 1),
            new Range32 (0x1bc80, 0x1bc88, 1),
            new Range32 (0x1bc90, 0x1bc99, 1),
            new Range32 (0x1d400, 0x1d454, 1),
            new Range32 (0x1d456, 0x1d49c, 1),
            new Range32 (0x1d49e, 0x1d49f, 1),
            new Range32 (0x1d4a2, 0x1d4a5, 3),
            new Range32 (0x1d4a6, 0x1d4a9, 3),
            new Range32 (0x1d4aa, 0x1d4ac, 1),
            new Range32 (0x1d4ae, 0x1d4b9, 1),
            new Range32 (0x1d4bb, 0x1d4bd, 2),
            new Range32 (0x1d4be, 0x1d4c3, 1),
            new Range32 (0x1d4c5, 0x1d505, 1),
            new Range32 (0x1d507, 0x1d50a, 1),
            new Range32 (0x1d50d, 0x1d514, 1),
            new Range32 (0x1d516, 0x1d51c, 1),
            new Range32 (0x1d51e, 0x1d539, 1),
            new Range32 (0x1d53b, 0x1d53e, 1),
            new Range32 (0x1d540, 0x1d544, 1),
            new Range32 (0x1d546, 0x1d54a, 4),
            new Range32 (0x1d54b, 0x1d550, 1),
            new Range32 (0x1d552, 0x1d6a5, 1),
            new Range32 (0x1d6a8, 0x1d6c0, 1),
            new Range32 (0x1d6c2, 0x1d6da, 1),
            new Range32 (0x1d6dc, 0x1d6fa, 1),
            new Range32 (0x1d6fc, 0x1d714, 1),
            new Range32 (0x1d716, 0x1d734, 1),
            new Range32 (0x1d736, 0x1d74e, 1),
            new Range32 (0x1d750, 0x1d76e, 1),
            new Range32 (0x1d770, 0x1d788, 1),
            new Range32 (0x1d78a, 0x1d7a8, 1),
            new Range32 (0x1d7aa, 0x1d7c2, 1),
            new Range32 (0x1d7c4, 0x1d7cb, 1),
            new Range32 (0x1df00, 0x1df1e, 1),
            new Range32 (0x1df25, 0x1df2a, 1),
            new Range32 (0x1e030, 0x1e06d, 1),
            new Range32 (0x1e100, 0x1e12c, 1),
            new Range32 (0x1e137, 0x1e13d, 1),
            new Range32 (0x1e14e, 0x1e290, 322),
            new Range32 (0x1e291, 0x1e2ad, 1),
            new Range32 (0x1e2c0, 0x1e2eb, 1),
            new Range32 (0x1e4d0, 0x1e4eb, 1),
            new Range32 (0x1e7e0, 0x1e7e6, 1),
            new Range32 (0x1e7e8, 0x1e7eb, 1),
            new Range32 (0x1e7ed, 0x1e7ee, 1),
            new Range32 (0x1e7f0, 0x1e7fe, 1),
            new Range32 (0x1e800, 0x1e8c4, 1),
            new Range32 (0x1e900, 0x1e943, 1),
            new Range32 (0x1e94b, 0x1ee00, 1205),
            new Range32 (0x1ee01, 0x1ee03, 1),
            new Range32 (0x1ee05, 0x1ee1f, 1),
            new Range32 (0x1ee21, 0x1ee22, 1),
            new Range32 (0x1ee24, 0x1ee27, 3),
            new Range32 (0x1ee29, 0x1ee32, 1),
            new Range32 (0x1ee34, 0x1ee37, 1),
            new Range32 (0x1ee39, 0x1ee3b, 2),
            new Range32 (0x1ee42, 0x1ee47, 5),
            new Range32 (0x1ee49, 0x1ee4d, 2),
            new Range32 (0x1ee4e, 0x1ee4f, 1),
            new Range32 (0x1ee51, 0x1ee52, 1),
            new Range32 (0x1ee54, 0x1ee57, 3),
            new Range32 (0x1ee59, 0x1ee61, 2),
            new Range32 (0x1ee62, 0x1ee64, 2),
            new Range32 (0x1ee67, 0x1ee6a, 1),
            new Range32 (0x1ee6c, 0x1ee72, 1),
            new Range32 (0x1ee74, 0x1ee77, 1),
            new Range32 (0x1ee79, 0x1ee7c, 1),
            new Range32 (0x1ee7e, 0x1ee80, 2),
            new Range32 (0x1ee81, 0x1ee89, 1),
            new Range32 (0x1ee8b, 0x1ee9b, 1),
            new Range32 (0x1eea1, 0x1eea3, 1),
            new Range32 (0x1eea5, 0x1eea9, 1),
            new Range32 (0x1eeab, 0x1eebb, 1),
            new Range32 (0x20000, 0x2a6df, 1),
            new Range32 (0x2a700, 0x2b739, 1),
            new Range32 (0x2b740, 0x2b81d, 1),
            new Range32 (0x2b820, 0x2cea1, 1),
            new Range32 (0x2ceb0, 0x2ebe0, 1),
            new Range32 (0x2f800, 0x2fa1d, 1),
            new Range32 (0x30000, 0x3134a, 1),
            new Range32 (0x31350, 0x323af, 1),
                },
                latinOffset: 6
            );

            internal static RangeTable _Ll = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0061, 0x007a, 1),
            new Range16 (0x00b5, 0x00df, 42),
            new Range16 (0x00e0, 0x00f6, 1),
            new Range16 (0x00f8, 0x00ff, 1),
            new Range16 (0x0101, 0x0137, 2),
            new Range16 (0x0138, 0x0148, 2),
            new Range16 (0x0149, 0x0177, 2),
            new Range16 (0x017a, 0x017e, 2),
            new Range16 (0x017f, 0x0180, 1),
            new Range16 (0x0183, 0x0185, 2),
            new Range16 (0x0188, 0x018c, 4),
            new Range16 (0x018d, 0x0192, 5),
            new Range16 (0x0195, 0x0199, 4),
            new Range16 (0x019a, 0x019b, 1),
            new Range16 (0x019e, 0x01a1, 3),
            new Range16 (0x01a3, 0x01a5, 2),
            new Range16 (0x01a8, 0x01aa, 2),
            new Range16 (0x01ab, 0x01ad, 2),
            new Range16 (0x01b0, 0x01b4, 4),
            new Range16 (0x01b6, 0x01b9, 3),
            new Range16 (0x01ba, 0x01bd, 3),
            new Range16 (0x01be, 0x01bf, 1),
            new Range16 (0x01c6, 0x01cc, 3),
            new Range16 (0x01ce, 0x01dc, 2),
            new Range16 (0x01dd, 0x01ef, 2),
            new Range16 (0x01f0, 0x01f3, 3),
            new Range16 (0x01f5, 0x01f9, 4),
            new Range16 (0x01fb, 0x0233, 2),
            new Range16 (0x0234, 0x0239, 1),
            new Range16 (0x023c, 0x023f, 3),
            new Range16 (0x0240, 0x0242, 2),
            new Range16 (0x0247, 0x024f, 2),
            new Range16 (0x0250, 0x0293, 1),
            new Range16 (0x0295, 0x02af, 1),
            new Range16 (0x0371, 0x0373, 2),
            new Range16 (0x0377, 0x037b, 4),
            new Range16 (0x037c, 0x037d, 1),
            new Range16 (0x0390, 0x03ac, 28),
            new Range16 (0x03ad, 0x03ce, 1),
            new Range16 (0x03d0, 0x03d1, 1),
            new Range16 (0x03d5, 0x03d7, 1),
            new Range16 (0x03d9, 0x03ef, 2),
            new Range16 (0x03f0, 0x03f3, 1),
            new Range16 (0x03f5, 0x03fb, 3),
            new Range16 (0x03fc, 0x0430, 52),
            new Range16 (0x0431, 0x045f, 1),
            new Range16 (0x0461, 0x0481, 2),
            new Range16 (0x048b, 0x04bf, 2),
            new Range16 (0x04c2, 0x04ce, 2),
            new Range16 (0x04cf, 0x052f, 2),
            new Range16 (0x0560, 0x0588, 1),
            new Range16 (0x10d0, 0x10fa, 1),
            new Range16 (0x10fd, 0x10ff, 1),
            new Range16 (0x13f8, 0x13fd, 1),
            new Range16 (0x1c80, 0x1c88, 1),
            new Range16 (0x1d00, 0x1d2b, 1),
            new Range16 (0x1d6b, 0x1d77, 1),
            new Range16 (0x1d79, 0x1d9a, 1),
            new Range16 (0x1e01, 0x1e95, 2),
            new Range16 (0x1e96, 0x1e9d, 1),
            new Range16 (0x1e9f, 0x1eff, 2),
            new Range16 (0x1f00, 0x1f07, 1),
            new Range16 (0x1f10, 0x1f15, 1),
            new Range16 (0x1f20, 0x1f27, 1),
            new Range16 (0x1f30, 0x1f37, 1),
            new Range16 (0x1f40, 0x1f45, 1),
            new Range16 (0x1f50, 0x1f57, 1),
            new Range16 (0x1f60, 0x1f67, 1),
            new Range16 (0x1f70, 0x1f7d, 1),
            new Range16 (0x1f80, 0x1f87, 1),
            new Range16 (0x1f90, 0x1f97, 1),
            new Range16 (0x1fa0, 0x1fa7, 1),
            new Range16 (0x1fb0, 0x1fb4, 1),
            new Range16 (0x1fb6, 0x1fb7, 1),
            new Range16 (0x1fbe, 0x1fc2, 4),
            new Range16 (0x1fc3, 0x1fc4, 1),
            new Range16 (0x1fc6, 0x1fc7, 1),
            new Range16 (0x1fd0, 0x1fd3, 1),
            new Range16 (0x1fd6, 0x1fd7, 1),
            new Range16 (0x1fe0, 0x1fe7, 1),
            new Range16 (0x1ff2, 0x1ff4, 1),
            new Range16 (0x1ff6, 0x1ff7, 1),
            new Range16 (0x210a, 0x210e, 4),
            new Range16 (0x210f, 0x2113, 4),
            new Range16 (0x212f, 0x2139, 5),
            new Range16 (0x213c, 0x213d, 1),
            new Range16 (0x2146, 0x2149, 1),
            new Range16 (0x214e, 0x2184, 54),
            new Range16 (0x2c30, 0x2c5f, 1),
            new Range16 (0x2c61, 0x2c65, 4),
            new Range16 (0x2c66, 0x2c6c, 2),
            new Range16 (0x2c71, 0x2c73, 2),
            new Range16 (0x2c74, 0x2c76, 2),
            new Range16 (0x2c77, 0x2c7b, 1),
            new Range16 (0x2c81, 0x2ce3, 2),
            new Range16 (0x2ce4, 0x2cec, 8),
            new Range16 (0x2cee, 0x2cf3, 5),
            new Range16 (0x2d00, 0x2d25, 1),
            new Range16 (0x2d27, 0x2d2d, 6),
            new Range16 (0xa641, 0xa66d, 2),
            new Range16 (0xa681, 0xa69b, 2),
            new Range16 (0xa723, 0xa72f, 2),
            new Range16 (0xa730, 0xa731, 1),
            new Range16 (0xa733, 0xa771, 2),
            new Range16 (0xa772, 0xa778, 1),
            new Range16 (0xa77a, 0xa77c, 2),
            new Range16 (0xa77f, 0xa787, 2),
            new Range16 (0xa78c, 0xa78e, 2),
            new Range16 (0xa791, 0xa793, 2),
            new Range16 (0xa794, 0xa795, 1),
            new Range16 (0xa797, 0xa7a9, 2),
            new Range16 (0xa7af, 0xa7b5, 6),
            new Range16 (0xa7b7, 0xa7c3, 2),
            new Range16 (0xa7c8, 0xa7ca, 2),
            new Range16 (0xa7d1, 0xa7d9, 2),
            new Range16 (0xa7f6, 0xa7fa, 4),
            new Range16 (0xab30, 0xab5a, 1),
            new Range16 (0xab60, 0xab68, 1),
            new Range16 (0xab70, 0xabbf, 1),
            new Range16 (0xfb00, 0xfb06, 1),
            new Range16 (0xfb13, 0xfb17, 1),
            new Range16 (0xff41, 0xff5a, 1),
                },
                r32: new Range32[] {
            new Range32 (0x10428, 0x1044f, 1),
            new Range32 (0x104d8, 0x104fb, 1),
            new Range32 (0x10597, 0x105a1, 1),
            new Range32 (0x105a3, 0x105b1, 1),
            new Range32 (0x105b3, 0x105b9, 1),
            new Range32 (0x105bb, 0x105bc, 1),
            new Range32 (0x10cc0, 0x10cf2, 1),
            new Range32 (0x118c0, 0x118df, 1),
            new Range32 (0x16e60, 0x16e7f, 1),
            new Range32 (0x1d41a, 0x1d433, 1),
            new Range32 (0x1d44e, 0x1d454, 1),
            new Range32 (0x1d456, 0x1d467, 1),
            new Range32 (0x1d482, 0x1d49b, 1),
            new Range32 (0x1d4b6, 0x1d4b9, 1),
            new Range32 (0x1d4bb, 0x1d4bd, 2),
            new Range32 (0x1d4be, 0x1d4c3, 1),
            new Range32 (0x1d4c5, 0x1d4cf, 1),
            new Range32 (0x1d4ea, 0x1d503, 1),
            new Range32 (0x1d51e, 0x1d537, 1),
            new Range32 (0x1d552, 0x1d56b, 1),
            new Range32 (0x1d586, 0x1d59f, 1),
            new Range32 (0x1d5ba, 0x1d5d3, 1),
            new Range32 (0x1d5ee, 0x1d607, 1),
            new Range32 (0x1d622, 0x1d63b, 1),
            new Range32 (0x1d656, 0x1d66f, 1),
            new Range32 (0x1d68a, 0x1d6a5, 1),
            new Range32 (0x1d6c2, 0x1d6da, 1),
            new Range32 (0x1d6dc, 0x1d6e1, 1),
            new Range32 (0x1d6fc, 0x1d714, 1),
            new Range32 (0x1d716, 0x1d71b, 1),
            new Range32 (0x1d736, 0x1d74e, 1),
            new Range32 (0x1d750, 0x1d755, 1),
            new Range32 (0x1d770, 0x1d788, 1),
            new Range32 (0x1d78a, 0x1d78f, 1),
            new Range32 (0x1d7aa, 0x1d7c2, 1),
            new Range32 (0x1d7c4, 0x1d7c9, 1),
            new Range32 (0x1d7cb, 0x1df00, 1845),
            new Range32 (0x1df01, 0x1df09, 1),
            new Range32 (0x1df0b, 0x1df1e, 1),
            new Range32 (0x1df25, 0x1df2a, 1),
            new Range32 (0x1e922, 0x1e943, 1),
                },
                latinOffset: 4
            );

            internal static RangeTable _Lm = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x02b0, 0x02c1, 1),
            new Range16 (0x02c6, 0x02d1, 1),
            new Range16 (0x02e0, 0x02e4, 1),
            new Range16 (0x02ec, 0x02ee, 2),
            new Range16 (0x0374, 0x037a, 6),
            new Range16 (0x0559, 0x0640, 231),
            new Range16 (0x06e5, 0x06e6, 1),
            new Range16 (0x07f4, 0x07f5, 1),
            new Range16 (0x07fa, 0x081a, 32),
            new Range16 (0x0824, 0x0828, 4),
            new Range16 (0x08c9, 0x0971, 168),
            new Range16 (0x0e46, 0x0ec6, 128),
            new Range16 (0x10fc, 0x17d7, 1755),
            new Range16 (0x1843, 0x1aa7, 612),
            new Range16 (0x1c78, 0x1c7d, 1),
            new Range16 (0x1d2c, 0x1d6a, 1),
            new Range16 (0x1d78, 0x1d9b, 35),
            new Range16 (0x1d9c, 0x1dbf, 1),
            new Range16 (0x2071, 0x207f, 14),
            new Range16 (0x2090, 0x209c, 1),
            new Range16 (0x2c7c, 0x2c7d, 1),
            new Range16 (0x2d6f, 0x2e2f, 192),
            new Range16 (0x3005, 0x3031, 44),
            new Range16 (0x3032, 0x3035, 1),
            new Range16 (0x303b, 0x309d, 98),
            new Range16 (0x309e, 0x30fc, 94),
            new Range16 (0x30fd, 0x30fe, 1),
            new Range16 (0xa015, 0xa4f8, 1251),
            new Range16 (0xa4f9, 0xa4fd, 1),
            new Range16 (0xa60c, 0xa67f, 115),
            new Range16 (0xa69c, 0xa69d, 1),
            new Range16 (0xa717, 0xa71f, 1),
            new Range16 (0xa770, 0xa788, 24),
            new Range16 (0xa7f2, 0xa7f4, 1),
            new Range16 (0xa7f8, 0xa7f9, 1),
            new Range16 (0xa9cf, 0xa9e6, 23),
            new Range16 (0xaa70, 0xaadd, 109),
            new Range16 (0xaaf3, 0xaaf4, 1),
            new Range16 (0xab5c, 0xab5f, 1),
            new Range16 (0xab69, 0xff70, 21511),
            new Range16 (0xff9e, 0xff9f, 1),
                },
                r32: new Range32[] {
            new Range32 (0x10780, 0x10785, 1),
            new Range32 (0x10787, 0x107b0, 1),
            new Range32 (0x107b2, 0x107ba, 1),
            new Range32 (0x16b40, 0x16b43, 1),
            new Range32 (0x16f93, 0x16f9f, 1),
            new Range32 (0x16fe0, 0x16fe1, 1),
            new Range32 (0x16fe3, 0x1aff0, 16397),
            new Range32 (0x1aff1, 0x1aff3, 1),
            new Range32 (0x1aff5, 0x1affb, 1),
            new Range32 (0x1affd, 0x1affe, 1),
            new Range32 (0x1e030, 0x1e06d, 1),
            new Range32 (0x1e137, 0x1e13d, 1),
            new Range32 (0x1e4eb, 0x1e94b, 1120),
                }
            );

            internal static RangeTable _Lo = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x00aa, 0x00ba, 16),
            new Range16 (0x01bb, 0x01c0, 5),
            new Range16 (0x01c1, 0x01c3, 1),
            new Range16 (0x0294, 0x05d0, 828),
            new Range16 (0x05d1, 0x05ea, 1),
            new Range16 (0x05ef, 0x05f2, 1),
            new Range16 (0x0620, 0x063f, 1),
            new Range16 (0x0641, 0x064a, 1),
            new Range16 (0x066e, 0x066f, 1),
            new Range16 (0x0671, 0x06d3, 1),
            new Range16 (0x06d5, 0x06ee, 25),
            new Range16 (0x06ef, 0x06fa, 11),
            new Range16 (0x06fb, 0x06fc, 1),
            new Range16 (0x06ff, 0x0710, 17),
            new Range16 (0x0712, 0x072f, 1),
            new Range16 (0x074d, 0x07a5, 1),
            new Range16 (0x07b1, 0x07ca, 25),
            new Range16 (0x07cb, 0x07ea, 1),
            new Range16 (0x0800, 0x0815, 1),
            new Range16 (0x0840, 0x0858, 1),
            new Range16 (0x0860, 0x086a, 1),
            new Range16 (0x0870, 0x0887, 1),
            new Range16 (0x0889, 0x088e, 1),
            new Range16 (0x08a0, 0x08c8, 1),
            new Range16 (0x0904, 0x0939, 1),
            new Range16 (0x093d, 0x0950, 19),
            new Range16 (0x0958, 0x0961, 1),
            new Range16 (0x0972, 0x0980, 1),
            new Range16 (0x0985, 0x098c, 1),
            new Range16 (0x098f, 0x0990, 1),
            new Range16 (0x0993, 0x09a8, 1),
            new Range16 (0x09aa, 0x09b0, 1),
            new Range16 (0x09b2, 0x09b6, 4),
            new Range16 (0x09b7, 0x09b9, 1),
            new Range16 (0x09bd, 0x09ce, 17),
            new Range16 (0x09dc, 0x09dd, 1),
            new Range16 (0x09df, 0x09e1, 1),
            new Range16 (0x09f0, 0x09f1, 1),
            new Range16 (0x09fc, 0x0a05, 9),
            new Range16 (0x0a06, 0x0a0a, 1),
            new Range16 (0x0a0f, 0x0a10, 1),
            new Range16 (0x0a13, 0x0a28, 1),
            new Range16 (0x0a2a, 0x0a30, 1),
            new Range16 (0x0a32, 0x0a33, 1),
            new Range16 (0x0a35, 0x0a36, 1),
            new Range16 (0x0a38, 0x0a39, 1),
            new Range16 (0x0a59, 0x0a5c, 1),
            new Range16 (0x0a5e, 0x0a72, 20),
            new Range16 (0x0a73, 0x0a74, 1),
            new Range16 (0x0a85, 0x0a8d, 1),
            new Range16 (0x0a8f, 0x0a91, 1),
            new Range16 (0x0a93, 0x0aa8, 1),
            new Range16 (0x0aaa, 0x0ab0, 1),
            new Range16 (0x0ab2, 0x0ab3, 1),
            new Range16 (0x0ab5, 0x0ab9, 1),
            new Range16 (0x0abd, 0x0ad0, 19),
            new Range16 (0x0ae0, 0x0ae1, 1),
            new Range16 (0x0af9, 0x0b05, 12),
            new Range16 (0x0b06, 0x0b0c, 1),
            new Range16 (0x0b0f, 0x0b10, 1),
            new Range16 (0x0b13, 0x0b28, 1),
            new Range16 (0x0b2a, 0x0b30, 1),
            new Range16 (0x0b32, 0x0b33, 1),
            new Range16 (0x0b35, 0x0b39, 1),
            new Range16 (0x0b3d, 0x0b5c, 31),
            new Range16 (0x0b5d, 0x0b5f, 2),
            new Range16 (0x0b60, 0x0b61, 1),
            new Range16 (0x0b71, 0x0b83, 18),
            new Range16 (0x0b85, 0x0b8a, 1),
            new Range16 (0x0b8e, 0x0b90, 1),
            new Range16 (0x0b92, 0x0b95, 1),
            new Range16 (0x0b99, 0x0b9a, 1),
            new Range16 (0x0b9c, 0x0b9e, 2),
            new Range16 (0x0b9f, 0x0ba3, 4),
            new Range16 (0x0ba4, 0x0ba8, 4),
            new Range16 (0x0ba9, 0x0baa, 1),
            new Range16 (0x0bae, 0x0bb9, 1),
            new Range16 (0x0bd0, 0x0c05, 53),
            new Range16 (0x0c06, 0x0c0c, 1),
            new Range16 (0x0c0e, 0x0c10, 1),
            new Range16 (0x0c12, 0x0c28, 1),
            new Range16 (0x0c2a, 0x0c39, 1),
            new Range16 (0x0c3d, 0x0c58, 27),
            new Range16 (0x0c59, 0x0c5a, 1),
            new Range16 (0x0c5d, 0x0c60, 3),
            new Range16 (0x0c61, 0x0c80, 31),
            new Range16 (0x0c85, 0x0c8c, 1),
            new Range16 (0x0c8e, 0x0c90, 1),
            new Range16 (0x0c92, 0x0ca8, 1),
            new Range16 (0x0caa, 0x0cb3, 1),
            new Range16 (0x0cb5, 0x0cb9, 1),
            new Range16 (0x0cbd, 0x0cdd, 32),
            new Range16 (0x0cde, 0x0ce0, 2),
            new Range16 (0x0ce1, 0x0cf1, 16),
            new Range16 (0x0cf2, 0x0d04, 18),
            new Range16 (0x0d05, 0x0d0c, 1),
            new Range16 (0x0d0e, 0x0d10, 1),
            new Range16 (0x0d12, 0x0d3a, 1),
            new Range16 (0x0d3d, 0x0d4e, 17),
            new Range16 (0x0d54, 0x0d56, 1),
            new Range16 (0x0d5f, 0x0d61, 1),
            new Range16 (0x0d7a, 0x0d7f, 1),
            new Range16 (0x0d85, 0x0d96, 1),
            new Range16 (0x0d9a, 0x0db1, 1),
            new Range16 (0x0db3, 0x0dbb, 1),
            new Range16 (0x0dbd, 0x0dc0, 3),
            new Range16 (0x0dc1, 0x0dc6, 1),
            new Range16 (0x0e01, 0x0e30, 1),
            new Range16 (0x0e32, 0x0e33, 1),
            new Range16 (0x0e40, 0x0e45, 1),
            new Range16 (0x0e81, 0x0e82, 1),
            new Range16 (0x0e84, 0x0e86, 2),
            new Range16 (0x0e87, 0x0e8a, 1),
            new Range16 (0x0e8c, 0x0ea3, 1),
            new Range16 (0x0ea5, 0x0ea7, 2),
            new Range16 (0x0ea8, 0x0eb0, 1),
            new Range16 (0x0eb2, 0x0eb3, 1),
            new Range16 (0x0ebd, 0x0ec0, 3),
            new Range16 (0x0ec1, 0x0ec4, 1),
            new Range16 (0x0edc, 0x0edf, 1),
            new Range16 (0x0f00, 0x0f40, 64),
            new Range16 (0x0f41, 0x0f47, 1),
            new Range16 (0x0f49, 0x0f6c, 1),
            new Range16 (0x0f88, 0x0f8c, 1),
            new Range16 (0x1000, 0x102a, 1),
            new Range16 (0x103f, 0x1050, 17),
            new Range16 (0x1051, 0x1055, 1),
            new Range16 (0x105a, 0x105d, 1),
            new Range16 (0x1061, 0x1065, 4),
            new Range16 (0x1066, 0x106e, 8),
            new Range16 (0x106f, 0x1070, 1),
            new Range16 (0x1075, 0x1081, 1),
            new Range16 (0x108e, 0x1100, 114),
            new Range16 (0x1101, 0x1248, 1),
            new Range16 (0x124a, 0x124d, 1),
            new Range16 (0x1250, 0x1256, 1),
            new Range16 (0x1258, 0x125a, 2),
            new Range16 (0x125b, 0x125d, 1),
            new Range16 (0x1260, 0x1288, 1),
            new Range16 (0x128a, 0x128d, 1),
            new Range16 (0x1290, 0x12b0, 1),
            new Range16 (0x12b2, 0x12b5, 1),
            new Range16 (0x12b8, 0x12be, 1),
            new Range16 (0x12c0, 0x12c2, 2),
            new Range16 (0x12c3, 0x12c5, 1),
            new Range16 (0x12c8, 0x12d6, 1),
            new Range16 (0x12d8, 0x1310, 1),
            new Range16 (0x1312, 0x1315, 1),
            new Range16 (0x1318, 0x135a, 1),
            new Range16 (0x1380, 0x138f, 1),
            new Range16 (0x1401, 0x166c, 1),
            new Range16 (0x166f, 0x167f, 1),
            new Range16 (0x1681, 0x169a, 1),
            new Range16 (0x16a0, 0x16ea, 1),
            new Range16 (0x16f1, 0x16f8, 1),
            new Range16 (0x1700, 0x1711, 1),
            new Range16 (0x171f, 0x1731, 1),
            new Range16 (0x1740, 0x1751, 1),
            new Range16 (0x1760, 0x176c, 1),
            new Range16 (0x176e, 0x1770, 1),
            new Range16 (0x1780, 0x17b3, 1),
            new Range16 (0x17dc, 0x1820, 68),
            new Range16 (0x1821, 0x1842, 1),
            new Range16 (0x1844, 0x1878, 1),
            new Range16 (0x1880, 0x1884, 1),
            new Range16 (0x1887, 0x18a8, 1),
            new Range16 (0x18aa, 0x18b0, 6),
            new Range16 (0x18b1, 0x18f5, 1),
            new Range16 (0x1900, 0x191e, 1),
            new Range16 (0x1950, 0x196d, 1),
            new Range16 (0x1970, 0x1974, 1),
            new Range16 (0x1980, 0x19ab, 1),
            new Range16 (0x19b0, 0x19c9, 1),
            new Range16 (0x1a00, 0x1a16, 1),
            new Range16 (0x1a20, 0x1a54, 1),
            new Range16 (0x1b05, 0x1b33, 1),
            new Range16 (0x1b45, 0x1b4c, 1),
            new Range16 (0x1b83, 0x1ba0, 1),
            new Range16 (0x1bae, 0x1baf, 1),
            new Range16 (0x1bba, 0x1be5, 1),
            new Range16 (0x1c00, 0x1c23, 1),
            new Range16 (0x1c4d, 0x1c4f, 1),
            new Range16 (0x1c5a, 0x1c77, 1),
            new Range16 (0x1ce9, 0x1cec, 1),
            new Range16 (0x1cee, 0x1cf3, 1),
            new Range16 (0x1cf5, 0x1cf6, 1),
            new Range16 (0x1cfa, 0x2135, 1083),
            new Range16 (0x2136, 0x2138, 1),
            new Range16 (0x2d30, 0x2d67, 1),
            new Range16 (0x2d80, 0x2d96, 1),
            new Range16 (0x2da0, 0x2da6, 1),
            new Range16 (0x2da8, 0x2dae, 1),
            new Range16 (0x2db0, 0x2db6, 1),
            new Range16 (0x2db8, 0x2dbe, 1),
            new Range16 (0x2dc0, 0x2dc6, 1),
            new Range16 (0x2dc8, 0x2dce, 1),
            new Range16 (0x2dd0, 0x2dd6, 1),
            new Range16 (0x2dd8, 0x2dde, 1),
            new Range16 (0x3006, 0x303c, 54),
            new Range16 (0x3041, 0x3096, 1),
            new Range16 (0x309f, 0x30a1, 2),
            new Range16 (0x30a2, 0x30fa, 1),
            new Range16 (0x30ff, 0x3105, 6),
            new Range16 (0x3106, 0x312f, 1),
            new Range16 (0x3131, 0x318e, 1),
            new Range16 (0x31a0, 0x31bf, 1),
            new Range16 (0x31f0, 0x31ff, 1),
            new Range16 (0x3400, 0x4dbf, 1),
            new Range16 (0x4e00, 0xa014, 1),
            new Range16 (0xa016, 0xa48c, 1),
            new Range16 (0xa4d0, 0xa4f7, 1),
            new Range16 (0xa500, 0xa60b, 1),
            new Range16 (0xa610, 0xa61f, 1),
            new Range16 (0xa62a, 0xa62b, 1),
            new Range16 (0xa66e, 0xa6a0, 50),
            new Range16 (0xa6a1, 0xa6e5, 1),
            new Range16 (0xa78f, 0xa7f7, 104),
            new Range16 (0xa7fb, 0xa801, 1),
            new Range16 (0xa803, 0xa805, 1),
            new Range16 (0xa807, 0xa80a, 1),
            new Range16 (0xa80c, 0xa822, 1),
            new Range16 (0xa840, 0xa873, 1),
            new Range16 (0xa882, 0xa8b3, 1),
            new Range16 (0xa8f2, 0xa8f7, 1),
            new Range16 (0xa8fb, 0xa8fd, 2),
            new Range16 (0xa8fe, 0xa90a, 12),
            new Range16 (0xa90b, 0xa925, 1),
            new Range16 (0xa930, 0xa946, 1),
            new Range16 (0xa960, 0xa97c, 1),
            new Range16 (0xa984, 0xa9b2, 1),
            new Range16 (0xa9e0, 0xa9e4, 1),
            new Range16 (0xa9e7, 0xa9ef, 1),
            new Range16 (0xa9fa, 0xa9fe, 1),
            new Range16 (0xaa00, 0xaa28, 1),
            new Range16 (0xaa40, 0xaa42, 1),
            new Range16 (0xaa44, 0xaa4b, 1),
            new Range16 (0xaa60, 0xaa6f, 1),
            new Range16 (0xaa71, 0xaa76, 1),
            new Range16 (0xaa7a, 0xaa7e, 4),
            new Range16 (0xaa7f, 0xaaaf, 1),
            new Range16 (0xaab1, 0xaab5, 4),
            new Range16 (0xaab6, 0xaab9, 3),
            new Range16 (0xaaba, 0xaabd, 1),
            new Range16 (0xaac0, 0xaac2, 2),
            new Range16 (0xaadb, 0xaadc, 1),
            new Range16 (0xaae0, 0xaaea, 1),
            new Range16 (0xaaf2, 0xab01, 15),
            new Range16 (0xab02, 0xab06, 1),
            new Range16 (0xab09, 0xab0e, 1),
            new Range16 (0xab11, 0xab16, 1),
            new Range16 (0xab20, 0xab26, 1),
            new Range16 (0xab28, 0xab2e, 1),
            new Range16 (0xabc0, 0xabe2, 1),
            new Range16 (0xac00, 0xd7a3, 1),
            new Range16 (0xd7b0, 0xd7c6, 1),
            new Range16 (0xd7cb, 0xd7fb, 1),
            new Range16 (0xf900, 0xfa6d, 1),
            new Range16 (0xfa70, 0xfad9, 1),
            new Range16 (0xfb1d, 0xfb1f, 2),
            new Range16 (0xfb20, 0xfb28, 1),
            new Range16 (0xfb2a, 0xfb36, 1),
            new Range16 (0xfb38, 0xfb3c, 1),
            new Range16 (0xfb3e, 0xfb40, 2),
            new Range16 (0xfb41, 0xfb43, 2),
            new Range16 (0xfb44, 0xfb46, 2),
            new Range16 (0xfb47, 0xfbb1, 1),
            new Range16 (0xfbd3, 0xfd3d, 1),
            new Range16 (0xfd50, 0xfd8f, 1),
            new Range16 (0xfd92, 0xfdc7, 1),
            new Range16 (0xfdf0, 0xfdfb, 1),
            new Range16 (0xfe70, 0xfe74, 1),
            new Range16 (0xfe76, 0xfefc, 1),
            new Range16 (0xff66, 0xff6f, 1),
            new Range16 (0xff71, 0xff9d, 1),
            new Range16 (0xffa0, 0xffbe, 1),
            new Range16 (0xffc2, 0xffc7, 1),
            new Range16 (0xffca, 0xffcf, 1),
            new Range16 (0xffd2, 0xffd7, 1),
            new Range16 (0xffda, 0xffdc, 1),
                },
                r32: new Range32[] {
            new Range32 (0x10000, 0x1000b, 1),
            new Range32 (0x1000d, 0x10026, 1),
            new Range32 (0x10028, 0x1003a, 1),
            new Range32 (0x1003c, 0x1003d, 1),
            new Range32 (0x1003f, 0x1004d, 1),
            new Range32 (0x10050, 0x1005d, 1),
            new Range32 (0x10080, 0x100fa, 1),
            new Range32 (0x10280, 0x1029c, 1),
            new Range32 (0x102a0, 0x102d0, 1),
            new Range32 (0x10300, 0x1031f, 1),
            new Range32 (0x1032d, 0x10340, 1),
            new Range32 (0x10342, 0x10349, 1),
            new Range32 (0x10350, 0x10375, 1),
            new Range32 (0x10380, 0x1039d, 1),
            new Range32 (0x103a0, 0x103c3, 1),
            new Range32 (0x103c8, 0x103cf, 1),
            new Range32 (0x10450, 0x1049d, 1),
            new Range32 (0x10500, 0x10527, 1),
            new Range32 (0x10530, 0x10563, 1),
            new Range32 (0x10600, 0x10736, 1),
            new Range32 (0x10740, 0x10755, 1),
            new Range32 (0x10760, 0x10767, 1),
            new Range32 (0x10800, 0x10805, 1),
            new Range32 (0x10808, 0x1080a, 2),
            new Range32 (0x1080b, 0x10835, 1),
            new Range32 (0x10837, 0x10838, 1),
            new Range32 (0x1083c, 0x1083f, 3),
            new Range32 (0x10840, 0x10855, 1),
            new Range32 (0x10860, 0x10876, 1),
            new Range32 (0x10880, 0x1089e, 1),
            new Range32 (0x108e0, 0x108f2, 1),
            new Range32 (0x108f4, 0x108f5, 1),
            new Range32 (0x10900, 0x10915, 1),
            new Range32 (0x10920, 0x10939, 1),
            new Range32 (0x10980, 0x109b7, 1),
            new Range32 (0x109be, 0x109bf, 1),
            new Range32 (0x10a00, 0x10a10, 16),
            new Range32 (0x10a11, 0x10a13, 1),
            new Range32 (0x10a15, 0x10a17, 1),
            new Range32 (0x10a19, 0x10a35, 1),
            new Range32 (0x10a60, 0x10a7c, 1),
            new Range32 (0x10a80, 0x10a9c, 1),
            new Range32 (0x10ac0, 0x10ac7, 1),
            new Range32 (0x10ac9, 0x10ae4, 1),
            new Range32 (0x10b00, 0x10b35, 1),
            new Range32 (0x10b40, 0x10b55, 1),
            new Range32 (0x10b60, 0x10b72, 1),
            new Range32 (0x10b80, 0x10b91, 1),
            new Range32 (0x10c00, 0x10c48, 1),
            new Range32 (0x10d00, 0x10d23, 1),
            new Range32 (0x10e80, 0x10ea9, 1),
            new Range32 (0x10eb0, 0x10eb1, 1),
            new Range32 (0x10f00, 0x10f1c, 1),
            new Range32 (0x10f27, 0x10f30, 9),
            new Range32 (0x10f31, 0x10f45, 1),
            new Range32 (0x10f70, 0x10f81, 1),
            new Range32 (0x10fb0, 0x10fc4, 1),
            new Range32 (0x10fe0, 0x10ff6, 1),
            new Range32 (0x11003, 0x11037, 1),
            new Range32 (0x11071, 0x11072, 1),
            new Range32 (0x11075, 0x11083, 14),
            new Range32 (0x11084, 0x110af, 1),
            new Range32 (0x110d0, 0x110e8, 1),
            new Range32 (0x11103, 0x11126, 1),
            new Range32 (0x11144, 0x11147, 3),
            new Range32 (0x11150, 0x11172, 1),
            new Range32 (0x11176, 0x11183, 13),
            new Range32 (0x11184, 0x111b2, 1),
            new Range32 (0x111c1, 0x111c4, 1),
            new Range32 (0x111da, 0x111dc, 2),
            new Range32 (0x11200, 0x11211, 1),
            new Range32 (0x11213, 0x1122b, 1),
            new Range32 (0x1123f, 0x11240, 1),
            new Range32 (0x11280, 0x11286, 1),
            new Range32 (0x11288, 0x1128a, 2),
            new Range32 (0x1128b, 0x1128d, 1),
            new Range32 (0x1128f, 0x1129d, 1),
            new Range32 (0x1129f, 0x112a8, 1),
            new Range32 (0x112b0, 0x112de, 1),
            new Range32 (0x11305, 0x1130c, 1),
            new Range32 (0x1130f, 0x11310, 1),
            new Range32 (0x11313, 0x11328, 1),
            new Range32 (0x1132a, 0x11330, 1),
            new Range32 (0x11332, 0x11333, 1),
            new Range32 (0x11335, 0x11339, 1),
            new Range32 (0x1133d, 0x11350, 19),
            new Range32 (0x1135d, 0x11361, 1),
            new Range32 (0x11400, 0x11434, 1),
            new Range32 (0x11447, 0x1144a, 1),
            new Range32 (0x1145f, 0x11461, 1),
            new Range32 (0x11480, 0x114af, 1),
            new Range32 (0x114c4, 0x114c5, 1),
            new Range32 (0x114c7, 0x11580, 185),
            new Range32 (0x11581, 0x115ae, 1),
            new Range32 (0x115d8, 0x115db, 1),
            new Range32 (0x11600, 0x1162f, 1),
            new Range32 (0x11644, 0x11680, 60),
            new Range32 (0x11681, 0x116aa, 1),
            new Range32 (0x116b8, 0x11700, 72),
            new Range32 (0x11701, 0x1171a, 1),
            new Range32 (0x11740, 0x11746, 1),
            new Range32 (0x11800, 0x1182b, 1),
            new Range32 (0x118ff, 0x11906, 1),
            new Range32 (0x11909, 0x1190c, 3),
            new Range32 (0x1190d, 0x11913, 1),
            new Range32 (0x11915, 0x11916, 1),
            new Range32 (0x11918, 0x1192f, 1),
            new Range32 (0x1193f, 0x11941, 2),
            new Range32 (0x119a0, 0x119a7, 1),
            new Range32 (0x119aa, 0x119d0, 1),
            new Range32 (0x119e1, 0x119e3, 2),
            new Range32 (0x11a00, 0x11a0b, 11),
            new Range32 (0x11a0c, 0x11a32, 1),
            new Range32 (0x11a3a, 0x11a50, 22),
            new Range32 (0x11a5c, 0x11a89, 1),
            new Range32 (0x11a9d, 0x11ab0, 19),
            new Range32 (0x11ab1, 0x11af8, 1),
            new Range32 (0x11c00, 0x11c08, 1),
            new Range32 (0x11c0a, 0x11c2e, 1),
            new Range32 (0x11c40, 0x11c72, 50),
            new Range32 (0x11c73, 0x11c8f, 1),
            new Range32 (0x11d00, 0x11d06, 1),
            new Range32 (0x11d08, 0x11d09, 1),
            new Range32 (0x11d0b, 0x11d30, 1),
            new Range32 (0x11d46, 0x11d60, 26),
            new Range32 (0x11d61, 0x11d65, 1),
            new Range32 (0x11d67, 0x11d68, 1),
            new Range32 (0x11d6a, 0x11d89, 1),
            new Range32 (0x11d98, 0x11ee0, 328),
            new Range32 (0x11ee1, 0x11ef2, 1),
            new Range32 (0x11f02, 0x11f04, 2),
            new Range32 (0x11f05, 0x11f10, 1),
            new Range32 (0x11f12, 0x11f33, 1),
            new Range32 (0x11fb0, 0x12000, 80),
            new Range32 (0x12001, 0x12399, 1),
            new Range32 (0x12480, 0x12543, 1),
            new Range32 (0x12f90, 0x12ff0, 1),
            new Range32 (0x13000, 0x1342f, 1),
            new Range32 (0x13441, 0x13446, 1),
            new Range32 (0x14400, 0x14646, 1),
            new Range32 (0x16800, 0x16a38, 1),
            new Range32 (0x16a40, 0x16a5e, 1),
            new Range32 (0x16a70, 0x16abe, 1),
            new Range32 (0x16ad0, 0x16aed, 1),
            new Range32 (0x16b00, 0x16b2f, 1),
            new Range32 (0x16b63, 0x16b77, 1),
            new Range32 (0x16b7d, 0x16b8f, 1),
            new Range32 (0x16f00, 0x16f4a, 1),
            new Range32 (0x16f50, 0x17000, 176),
            new Range32 (0x17001, 0x187f7, 1),
            new Range32 (0x18800, 0x18cd5, 1),
            new Range32 (0x18d00, 0x18d08, 1),
            new Range32 (0x1b000, 0x1b122, 1),
            new Range32 (0x1b132, 0x1b150, 30),
            new Range32 (0x1b151, 0x1b152, 1),
            new Range32 (0x1b155, 0x1b164, 15),
            new Range32 (0x1b165, 0x1b167, 1),
            new Range32 (0x1b170, 0x1b2fb, 1),
            new Range32 (0x1bc00, 0x1bc6a, 1),
            new Range32 (0x1bc70, 0x1bc7c, 1),
            new Range32 (0x1bc80, 0x1bc88, 1),
            new Range32 (0x1bc90, 0x1bc99, 1),
            new Range32 (0x1df0a, 0x1e100, 502),
            new Range32 (0x1e101, 0x1e12c, 1),
            new Range32 (0x1e14e, 0x1e290, 322),
            new Range32 (0x1e291, 0x1e2ad, 1),
            new Range32 (0x1e2c0, 0x1e2eb, 1),
            new Range32 (0x1e4d0, 0x1e4ea, 1),
            new Range32 (0x1e7e0, 0x1e7e6, 1),
            new Range32 (0x1e7e8, 0x1e7eb, 1),
            new Range32 (0x1e7ed, 0x1e7ee, 1),
            new Range32 (0x1e7f0, 0x1e7fe, 1),
            new Range32 (0x1e800, 0x1e8c4, 1),
            new Range32 (0x1ee00, 0x1ee03, 1),
            new Range32 (0x1ee05, 0x1ee1f, 1),
            new Range32 (0x1ee21, 0x1ee22, 1),
            new Range32 (0x1ee24, 0x1ee27, 3),
            new Range32 (0x1ee29, 0x1ee32, 1),
            new Range32 (0x1ee34, 0x1ee37, 1),
            new Range32 (0x1ee39, 0x1ee3b, 2),
            new Range32 (0x1ee42, 0x1ee47, 5),
            new Range32 (0x1ee49, 0x1ee4d, 2),
            new Range32 (0x1ee4e, 0x1ee4f, 1),
            new Range32 (0x1ee51, 0x1ee52, 1),
            new Range32 (0x1ee54, 0x1ee57, 3),
            new Range32 (0x1ee59, 0x1ee61, 2),
            new Range32 (0x1ee62, 0x1ee64, 2),
            new Range32 (0x1ee67, 0x1ee6a, 1),
            new Range32 (0x1ee6c, 0x1ee72, 1),
            new Range32 (0x1ee74, 0x1ee77, 1),
            new Range32 (0x1ee79, 0x1ee7c, 1),
            new Range32 (0x1ee7e, 0x1ee80, 2),
            new Range32 (0x1ee81, 0x1ee89, 1),
            new Range32 (0x1ee8b, 0x1ee9b, 1),
            new Range32 (0x1eea1, 0x1eea3, 1),
            new Range32 (0x1eea5, 0x1eea9, 1),
            new Range32 (0x1eeab, 0x1eebb, 1),
            new Range32 (0x20000, 0x2a6df, 1),
            new Range32 (0x2a700, 0x2b739, 1),
            new Range32 (0x2b740, 0x2b81d, 1),
            new Range32 (0x2b820, 0x2cea1, 1),
            new Range32 (0x2ceb0, 0x2ebe0, 1),
            new Range32 (0x2f800, 0x2fa1d, 1),
            new Range32 (0x30000, 0x3134a, 1),
            new Range32 (0x31350, 0x323af, 1),
                },
                latinOffset: 1
            );

            internal static RangeTable _Lt = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x01c5, 0x01cb, 3),
            new Range16 (0x01f2, 0x1f88, 7574),
            new Range16 (0x1f89, 0x1f8f, 1),
            new Range16 (0x1f98, 0x1f9f, 1),
            new Range16 (0x1fa8, 0x1faf, 1),
            new Range16 (0x1fbc, 0x1fcc, 16),
            new Range16 (0x1ffc, 0x1ffc, 1),
                }
            );

            internal static RangeTable _Lu = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0041, 0x005a, 1),
            new Range16 (0x00c0, 0x00d6, 1),
            new Range16 (0x00d8, 0x00de, 1),
            new Range16 (0x0100, 0x0136, 2),
            new Range16 (0x0139, 0x0147, 2),
            new Range16 (0x014a, 0x0178, 2),
            new Range16 (0x0179, 0x017d, 2),
            new Range16 (0x0181, 0x0182, 1),
            new Range16 (0x0184, 0x0186, 2),
            new Range16 (0x0187, 0x0189, 2),
            new Range16 (0x018a, 0x018b, 1),
            new Range16 (0x018e, 0x0191, 1),
            new Range16 (0x0193, 0x0194, 1),
            new Range16 (0x0196, 0x0198, 1),
            new Range16 (0x019c, 0x019d, 1),
            new Range16 (0x019f, 0x01a0, 1),
            new Range16 (0x01a2, 0x01a6, 2),
            new Range16 (0x01a7, 0x01a9, 2),
            new Range16 (0x01ac, 0x01ae, 2),
            new Range16 (0x01af, 0x01b1, 2),
            new Range16 (0x01b2, 0x01b3, 1),
            new Range16 (0x01b5, 0x01b7, 2),
            new Range16 (0x01b8, 0x01bc, 4),
            new Range16 (0x01c4, 0x01cd, 3),
            new Range16 (0x01cf, 0x01db, 2),
            new Range16 (0x01de, 0x01ee, 2),
            new Range16 (0x01f1, 0x01f4, 3),
            new Range16 (0x01f6, 0x01f8, 1),
            new Range16 (0x01fa, 0x0232, 2),
            new Range16 (0x023a, 0x023b, 1),
            new Range16 (0x023d, 0x023e, 1),
            new Range16 (0x0241, 0x0243, 2),
            new Range16 (0x0244, 0x0246, 1),
            new Range16 (0x0248, 0x024e, 2),
            new Range16 (0x0370, 0x0372, 2),
            new Range16 (0x0376, 0x037f, 9),
            new Range16 (0x0386, 0x0388, 2),
            new Range16 (0x0389, 0x038a, 1),
            new Range16 (0x038c, 0x038e, 2),
            new Range16 (0x038f, 0x0391, 2),
            new Range16 (0x0392, 0x03a1, 1),
            new Range16 (0x03a3, 0x03ab, 1),
            new Range16 (0x03cf, 0x03d2, 3),
            new Range16 (0x03d3, 0x03d4, 1),
            new Range16 (0x03d8, 0x03ee, 2),
            new Range16 (0x03f4, 0x03f7, 3),
            new Range16 (0x03f9, 0x03fa, 1),
            new Range16 (0x03fd, 0x042f, 1),
            new Range16 (0x0460, 0x0480, 2),
            new Range16 (0x048a, 0x04c0, 2),
            new Range16 (0x04c1, 0x04cd, 2),
            new Range16 (0x04d0, 0x052e, 2),
            new Range16 (0x0531, 0x0556, 1),
            new Range16 (0x10a0, 0x10c5, 1),
            new Range16 (0x10c7, 0x10cd, 6),
            new Range16 (0x13a0, 0x13f5, 1),
            new Range16 (0x1c90, 0x1cba, 1),
            new Range16 (0x1cbd, 0x1cbf, 1),
            new Range16 (0x1e00, 0x1e94, 2),
            new Range16 (0x1e9e, 0x1efe, 2),
            new Range16 (0x1f08, 0x1f0f, 1),
            new Range16 (0x1f18, 0x1f1d, 1),
            new Range16 (0x1f28, 0x1f2f, 1),
            new Range16 (0x1f38, 0x1f3f, 1),
            new Range16 (0x1f48, 0x1f4d, 1),
            new Range16 (0x1f59, 0x1f5f, 2),
            new Range16 (0x1f68, 0x1f6f, 1),
            new Range16 (0x1fb8, 0x1fbb, 1),
            new Range16 (0x1fc8, 0x1fcb, 1),
            new Range16 (0x1fd8, 0x1fdb, 1),
            new Range16 (0x1fe8, 0x1fec, 1),
            new Range16 (0x1ff8, 0x1ffb, 1),
            new Range16 (0x2102, 0x2107, 5),
            new Range16 (0x210b, 0x210d, 1),
            new Range16 (0x2110, 0x2112, 1),
            new Range16 (0x2115, 0x2119, 4),
            new Range16 (0x211a, 0x211d, 1),
            new Range16 (0x2124, 0x212a, 2),
            new Range16 (0x212b, 0x212d, 1),
            new Range16 (0x2130, 0x2133, 1),
            new Range16 (0x213e, 0x213f, 1),
            new Range16 (0x2145, 0x2183, 62),
            new Range16 (0x2c00, 0x2c2f, 1),
            new Range16 (0x2c60, 0x2c62, 2),
            new Range16 (0x2c63, 0x2c64, 1),
            new Range16 (0x2c67, 0x2c6d, 2),
            new Range16 (0x2c6e, 0x2c70, 1),
            new Range16 (0x2c72, 0x2c75, 3),
            new Range16 (0x2c7e, 0x2c80, 1),
            new Range16 (0x2c82, 0x2ce2, 2),
            new Range16 (0x2ceb, 0x2ced, 2),
            new Range16 (0x2cf2, 0xa640, 31054),
            new Range16 (0xa642, 0xa66c, 2),
            new Range16 (0xa680, 0xa69a, 2),
            new Range16 (0xa722, 0xa72e, 2),
            new Range16 (0xa732, 0xa76e, 2),
            new Range16 (0xa779, 0xa77d, 2),
            new Range16 (0xa77e, 0xa786, 2),
            new Range16 (0xa78b, 0xa78d, 2),
            new Range16 (0xa790, 0xa792, 2),
            new Range16 (0xa796, 0xa7aa, 2),
            new Range16 (0xa7ab, 0xa7ae, 1),
            new Range16 (0xa7b0, 0xa7b4, 1),
            new Range16 (0xa7b6, 0xa7c4, 2),
            new Range16 (0xa7c5, 0xa7c7, 1),
            new Range16 (0xa7c9, 0xa7d0, 7),
            new Range16 (0xa7d6, 0xa7d8, 2),
            new Range16 (0xa7f5, 0xff21, 22316),
            new Range16 (0xff22, 0xff3a, 1),
                },
                r32: new Range32[] {
            new Range32 (0x10400, 0x10427, 1),
            new Range32 (0x104b0, 0x104d3, 1),
            new Range32 (0x10570, 0x1057a, 1),
            new Range32 (0x1057c, 0x1058a, 1),
            new Range32 (0x1058c, 0x10592, 1),
            new Range32 (0x10594, 0x10595, 1),
            new Range32 (0x10c80, 0x10cb2, 1),
            new Range32 (0x118a0, 0x118bf, 1),
            new Range32 (0x16e40, 0x16e5f, 1),
            new Range32 (0x1d400, 0x1d419, 1),
            new Range32 (0x1d434, 0x1d44d, 1),
            new Range32 (0x1d468, 0x1d481, 1),
            new Range32 (0x1d49c, 0x1d49e, 2),
            new Range32 (0x1d49f, 0x1d4a5, 3),
            new Range32 (0x1d4a6, 0x1d4a9, 3),
            new Range32 (0x1d4aa, 0x1d4ac, 1),
            new Range32 (0x1d4ae, 0x1d4b5, 1),
            new Range32 (0x1d4d0, 0x1d4e9, 1),
            new Range32 (0x1d504, 0x1d505, 1),
            new Range32 (0x1d507, 0x1d50a, 1),
            new Range32 (0x1d50d, 0x1d514, 1),
            new Range32 (0x1d516, 0x1d51c, 1),
            new Range32 (0x1d538, 0x1d539, 1),
            new Range32 (0x1d53b, 0x1d53e, 1),
            new Range32 (0x1d540, 0x1d544, 1),
            new Range32 (0x1d546, 0x1d54a, 4),
            new Range32 (0x1d54b, 0x1d550, 1),
            new Range32 (0x1d56c, 0x1d585, 1),
            new Range32 (0x1d5a0, 0x1d5b9, 1),
            new Range32 (0x1d5d4, 0x1d5ed, 1),
            new Range32 (0x1d608, 0x1d621, 1),
            new Range32 (0x1d63c, 0x1d655, 1),
            new Range32 (0x1d670, 0x1d689, 1),
            new Range32 (0x1d6a8, 0x1d6c0, 1),
            new Range32 (0x1d6e2, 0x1d6fa, 1),
            new Range32 (0x1d71c, 0x1d734, 1),
            new Range32 (0x1d756, 0x1d76e, 1),
            new Range32 (0x1d790, 0x1d7a8, 1),
            new Range32 (0x1d7ca, 0x1e900, 4406),
            new Range32 (0x1e901, 0x1e921, 1),
                },
                latinOffset: 3
            );

            internal static RangeTable _M = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0300, 0x036f, 1),
            new Range16 (0x0483, 0x0489, 1),
            new Range16 (0x0591, 0x05bd, 1),
            new Range16 (0x05bf, 0x05c1, 2),
            new Range16 (0x05c2, 0x05c4, 2),
            new Range16 (0x05c5, 0x05c7, 2),
            new Range16 (0x0610, 0x061a, 1),
            new Range16 (0x064b, 0x065f, 1),
            new Range16 (0x0670, 0x06d6, 102),
            new Range16 (0x06d7, 0x06dc, 1),
            new Range16 (0x06df, 0x06e4, 1),
            new Range16 (0x06e7, 0x06e8, 1),
            new Range16 (0x06ea, 0x06ed, 1),
            new Range16 (0x0711, 0x0730, 31),
            new Range16 (0x0731, 0x074a, 1),
            new Range16 (0x07a6, 0x07b0, 1),
            new Range16 (0x07eb, 0x07f3, 1),
            new Range16 (0x07fd, 0x0816, 25),
            new Range16 (0x0817, 0x0819, 1),
            new Range16 (0x081b, 0x0823, 1),
            new Range16 (0x0825, 0x0827, 1),
            new Range16 (0x0829, 0x082d, 1),
            new Range16 (0x0859, 0x085b, 1),
            new Range16 (0x0898, 0x089f, 1),
            new Range16 (0x08ca, 0x08e1, 1),
            new Range16 (0x08e3, 0x0903, 1),
            new Range16 (0x093a, 0x093c, 1),
            new Range16 (0x093e, 0x094f, 1),
            new Range16 (0x0951, 0x0957, 1),
            new Range16 (0x0962, 0x0963, 1),
            new Range16 (0x0981, 0x0983, 1),
            new Range16 (0x09bc, 0x09be, 2),
            new Range16 (0x09bf, 0x09c4, 1),
            new Range16 (0x09c7, 0x09c8, 1),
            new Range16 (0x09cb, 0x09cd, 1),
            new Range16 (0x09d7, 0x09e2, 11),
            new Range16 (0x09e3, 0x09fe, 27),
            new Range16 (0x0a01, 0x0a03, 1),
            new Range16 (0x0a3c, 0x0a3e, 2),
            new Range16 (0x0a3f, 0x0a42, 1),
            new Range16 (0x0a47, 0x0a48, 1),
            new Range16 (0x0a4b, 0x0a4d, 1),
            new Range16 (0x0a51, 0x0a70, 31),
            new Range16 (0x0a71, 0x0a75, 4),
            new Range16 (0x0a81, 0x0a83, 1),
            new Range16 (0x0abc, 0x0abe, 2),
            new Range16 (0x0abf, 0x0ac5, 1),
            new Range16 (0x0ac7, 0x0ac9, 1),
            new Range16 (0x0acb, 0x0acd, 1),
            new Range16 (0x0ae2, 0x0ae3, 1),
            new Range16 (0x0afa, 0x0aff, 1),
            new Range16 (0x0b01, 0x0b03, 1),
            new Range16 (0x0b3c, 0x0b3e, 2),
            new Range16 (0x0b3f, 0x0b44, 1),
            new Range16 (0x0b47, 0x0b48, 1),
            new Range16 (0x0b4b, 0x0b4d, 1),
            new Range16 (0x0b55, 0x0b57, 1),
            new Range16 (0x0b62, 0x0b63, 1),
            new Range16 (0x0b82, 0x0bbe, 60),
            new Range16 (0x0bbf, 0x0bc2, 1),
            new Range16 (0x0bc6, 0x0bc8, 1),
            new Range16 (0x0bca, 0x0bcd, 1),
            new Range16 (0x0bd7, 0x0c00, 41),
            new Range16 (0x0c01, 0x0c04, 1),
            new Range16 (0x0c3c, 0x0c3e, 2),
            new Range16 (0x0c3f, 0x0c44, 1),
            new Range16 (0x0c46, 0x0c48, 1),
            new Range16 (0x0c4a, 0x0c4d, 1),
            new Range16 (0x0c55, 0x0c56, 1),
            new Range16 (0x0c62, 0x0c63, 1),
            new Range16 (0x0c81, 0x0c83, 1),
            new Range16 (0x0cbc, 0x0cbe, 2),
            new Range16 (0x0cbf, 0x0cc4, 1),
            new Range16 (0x0cc6, 0x0cc8, 1),
            new Range16 (0x0cca, 0x0ccd, 1),
            new Range16 (0x0cd5, 0x0cd6, 1),
            new Range16 (0x0ce2, 0x0ce3, 1),
            new Range16 (0x0cf3, 0x0d00, 13),
            new Range16 (0x0d01, 0x0d03, 1),
            new Range16 (0x0d3b, 0x0d3c, 1),
            new Range16 (0x0d3e, 0x0d44, 1),
            new Range16 (0x0d46, 0x0d48, 1),
            new Range16 (0x0d4a, 0x0d4d, 1),
            new Range16 (0x0d57, 0x0d62, 11),
            new Range16 (0x0d63, 0x0d81, 30),
            new Range16 (0x0d82, 0x0d83, 1),
            new Range16 (0x0dca, 0x0dcf, 5),
            new Range16 (0x0dd0, 0x0dd4, 1),
            new Range16 (0x0dd6, 0x0dd8, 2),
            new Range16 (0x0dd9, 0x0ddf, 1),
            new Range16 (0x0df2, 0x0df3, 1),
            new Range16 (0x0e31, 0x0e34, 3),
            new Range16 (0x0e35, 0x0e3a, 1),
            new Range16 (0x0e47, 0x0e4e, 1),
            new Range16 (0x0eb1, 0x0eb4, 3),
            new Range16 (0x0eb5, 0x0ebc, 1),
            new Range16 (0x0ec8, 0x0ece, 1),
            new Range16 (0x0f18, 0x0f19, 1),
            new Range16 (0x0f35, 0x0f39, 2),
            new Range16 (0x0f3e, 0x0f3f, 1),
            new Range16 (0x0f71, 0x0f84, 1),
            new Range16 (0x0f86, 0x0f87, 1),
            new Range16 (0x0f8d, 0x0f97, 1),
            new Range16 (0x0f99, 0x0fbc, 1),
            new Range16 (0x0fc6, 0x102b, 101),
            new Range16 (0x102c, 0x103e, 1),
            new Range16 (0x1056, 0x1059, 1),
            new Range16 (0x105e, 0x1060, 1),
            new Range16 (0x1062, 0x1064, 1),
            new Range16 (0x1067, 0x106d, 1),
            new Range16 (0x1071, 0x1074, 1),
            new Range16 (0x1082, 0x108d, 1),
            new Range16 (0x108f, 0x109a, 11),
            new Range16 (0x109b, 0x109d, 1),
            new Range16 (0x135d, 0x135f, 1),
            new Range16 (0x1712, 0x1715, 1),
            new Range16 (0x1732, 0x1734, 1),
            new Range16 (0x1752, 0x1753, 1),
            new Range16 (0x1772, 0x1773, 1),
            new Range16 (0x17b4, 0x17d3, 1),
            new Range16 (0x17dd, 0x180b, 46),
            new Range16 (0x180c, 0x180d, 1),
            new Range16 (0x180f, 0x1885, 118),
            new Range16 (0x1886, 0x18a9, 35),
            new Range16 (0x1920, 0x192b, 1),
            new Range16 (0x1930, 0x193b, 1),
            new Range16 (0x1a17, 0x1a1b, 1),
            new Range16 (0x1a55, 0x1a5e, 1),
            new Range16 (0x1a60, 0x1a7c, 1),
            new Range16 (0x1a7f, 0x1ab0, 49),
            new Range16 (0x1ab1, 0x1ace, 1),
            new Range16 (0x1b00, 0x1b04, 1),
            new Range16 (0x1b34, 0x1b44, 1),
            new Range16 (0x1b6b, 0x1b73, 1),
            new Range16 (0x1b80, 0x1b82, 1),
            new Range16 (0x1ba1, 0x1bad, 1),
            new Range16 (0x1be6, 0x1bf3, 1),
            new Range16 (0x1c24, 0x1c37, 1),
            new Range16 (0x1cd0, 0x1cd2, 1),
            new Range16 (0x1cd4, 0x1ce8, 1),
            new Range16 (0x1ced, 0x1cf4, 7),
            new Range16 (0x1cf7, 0x1cf9, 1),
            new Range16 (0x1dc0, 0x1dff, 1),
            new Range16 (0x20d0, 0x20f0, 1),
            new Range16 (0x2cef, 0x2cf1, 1),
            new Range16 (0x2d7f, 0x2de0, 97),
            new Range16 (0x2de1, 0x2dff, 1),
            new Range16 (0x302a, 0x302f, 1),
            new Range16 (0x3099, 0x309a, 1),
            new Range16 (0xa66f, 0xa672, 1),
            new Range16 (0xa674, 0xa67d, 1),
            new Range16 (0xa69e, 0xa69f, 1),
            new Range16 (0xa6f0, 0xa6f1, 1),
            new Range16 (0xa802, 0xa806, 4),
            new Range16 (0xa80b, 0xa823, 24),
            new Range16 (0xa824, 0xa827, 1),
            new Range16 (0xa82c, 0xa880, 84),
            new Range16 (0xa881, 0xa8b4, 51),
            new Range16 (0xa8b5, 0xa8c5, 1),
            new Range16 (0xa8e0, 0xa8f1, 1),
            new Range16 (0xa8ff, 0xa926, 39),
            new Range16 (0xa927, 0xa92d, 1),
            new Range16 (0xa947, 0xa953, 1),
            new Range16 (0xa980, 0xa983, 1),
            new Range16 (0xa9b3, 0xa9c0, 1),
            new Range16 (0xa9e5, 0xaa29, 68),
            new Range16 (0xaa2a, 0xaa36, 1),
            new Range16 (0xaa43, 0xaa4c, 9),
            new Range16 (0xaa4d, 0xaa7b, 46),
            new Range16 (0xaa7c, 0xaa7d, 1),
            new Range16 (0xaab0, 0xaab2, 2),
            new Range16 (0xaab3, 0xaab4, 1),
            new Range16 (0xaab7, 0xaab8, 1),
            new Range16 (0xaabe, 0xaabf, 1),
            new Range16 (0xaac1, 0xaaeb, 42),
            new Range16 (0xaaec, 0xaaef, 1),
            new Range16 (0xaaf5, 0xaaf6, 1),
            new Range16 (0xabe3, 0xabea, 1),
            new Range16 (0xabec, 0xabed, 1),
            new Range16 (0xfb1e, 0xfe00, 738),
            new Range16 (0xfe01, 0xfe0f, 1),
            new Range16 (0xfe20, 0xfe2f, 1),
                },
                r32: new Range32[] {
            new Range32 (0x101fd, 0x102e0, 227),
            new Range32 (0x10376, 0x1037a, 1),
            new Range32 (0x10a01, 0x10a03, 1),
            new Range32 (0x10a05, 0x10a06, 1),
            new Range32 (0x10a0c, 0x10a0f, 1),
            new Range32 (0x10a38, 0x10a3a, 1),
            new Range32 (0x10a3f, 0x10ae5, 166),
            new Range32 (0x10ae6, 0x10d24, 574),
            new Range32 (0x10d25, 0x10d27, 1),
            new Range32 (0x10eab, 0x10eac, 1),
            new Range32 (0x10efd, 0x10eff, 1),
            new Range32 (0x10f46, 0x10f50, 1),
            new Range32 (0x10f82, 0x10f85, 1),
            new Range32 (0x11000, 0x11002, 1),
            new Range32 (0x11038, 0x11046, 1),
            new Range32 (0x11070, 0x11073, 3),
            new Range32 (0x11074, 0x1107f, 11),
            new Range32 (0x11080, 0x11082, 1),
            new Range32 (0x110b0, 0x110ba, 1),
            new Range32 (0x110c2, 0x11100, 62),
            new Range32 (0x11101, 0x11102, 1),
            new Range32 (0x11127, 0x11134, 1),
            new Range32 (0x11145, 0x11146, 1),
            new Range32 (0x11173, 0x11180, 13),
            new Range32 (0x11181, 0x11182, 1),
            new Range32 (0x111b3, 0x111c0, 1),
            new Range32 (0x111c9, 0x111cc, 1),
            new Range32 (0x111ce, 0x111cf, 1),
            new Range32 (0x1122c, 0x11237, 1),
            new Range32 (0x1123e, 0x11241, 3),
            new Range32 (0x112df, 0x112ea, 1),
            new Range32 (0x11300, 0x11303, 1),
            new Range32 (0x1133b, 0x1133c, 1),
            new Range32 (0x1133e, 0x11344, 1),
            new Range32 (0x11347, 0x11348, 1),
            new Range32 (0x1134b, 0x1134d, 1),
            new Range32 (0x11357, 0x11362, 11),
            new Range32 (0x11363, 0x11366, 3),
            new Range32 (0x11367, 0x1136c, 1),
            new Range32 (0x11370, 0x11374, 1),
            new Range32 (0x11435, 0x11446, 1),
            new Range32 (0x1145e, 0x114b0, 82),
            new Range32 (0x114b1, 0x114c3, 1),
            new Range32 (0x115af, 0x115b5, 1),
            new Range32 (0x115b8, 0x115c0, 1),
            new Range32 (0x115dc, 0x115dd, 1),
            new Range32 (0x11630, 0x11640, 1),
            new Range32 (0x116ab, 0x116b7, 1),
            new Range32 (0x1171d, 0x1172b, 1),
            new Range32 (0x1182c, 0x1183a, 1),
            new Range32 (0x11930, 0x11935, 1),
            new Range32 (0x11937, 0x11938, 1),
            new Range32 (0x1193b, 0x1193e, 1),
            new Range32 (0x11940, 0x11942, 2),
            new Range32 (0x11943, 0x119d1, 142),
            new Range32 (0x119d2, 0x119d7, 1),
            new Range32 (0x119da, 0x119e0, 1),
            new Range32 (0x119e4, 0x11a01, 29),
            new Range32 (0x11a02, 0x11a0a, 1),
            new Range32 (0x11a33, 0x11a39, 1),
            new Range32 (0x11a3b, 0x11a3e, 1),
            new Range32 (0x11a47, 0x11a51, 10),
            new Range32 (0x11a52, 0x11a5b, 1),
            new Range32 (0x11a8a, 0x11a99, 1),
            new Range32 (0x11c2f, 0x11c36, 1),
            new Range32 (0x11c38, 0x11c3f, 1),
            new Range32 (0x11c92, 0x11ca7, 1),
            new Range32 (0x11ca9, 0x11cb6, 1),
            new Range32 (0x11d31, 0x11d36, 1),
            new Range32 (0x11d3a, 0x11d3c, 2),
            new Range32 (0x11d3d, 0x11d3f, 2),
            new Range32 (0x11d40, 0x11d45, 1),
            new Range32 (0x11d47, 0x11d8a, 67),
            new Range32 (0x11d8b, 0x11d8e, 1),
            new Range32 (0x11d90, 0x11d91, 1),
            new Range32 (0x11d93, 0x11d97, 1),
            new Range32 (0x11ef3, 0x11ef6, 1),
            new Range32 (0x11f00, 0x11f01, 1),
            new Range32 (0x11f03, 0x11f34, 49),
            new Range32 (0x11f35, 0x11f3a, 1),
            new Range32 (0x11f3e, 0x11f42, 1),
            new Range32 (0x13440, 0x13447, 7),
            new Range32 (0x13448, 0x13455, 1),
            new Range32 (0x16af0, 0x16af4, 1),
            new Range32 (0x16b30, 0x16b36, 1),
            new Range32 (0x16f4f, 0x16f51, 2),
            new Range32 (0x16f52, 0x16f87, 1),
            new Range32 (0x16f8f, 0x16f92, 1),
            new Range32 (0x16fe4, 0x16ff0, 12),
            new Range32 (0x16ff1, 0x1bc9d, 19628),
            new Range32 (0x1bc9e, 0x1cf00, 4706),
            new Range32 (0x1cf01, 0x1cf2d, 1),
            new Range32 (0x1cf30, 0x1cf46, 1),
            new Range32 (0x1d165, 0x1d169, 1),
            new Range32 (0x1d16d, 0x1d172, 1),
            new Range32 (0x1d17b, 0x1d182, 1),
            new Range32 (0x1d185, 0x1d18b, 1),
            new Range32 (0x1d1aa, 0x1d1ad, 1),
            new Range32 (0x1d242, 0x1d244, 1),
            new Range32 (0x1da00, 0x1da36, 1),
            new Range32 (0x1da3b, 0x1da6c, 1),
            new Range32 (0x1da75, 0x1da84, 15),
            new Range32 (0x1da9b, 0x1da9f, 1),
            new Range32 (0x1daa1, 0x1daaf, 1),
            new Range32 (0x1e000, 0x1e006, 1),
            new Range32 (0x1e008, 0x1e018, 1),
            new Range32 (0x1e01b, 0x1e021, 1),
            new Range32 (0x1e023, 0x1e024, 1),
            new Range32 (0x1e026, 0x1e02a, 1),
            new Range32 (0x1e08f, 0x1e130, 161),
            new Range32 (0x1e131, 0x1e136, 1),
            new Range32 (0x1e2ae, 0x1e2ec, 62),
            new Range32 (0x1e2ed, 0x1e2ef, 1),
            new Range32 (0x1e4ec, 0x1e4ef, 1),
            new Range32 (0x1e8d0, 0x1e8d6, 1),
            new Range32 (0x1e944, 0x1e94a, 1),
            new Range32 (0xe0100, 0xe01ef, 1),
                }
            );

            internal static RangeTable _Mc = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0903, 0x093b, 56),
            new Range16 (0x093e, 0x0940, 1),
            new Range16 (0x0949, 0x094c, 1),
            new Range16 (0x094e, 0x094f, 1),
            new Range16 (0x0982, 0x0983, 1),
            new Range16 (0x09be, 0x09c0, 1),
            new Range16 (0x09c7, 0x09c8, 1),
            new Range16 (0x09cb, 0x09cc, 1),
            new Range16 (0x09d7, 0x0a03, 44),
            new Range16 (0x0a3e, 0x0a40, 1),
            new Range16 (0x0a83, 0x0abe, 59),
            new Range16 (0x0abf, 0x0ac0, 1),
            new Range16 (0x0ac9, 0x0acb, 2),
            new Range16 (0x0acc, 0x0b02, 54),
            new Range16 (0x0b03, 0x0b3e, 59),
            new Range16 (0x0b40, 0x0b47, 7),
            new Range16 (0x0b48, 0x0b4b, 3),
            new Range16 (0x0b4c, 0x0b57, 11),
            new Range16 (0x0bbe, 0x0bbf, 1),
            new Range16 (0x0bc1, 0x0bc2, 1),
            new Range16 (0x0bc6, 0x0bc8, 1),
            new Range16 (0x0bca, 0x0bcc, 1),
            new Range16 (0x0bd7, 0x0c01, 42),
            new Range16 (0x0c02, 0x0c03, 1),
            new Range16 (0x0c41, 0x0c44, 1),
            new Range16 (0x0c82, 0x0c83, 1),
            new Range16 (0x0cbe, 0x0cc0, 2),
            new Range16 (0x0cc1, 0x0cc4, 1),
            new Range16 (0x0cc7, 0x0cc8, 1),
            new Range16 (0x0cca, 0x0ccb, 1),
            new Range16 (0x0cd5, 0x0cd6, 1),
            new Range16 (0x0cf3, 0x0d02, 15),
            new Range16 (0x0d03, 0x0d3e, 59),
            new Range16 (0x0d3f, 0x0d40, 1),
            new Range16 (0x0d46, 0x0d48, 1),
            new Range16 (0x0d4a, 0x0d4c, 1),
            new Range16 (0x0d57, 0x0d82, 43),
            new Range16 (0x0d83, 0x0dcf, 76),
            new Range16 (0x0dd0, 0x0dd1, 1),
            new Range16 (0x0dd8, 0x0ddf, 1),
            new Range16 (0x0df2, 0x0df3, 1),
            new Range16 (0x0f3e, 0x0f3f, 1),
            new Range16 (0x0f7f, 0x102b, 172),
            new Range16 (0x102c, 0x1031, 5),
            new Range16 (0x1038, 0x103b, 3),
            new Range16 (0x103c, 0x1056, 26),
            new Range16 (0x1057, 0x1062, 11),
            new Range16 (0x1063, 0x1064, 1),
            new Range16 (0x1067, 0x106d, 1),
            new Range16 (0x1083, 0x1084, 1),
            new Range16 (0x1087, 0x108c, 1),
            new Range16 (0x108f, 0x109a, 11),
            new Range16 (0x109b, 0x109c, 1),
            new Range16 (0x1715, 0x1734, 31),
            new Range16 (0x17b6, 0x17be, 8),
            new Range16 (0x17bf, 0x17c5, 1),
            new Range16 (0x17c7, 0x17c8, 1),
            new Range16 (0x1923, 0x1926, 1),
            new Range16 (0x1929, 0x192b, 1),
            new Range16 (0x1930, 0x1931, 1),
            new Range16 (0x1933, 0x1938, 1),
            new Range16 (0x1a19, 0x1a1a, 1),
            new Range16 (0x1a55, 0x1a57, 2),
            new Range16 (0x1a61, 0x1a63, 2),
            new Range16 (0x1a64, 0x1a6d, 9),
            new Range16 (0x1a6e, 0x1a72, 1),
            new Range16 (0x1b04, 0x1b35, 49),
            new Range16 (0x1b3b, 0x1b3d, 2),
            new Range16 (0x1b3e, 0x1b41, 1),
            new Range16 (0x1b43, 0x1b44, 1),
            new Range16 (0x1b82, 0x1ba1, 31),
            new Range16 (0x1ba6, 0x1ba7, 1),
            new Range16 (0x1baa, 0x1be7, 61),
            new Range16 (0x1bea, 0x1bec, 1),
            new Range16 (0x1bee, 0x1bf2, 4),
            new Range16 (0x1bf3, 0x1c24, 49),
            new Range16 (0x1c25, 0x1c2b, 1),
            new Range16 (0x1c34, 0x1c35, 1),
            new Range16 (0x1ce1, 0x1cf7, 22),
            new Range16 (0x302e, 0x302f, 1),
            new Range16 (0xa823, 0xa824, 1),
            new Range16 (0xa827, 0xa880, 89),
            new Range16 (0xa881, 0xa8b4, 51),
            new Range16 (0xa8b5, 0xa8c3, 1),
            new Range16 (0xa952, 0xa953, 1),
            new Range16 (0xa983, 0xa9b4, 49),
            new Range16 (0xa9b5, 0xa9ba, 5),
            new Range16 (0xa9bb, 0xa9be, 3),
            new Range16 (0xa9bf, 0xa9c0, 1),
            new Range16 (0xaa2f, 0xaa30, 1),
            new Range16 (0xaa33, 0xaa34, 1),
            new Range16 (0xaa4d, 0xaa7b, 46),
            new Range16 (0xaa7d, 0xaaeb, 110),
            new Range16 (0xaaee, 0xaaef, 1),
            new Range16 (0xaaf5, 0xabe3, 238),
            new Range16 (0xabe4, 0xabe6, 2),
            new Range16 (0xabe7, 0xabe9, 2),
            new Range16 (0xabea, 0xabec, 2),
                },
                r32: new Range32[] {
            new Range32 (0x11000, 0x11002, 2),
            new Range32 (0x11082, 0x110b0, 46),
            new Range32 (0x110b1, 0x110b2, 1),
            new Range32 (0x110b7, 0x110b8, 1),
            new Range32 (0x1112c, 0x11145, 25),
            new Range32 (0x11146, 0x11182, 60),
            new Range32 (0x111b3, 0x111b5, 1),
            new Range32 (0x111bf, 0x111c0, 1),
            new Range32 (0x111ce, 0x1122c, 94),
            new Range32 (0x1122d, 0x1122e, 1),
            new Range32 (0x11232, 0x11233, 1),
            new Range32 (0x11235, 0x112e0, 171),
            new Range32 (0x112e1, 0x112e2, 1),
            new Range32 (0x11302, 0x11303, 1),
            new Range32 (0x1133e, 0x1133f, 1),
            new Range32 (0x11341, 0x11344, 1),
            new Range32 (0x11347, 0x11348, 1),
            new Range32 (0x1134b, 0x1134d, 1),
            new Range32 (0x11357, 0x11362, 11),
            new Range32 (0x11363, 0x11435, 210),
            new Range32 (0x11436, 0x11437, 1),
            new Range32 (0x11440, 0x11441, 1),
            new Range32 (0x11445, 0x114b0, 107),
            new Range32 (0x114b1, 0x114b2, 1),
            new Range32 (0x114b9, 0x114bb, 2),
            new Range32 (0x114bc, 0x114be, 1),
            new Range32 (0x114c1, 0x115af, 238),
            new Range32 (0x115b0, 0x115b1, 1),
            new Range32 (0x115b8, 0x115bb, 1),
            new Range32 (0x115be, 0x11630, 114),
            new Range32 (0x11631, 0x11632, 1),
            new Range32 (0x1163b, 0x1163c, 1),
            new Range32 (0x1163e, 0x116ac, 110),
            new Range32 (0x116ae, 0x116af, 1),
            new Range32 (0x116b6, 0x11720, 106),
            new Range32 (0x11721, 0x11726, 5),
            new Range32 (0x1182c, 0x1182e, 1),
            new Range32 (0x11838, 0x11930, 248),
            new Range32 (0x11931, 0x11935, 1),
            new Range32 (0x11937, 0x11938, 1),
            new Range32 (0x1193d, 0x11940, 3),
            new Range32 (0x11942, 0x119d1, 143),
            new Range32 (0x119d2, 0x119d3, 1),
            new Range32 (0x119dc, 0x119df, 1),
            new Range32 (0x119e4, 0x11a39, 85),
            new Range32 (0x11a57, 0x11a58, 1),
            new Range32 (0x11a97, 0x11c2f, 408),
            new Range32 (0x11c3e, 0x11ca9, 107),
            new Range32 (0x11cb1, 0x11cb4, 3),
            new Range32 (0x11d8a, 0x11d8e, 1),
            new Range32 (0x11d93, 0x11d94, 1),
            new Range32 (0x11d96, 0x11ef5, 351),
            new Range32 (0x11ef6, 0x11f03, 13),
            new Range32 (0x11f34, 0x11f35, 1),
            new Range32 (0x11f3e, 0x11f3f, 1),
            new Range32 (0x11f41, 0x16f51, 20496),
            new Range32 (0x16f52, 0x16f87, 1),
            new Range32 (0x16ff0, 0x16ff1, 1),
            new Range32 (0x1d165, 0x1d166, 1),
            new Range32 (0x1d16d, 0x1d172, 1),
                }
            );

            internal static RangeTable _Me = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0488, 0x0489, 1),
            new Range16 (0x1abe, 0x20dd, 1567),
            new Range16 (0x20de, 0x20e0, 1),
            new Range16 (0x20e2, 0x20e4, 1),
            new Range16 (0xa670, 0xa672, 1),
                }
            );

            internal static RangeTable _Mn = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0300, 0x036f, 1),
            new Range16 (0x0483, 0x0487, 1),
            new Range16 (0x0591, 0x05bd, 1),
            new Range16 (0x05bf, 0x05c1, 2),
            new Range16 (0x05c2, 0x05c4, 2),
            new Range16 (0x05c5, 0x05c7, 2),
            new Range16 (0x0610, 0x061a, 1),
            new Range16 (0x064b, 0x065f, 1),
            new Range16 (0x0670, 0x06d6, 102),
            new Range16 (0x06d7, 0x06dc, 1),
            new Range16 (0x06df, 0x06e4, 1),
            new Range16 (0x06e7, 0x06e8, 1),
            new Range16 (0x06ea, 0x06ed, 1),
            new Range16 (0x0711, 0x0730, 31),
            new Range16 (0x0731, 0x074a, 1),
            new Range16 (0x07a6, 0x07b0, 1),
            new Range16 (0x07eb, 0x07f3, 1),
            new Range16 (0x07fd, 0x0816, 25),
            new Range16 (0x0817, 0x0819, 1),
            new Range16 (0x081b, 0x0823, 1),
            new Range16 (0x0825, 0x0827, 1),
            new Range16 (0x0829, 0x082d, 1),
            new Range16 (0x0859, 0x085b, 1),
            new Range16 (0x0898, 0x089f, 1),
            new Range16 (0x08ca, 0x08e1, 1),
            new Range16 (0x08e3, 0x0902, 1),
            new Range16 (0x093a, 0x093c, 2),
            new Range16 (0x0941, 0x0948, 1),
            new Range16 (0x094d, 0x0951, 4),
            new Range16 (0x0952, 0x0957, 1),
            new Range16 (0x0962, 0x0963, 1),
            new Range16 (0x0981, 0x09bc, 59),
            new Range16 (0x09c1, 0x09c4, 1),
            new Range16 (0x09cd, 0x09e2, 21),
            new Range16 (0x09e3, 0x09fe, 27),
            new Range16 (0x0a01, 0x0a02, 1),
            new Range16 (0x0a3c, 0x0a41, 5),
            new Range16 (0x0a42, 0x0a47, 5),
            new Range16 (0x0a48, 0x0a4b, 3),
            new Range16 (0x0a4c, 0x0a4d, 1),
            new Range16 (0x0a51, 0x0a70, 31),
            new Range16 (0x0a71, 0x0a75, 4),
            new Range16 (0x0a81, 0x0a82, 1),
            new Range16 (0x0abc, 0x0ac1, 5),
            new Range16 (0x0ac2, 0x0ac5, 1),
            new Range16 (0x0ac7, 0x0ac8, 1),
            new Range16 (0x0acd, 0x0ae2, 21),
            new Range16 (0x0ae3, 0x0afa, 23),
            new Range16 (0x0afb, 0x0aff, 1),
            new Range16 (0x0b01, 0x0b3c, 59),
            new Range16 (0x0b3f, 0x0b41, 2),
            new Range16 (0x0b42, 0x0b44, 1),
            new Range16 (0x0b4d, 0x0b55, 8),
            new Range16 (0x0b56, 0x0b62, 12),
            new Range16 (0x0b63, 0x0b82, 31),
            new Range16 (0x0bc0, 0x0bcd, 13),
            new Range16 (0x0c00, 0x0c04, 4),
            new Range16 (0x0c3c, 0x0c3e, 2),
            new Range16 (0x0c3f, 0x0c40, 1),
            new Range16 (0x0c46, 0x0c48, 1),
            new Range16 (0x0c4a, 0x0c4d, 1),
            new Range16 (0x0c55, 0x0c56, 1),
            new Range16 (0x0c62, 0x0c63, 1),
            new Range16 (0x0c81, 0x0cbc, 59),
            new Range16 (0x0cbf, 0x0cc6, 7),
            new Range16 (0x0ccc, 0x0ccd, 1),
            new Range16 (0x0ce2, 0x0ce3, 1),
            new Range16 (0x0d00, 0x0d01, 1),
            new Range16 (0x0d3b, 0x0d3c, 1),
            new Range16 (0x0d41, 0x0d44, 1),
            new Range16 (0x0d4d, 0x0d62, 21),
            new Range16 (0x0d63, 0x0d81, 30),
            new Range16 (0x0dca, 0x0dd2, 8),
            new Range16 (0x0dd3, 0x0dd4, 1),
            new Range16 (0x0dd6, 0x0e31, 91),
            new Range16 (0x0e34, 0x0e3a, 1),
            new Range16 (0x0e47, 0x0e4e, 1),
            new Range16 (0x0eb1, 0x0eb4, 3),
            new Range16 (0x0eb5, 0x0ebc, 1),
            new Range16 (0x0ec8, 0x0ece, 1),
            new Range16 (0x0f18, 0x0f19, 1),
            new Range16 (0x0f35, 0x0f39, 2),
            new Range16 (0x0f71, 0x0f7e, 1),
            new Range16 (0x0f80, 0x0f84, 1),
            new Range16 (0x0f86, 0x0f87, 1),
            new Range16 (0x0f8d, 0x0f97, 1),
            new Range16 (0x0f99, 0x0fbc, 1),
            new Range16 (0x0fc6, 0x102d, 103),
            new Range16 (0x102e, 0x1030, 1),
            new Range16 (0x1032, 0x1037, 1),
            new Range16 (0x1039, 0x103a, 1),
            new Range16 (0x103d, 0x103e, 1),
            new Range16 (0x1058, 0x1059, 1),
            new Range16 (0x105e, 0x1060, 1),
            new Range16 (0x1071, 0x1074, 1),
            new Range16 (0x1082, 0x1085, 3),
            new Range16 (0x1086, 0x108d, 7),
            new Range16 (0x109d, 0x135d, 704),
            new Range16 (0x135e, 0x135f, 1),
            new Range16 (0x1712, 0x1714, 1),
            new Range16 (0x1732, 0x1733, 1),
            new Range16 (0x1752, 0x1753, 1),
            new Range16 (0x1772, 0x1773, 1),
            new Range16 (0x17b4, 0x17b5, 1),
            new Range16 (0x17b7, 0x17bd, 1),
            new Range16 (0x17c6, 0x17c9, 3),
            new Range16 (0x17ca, 0x17d3, 1),
            new Range16 (0x17dd, 0x180b, 46),
            new Range16 (0x180c, 0x180d, 1),
            new Range16 (0x180f, 0x1885, 118),
            new Range16 (0x1886, 0x18a9, 35),
            new Range16 (0x1920, 0x1922, 1),
            new Range16 (0x1927, 0x1928, 1),
            new Range16 (0x1932, 0x1939, 7),
            new Range16 (0x193a, 0x193b, 1),
            new Range16 (0x1a17, 0x1a18, 1),
            new Range16 (0x1a1b, 0x1a56, 59),
            new Range16 (0x1a58, 0x1a5e, 1),
            new Range16 (0x1a60, 0x1a62, 2),
            new Range16 (0x1a65, 0x1a6c, 1),
            new Range16 (0x1a73, 0x1a7c, 1),
            new Range16 (0x1a7f, 0x1ab0, 49),
            new Range16 (0x1ab1, 0x1abd, 1),
            new Range16 (0x1abf, 0x1ace, 1),
            new Range16 (0x1b00, 0x1b03, 1),
            new Range16 (0x1b34, 0x1b36, 2),
            new Range16 (0x1b37, 0x1b3a, 1),
            new Range16 (0x1b3c, 0x1b42, 6),
            new Range16 (0x1b6b, 0x1b73, 1),
            new Range16 (0x1b80, 0x1b81, 1),
            new Range16 (0x1ba2, 0x1ba5, 1),
            new Range16 (0x1ba8, 0x1ba9, 1),
            new Range16 (0x1bab, 0x1bad, 1),
            new Range16 (0x1be6, 0x1be8, 2),
            new Range16 (0x1be9, 0x1bed, 4),
            new Range16 (0x1bef, 0x1bf1, 1),
            new Range16 (0x1c2c, 0x1c33, 1),
            new Range16 (0x1c36, 0x1c37, 1),
            new Range16 (0x1cd0, 0x1cd2, 1),
            new Range16 (0x1cd4, 0x1ce0, 1),
            new Range16 (0x1ce2, 0x1ce8, 1),
            new Range16 (0x1ced, 0x1cf4, 7),
            new Range16 (0x1cf8, 0x1cf9, 1),
            new Range16 (0x1dc0, 0x1dff, 1),
            new Range16 (0x20d0, 0x20dc, 1),
            new Range16 (0x20e1, 0x20e5, 4),
            new Range16 (0x20e6, 0x20f0, 1),
            new Range16 (0x2cef, 0x2cf1, 1),
            new Range16 (0x2d7f, 0x2de0, 97),
            new Range16 (0x2de1, 0x2dff, 1),
            new Range16 (0x302a, 0x302d, 1),
            new Range16 (0x3099, 0x309a, 1),
            new Range16 (0xa66f, 0xa674, 5),
            new Range16 (0xa675, 0xa67d, 1),
            new Range16 (0xa69e, 0xa69f, 1),
            new Range16 (0xa6f0, 0xa6f1, 1),
            new Range16 (0xa802, 0xa806, 4),
            new Range16 (0xa80b, 0xa825, 26),
            new Range16 (0xa826, 0xa82c, 6),
            new Range16 (0xa8c4, 0xa8c5, 1),
            new Range16 (0xa8e0, 0xa8f1, 1),
            new Range16 (0xa8ff, 0xa926, 39),
            new Range16 (0xa927, 0xa92d, 1),
            new Range16 (0xa947, 0xa951, 1),
            new Range16 (0xa980, 0xa982, 1),
            new Range16 (0xa9b3, 0xa9b6, 3),
            new Range16 (0xa9b7, 0xa9b9, 1),
            new Range16 (0xa9bc, 0xa9bd, 1),
            new Range16 (0xa9e5, 0xaa29, 68),
            new Range16 (0xaa2a, 0xaa2e, 1),
            new Range16 (0xaa31, 0xaa32, 1),
            new Range16 (0xaa35, 0xaa36, 1),
            new Range16 (0xaa43, 0xaa4c, 9),
            new Range16 (0xaa7c, 0xaab0, 52),
            new Range16 (0xaab2, 0xaab4, 1),
            new Range16 (0xaab7, 0xaab8, 1),
            new Range16 (0xaabe, 0xaabf, 1),
            new Range16 (0xaac1, 0xaaec, 43),
            new Range16 (0xaaed, 0xaaf6, 9),
            new Range16 (0xabe5, 0xabe8, 3),
            new Range16 (0xabed, 0xfb1e, 20273),
            new Range16 (0xfe00, 0xfe0f, 1),
            new Range16 (0xfe20, 0xfe2f, 1),
                },
                r32: new Range32[] {
            new Range32 (0x101fd, 0x102e0, 227),
            new Range32 (0x10376, 0x1037a, 1),
            new Range32 (0x10a01, 0x10a03, 1),
            new Range32 (0x10a05, 0x10a06, 1),
            new Range32 (0x10a0c, 0x10a0f, 1),
            new Range32 (0x10a38, 0x10a3a, 1),
            new Range32 (0x10a3f, 0x10ae5, 166),
            new Range32 (0x10ae6, 0x10d24, 574),
            new Range32 (0x10d25, 0x10d27, 1),
            new Range32 (0x10eab, 0x10eac, 1),
            new Range32 (0x10efd, 0x10eff, 1),
            new Range32 (0x10f46, 0x10f50, 1),
            new Range32 (0x10f82, 0x10f85, 1),
            new Range32 (0x11001, 0x11038, 55),
            new Range32 (0x11039, 0x11046, 1),
            new Range32 (0x11070, 0x11073, 3),
            new Range32 (0x11074, 0x1107f, 11),
            new Range32 (0x11080, 0x11081, 1),
            new Range32 (0x110b3, 0x110b6, 1),
            new Range32 (0x110b9, 0x110ba, 1),
            new Range32 (0x110c2, 0x11100, 62),
            new Range32 (0x11101, 0x11102, 1),
            new Range32 (0x11127, 0x1112b, 1),
            new Range32 (0x1112d, 0x11134, 1),
            new Range32 (0x11173, 0x11180, 13),
            new Range32 (0x11181, 0x111b6, 53),
            new Range32 (0x111b7, 0x111be, 1),
            new Range32 (0x111c9, 0x111cc, 1),
            new Range32 (0x111cf, 0x1122f, 96),
            new Range32 (0x11230, 0x11231, 1),
            new Range32 (0x11234, 0x11236, 2),
            new Range32 (0x11237, 0x1123e, 7),
            new Range32 (0x11241, 0x112df, 158),
            new Range32 (0x112e3, 0x112ea, 1),
            new Range32 (0x11300, 0x11301, 1),
            new Range32 (0x1133b, 0x1133c, 1),
            new Range32 (0x11340, 0x11366, 38),
            new Range32 (0x11367, 0x1136c, 1),
            new Range32 (0x11370, 0x11374, 1),
            new Range32 (0x11438, 0x1143f, 1),
            new Range32 (0x11442, 0x11444, 1),
            new Range32 (0x11446, 0x1145e, 24),
            new Range32 (0x114b3, 0x114b8, 1),
            new Range32 (0x114ba, 0x114bf, 5),
            new Range32 (0x114c0, 0x114c2, 2),
            new Range32 (0x114c3, 0x115b2, 239),
            new Range32 (0x115b3, 0x115b5, 1),
            new Range32 (0x115bc, 0x115bd, 1),
            new Range32 (0x115bf, 0x115c0, 1),
            new Range32 (0x115dc, 0x115dd, 1),
            new Range32 (0x11633, 0x1163a, 1),
            new Range32 (0x1163d, 0x1163f, 2),
            new Range32 (0x11640, 0x116ab, 107),
            new Range32 (0x116ad, 0x116b0, 3),
            new Range32 (0x116b1, 0x116b5, 1),
            new Range32 (0x116b7, 0x1171d, 102),
            new Range32 (0x1171e, 0x1171f, 1),
            new Range32 (0x11722, 0x11725, 1),
            new Range32 (0x11727, 0x1172b, 1),
            new Range32 (0x1182f, 0x11837, 1),
            new Range32 (0x11839, 0x1183a, 1),
            new Range32 (0x1193b, 0x1193c, 1),
            new Range32 (0x1193e, 0x11943, 5),
            new Range32 (0x119d4, 0x119d7, 1),
            new Range32 (0x119da, 0x119db, 1),
            new Range32 (0x119e0, 0x11a01, 33),
            new Range32 (0x11a02, 0x11a0a, 1),
            new Range32 (0x11a33, 0x11a38, 1),
            new Range32 (0x11a3b, 0x11a3e, 1),
            new Range32 (0x11a47, 0x11a51, 10),
            new Range32 (0x11a52, 0x11a56, 1),
            new Range32 (0x11a59, 0x11a5b, 1),
            new Range32 (0x11a8a, 0x11a96, 1),
            new Range32 (0x11a98, 0x11a99, 1),
            new Range32 (0x11c30, 0x11c36, 1),
            new Range32 (0x11c38, 0x11c3d, 1),
            new Range32 (0x11c3f, 0x11c92, 83),
            new Range32 (0x11c93, 0x11ca7, 1),
            new Range32 (0x11caa, 0x11cb0, 1),
            new Range32 (0x11cb2, 0x11cb3, 1),
            new Range32 (0x11cb5, 0x11cb6, 1),
            new Range32 (0x11d31, 0x11d36, 1),
            new Range32 (0x11d3a, 0x11d3c, 2),
            new Range32 (0x11d3d, 0x11d3f, 2),
            new Range32 (0x11d40, 0x11d45, 1),
            new Range32 (0x11d47, 0x11d90, 73),
            new Range32 (0x11d91, 0x11d95, 4),
            new Range32 (0x11d97, 0x11ef3, 348),
            new Range32 (0x11ef4, 0x11f00, 12),
            new Range32 (0x11f01, 0x11f36, 53),
            new Range32 (0x11f37, 0x11f3a, 1),
            new Range32 (0x11f40, 0x11f42, 2),
            new Range32 (0x13440, 0x13447, 7),
            new Range32 (0x13448, 0x13455, 1),
            new Range32 (0x16af0, 0x16af4, 1),
            new Range32 (0x16b30, 0x16b36, 1),
            new Range32 (0x16f4f, 0x16f8f, 64),
            new Range32 (0x16f90, 0x16f92, 1),
            new Range32 (0x16fe4, 0x1bc9d, 19641),
            new Range32 (0x1bc9e, 0x1cf00, 4706),
            new Range32 (0x1cf01, 0x1cf2d, 1),
            new Range32 (0x1cf30, 0x1cf46, 1),
            new Range32 (0x1d167, 0x1d169, 1),
            new Range32 (0x1d17b, 0x1d182, 1),
            new Range32 (0x1d185, 0x1d18b, 1),
            new Range32 (0x1d1aa, 0x1d1ad, 1),
            new Range32 (0x1d242, 0x1d244, 1),
            new Range32 (0x1da00, 0x1da36, 1),
            new Range32 (0x1da3b, 0x1da6c, 1),
            new Range32 (0x1da75, 0x1da84, 15),
            new Range32 (0x1da9b, 0x1da9f, 1),
            new Range32 (0x1daa1, 0x1daaf, 1),
            new Range32 (0x1e000, 0x1e006, 1),
            new Range32 (0x1e008, 0x1e018, 1),
            new Range32 (0x1e01b, 0x1e021, 1),
            new Range32 (0x1e023, 0x1e024, 1),
            new Range32 (0x1e026, 0x1e02a, 1),
            new Range32 (0x1e08f, 0x1e130, 161),
            new Range32 (0x1e131, 0x1e136, 1),
            new Range32 (0x1e2ae, 0x1e2ec, 62),
            new Range32 (0x1e2ed, 0x1e2ef, 1),
            new Range32 (0x1e4ec, 0x1e4ef, 1),
            new Range32 (0x1e8d0, 0x1e8d6, 1),
            new Range32 (0x1e944, 0x1e94a, 1),
            new Range32 (0xe0100, 0xe01ef, 1),
                }
            );

            internal static RangeTable _N = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0030, 0x0039, 1),
            new Range16 (0x00b2, 0x00b3, 1),
            new Range16 (0x00b9, 0x00bc, 3),
            new Range16 (0x00bd, 0x00be, 1),
            new Range16 (0x0660, 0x0669, 1),
            new Range16 (0x06f0, 0x06f9, 1),
            new Range16 (0x07c0, 0x07c9, 1),
            new Range16 (0x0966, 0x096f, 1),
            new Range16 (0x09e6, 0x09ef, 1),
            new Range16 (0x09f4, 0x09f9, 1),
            new Range16 (0x0a66, 0x0a6f, 1),
            new Range16 (0x0ae6, 0x0aef, 1),
            new Range16 (0x0b66, 0x0b6f, 1),
            new Range16 (0x0b72, 0x0b77, 1),
            new Range16 (0x0be6, 0x0bf2, 1),
            new Range16 (0x0c66, 0x0c6f, 1),
            new Range16 (0x0c78, 0x0c7e, 1),
            new Range16 (0x0ce6, 0x0cef, 1),
            new Range16 (0x0d58, 0x0d5e, 1),
            new Range16 (0x0d66, 0x0d78, 1),
            new Range16 (0x0de6, 0x0def, 1),
            new Range16 (0x0e50, 0x0e59, 1),
            new Range16 (0x0ed0, 0x0ed9, 1),
            new Range16 (0x0f20, 0x0f33, 1),
            new Range16 (0x1040, 0x1049, 1),
            new Range16 (0x1090, 0x1099, 1),
            new Range16 (0x1369, 0x137c, 1),
            new Range16 (0x16ee, 0x16f0, 1),
            new Range16 (0x17e0, 0x17e9, 1),
            new Range16 (0x17f0, 0x17f9, 1),
            new Range16 (0x1810, 0x1819, 1),
            new Range16 (0x1946, 0x194f, 1),
            new Range16 (0x19d0, 0x19da, 1),
            new Range16 (0x1a80, 0x1a89, 1),
            new Range16 (0x1a90, 0x1a99, 1),
            new Range16 (0x1b50, 0x1b59, 1),
            new Range16 (0x1bb0, 0x1bb9, 1),
            new Range16 (0x1c40, 0x1c49, 1),
            new Range16 (0x1c50, 0x1c59, 1),
            new Range16 (0x2070, 0x2074, 4),
            new Range16 (0x2075, 0x2079, 1),
            new Range16 (0x2080, 0x2089, 1),
            new Range16 (0x2150, 0x2182, 1),
            new Range16 (0x2185, 0x2189, 1),
            new Range16 (0x2460, 0x249b, 1),
            new Range16 (0x24ea, 0x24ff, 1),
            new Range16 (0x2776, 0x2793, 1),
            new Range16 (0x2cfd, 0x3007, 778),
            new Range16 (0x3021, 0x3029, 1),
            new Range16 (0x3038, 0x303a, 1),
            new Range16 (0x3192, 0x3195, 1),
            new Range16 (0x3220, 0x3229, 1),
            new Range16 (0x3248, 0x324f, 1),
            new Range16 (0x3251, 0x325f, 1),
            new Range16 (0x3280, 0x3289, 1),
            new Range16 (0x32b1, 0x32bf, 1),
            new Range16 (0xa620, 0xa629, 1),
            new Range16 (0xa6e6, 0xa6ef, 1),
            new Range16 (0xa830, 0xa835, 1),
            new Range16 (0xa8d0, 0xa8d9, 1),
            new Range16 (0xa900, 0xa909, 1),
            new Range16 (0xa9d0, 0xa9d9, 1),
            new Range16 (0xa9f0, 0xa9f9, 1),
            new Range16 (0xaa50, 0xaa59, 1),
            new Range16 (0xabf0, 0xabf9, 1),
            new Range16 (0xff10, 0xff19, 1),
                },
                r32: new Range32[] {
            new Range32 (0x10107, 0x10133, 1),
            new Range32 (0x10140, 0x10178, 1),
            new Range32 (0x1018a, 0x1018b, 1),
            new Range32 (0x102e1, 0x102fb, 1),
            new Range32 (0x10320, 0x10323, 1),
            new Range32 (0x10341, 0x1034a, 9),
            new Range32 (0x103d1, 0x103d5, 1),
            new Range32 (0x104a0, 0x104a9, 1),
            new Range32 (0x10858, 0x1085f, 1),
            new Range32 (0x10879, 0x1087f, 1),
            new Range32 (0x108a7, 0x108af, 1),
            new Range32 (0x108fb, 0x108ff, 1),
            new Range32 (0x10916, 0x1091b, 1),
            new Range32 (0x109bc, 0x109bd, 1),
            new Range32 (0x109c0, 0x109cf, 1),
            new Range32 (0x109d2, 0x109ff, 1),
            new Range32 (0x10a40, 0x10a48, 1),
            new Range32 (0x10a7d, 0x10a7e, 1),
            new Range32 (0x10a9d, 0x10a9f, 1),
            new Range32 (0x10aeb, 0x10aef, 1),
            new Range32 (0x10b58, 0x10b5f, 1),
            new Range32 (0x10b78, 0x10b7f, 1),
            new Range32 (0x10ba9, 0x10baf, 1),
            new Range32 (0x10cfa, 0x10cff, 1),
            new Range32 (0x10d30, 0x10d39, 1),
            new Range32 (0x10e60, 0x10e7e, 1),
            new Range32 (0x10f1d, 0x10f26, 1),
            new Range32 (0x10f51, 0x10f54, 1),
            new Range32 (0x10fc5, 0x10fcb, 1),
            new Range32 (0x11052, 0x1106f, 1),
            new Range32 (0x110f0, 0x110f9, 1),
            new Range32 (0x11136, 0x1113f, 1),
            new Range32 (0x111d0, 0x111d9, 1),
            new Range32 (0x111e1, 0x111f4, 1),
            new Range32 (0x112f0, 0x112f9, 1),
            new Range32 (0x11450, 0x11459, 1),
            new Range32 (0x114d0, 0x114d9, 1),
            new Range32 (0x11650, 0x11659, 1),
            new Range32 (0x116c0, 0x116c9, 1),
            new Range32 (0x11730, 0x1173b, 1),
            new Range32 (0x118e0, 0x118f2, 1),
            new Range32 (0x11950, 0x11959, 1),
            new Range32 (0x11c50, 0x11c6c, 1),
            new Range32 (0x11d50, 0x11d59, 1),
            new Range32 (0x11da0, 0x11da9, 1),
            new Range32 (0x11f50, 0x11f59, 1),
            new Range32 (0x11fc0, 0x11fd4, 1),
            new Range32 (0x12400, 0x1246e, 1),
            new Range32 (0x16a60, 0x16a69, 1),
            new Range32 (0x16ac0, 0x16ac9, 1),
            new Range32 (0x16b50, 0x16b59, 1),
            new Range32 (0x16b5b, 0x16b61, 1),
            new Range32 (0x16e80, 0x16e96, 1),
            new Range32 (0x1d2c0, 0x1d2d3, 1),
            new Range32 (0x1d2e0, 0x1d2f3, 1),
            new Range32 (0x1d360, 0x1d378, 1),
            new Range32 (0x1d7ce, 0x1d7ff, 1),
            new Range32 (0x1e140, 0x1e149, 1),
            new Range32 (0x1e2f0, 0x1e2f9, 1),
            new Range32 (0x1e4f0, 0x1e4f9, 1),
            new Range32 (0x1e8c7, 0x1e8cf, 1),
            new Range32 (0x1e950, 0x1e959, 1),
            new Range32 (0x1ec71, 0x1ecab, 1),
            new Range32 (0x1ecad, 0x1ecaf, 1),
            new Range32 (0x1ecb1, 0x1ecb4, 1),
            new Range32 (0x1ed01, 0x1ed2d, 1),
            new Range32 (0x1ed2f, 0x1ed3d, 1),
            new Range32 (0x1f100, 0x1f10c, 1),
            new Range32 (0x1fbf0, 0x1fbf9, 1),
                },
                latinOffset: 4
            );

            internal static RangeTable _Nd = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0030, 0x0039, 1),
            new Range16 (0x0660, 0x0669, 1),
            new Range16 (0x06f0, 0x06f9, 1),
            new Range16 (0x07c0, 0x07c9, 1),
            new Range16 (0x0966, 0x096f, 1),
            new Range16 (0x09e6, 0x09ef, 1),
            new Range16 (0x0a66, 0x0a6f, 1),
            new Range16 (0x0ae6, 0x0aef, 1),
            new Range16 (0x0b66, 0x0b6f, 1),
            new Range16 (0x0be6, 0x0bef, 1),
            new Range16 (0x0c66, 0x0c6f, 1),
            new Range16 (0x0ce6, 0x0cef, 1),
            new Range16 (0x0d66, 0x0d6f, 1),
            new Range16 (0x0de6, 0x0def, 1),
            new Range16 (0x0e50, 0x0e59, 1),
            new Range16 (0x0ed0, 0x0ed9, 1),
            new Range16 (0x0f20, 0x0f29, 1),
            new Range16 (0x1040, 0x1049, 1),
            new Range16 (0x1090, 0x1099, 1),
            new Range16 (0x17e0, 0x17e9, 1),
            new Range16 (0x1810, 0x1819, 1),
            new Range16 (0x1946, 0x194f, 1),
            new Range16 (0x19d0, 0x19d9, 1),
            new Range16 (0x1a80, 0x1a89, 1),
            new Range16 (0x1a90, 0x1a99, 1),
            new Range16 (0x1b50, 0x1b59, 1),
            new Range16 (0x1bb0, 0x1bb9, 1),
            new Range16 (0x1c40, 0x1c49, 1),
            new Range16 (0x1c50, 0x1c59, 1),
            new Range16 (0xa620, 0xa629, 1),
            new Range16 (0xa8d0, 0xa8d9, 1),
            new Range16 (0xa900, 0xa909, 1),
            new Range16 (0xa9d0, 0xa9d9, 1),
            new Range16 (0xa9f0, 0xa9f9, 1),
            new Range16 (0xaa50, 0xaa59, 1),
            new Range16 (0xabf0, 0xabf9, 1),
            new Range16 (0xff10, 0xff19, 1),
                },
                r32: new Range32[] {
            new Range32 (0x104a0, 0x104a9, 1),
            new Range32 (0x10d30, 0x10d39, 1),
            new Range32 (0x11066, 0x1106f, 1),
            new Range32 (0x110f0, 0x110f9, 1),
            new Range32 (0x11136, 0x1113f, 1),
            new Range32 (0x111d0, 0x111d9, 1),
            new Range32 (0x112f0, 0x112f9, 1),
            new Range32 (0x11450, 0x11459, 1),
            new Range32 (0x114d0, 0x114d9, 1),
            new Range32 (0x11650, 0x11659, 1),
            new Range32 (0x116c0, 0x116c9, 1),
            new Range32 (0x11730, 0x11739, 1),
            new Range32 (0x118e0, 0x118e9, 1),
            new Range32 (0x11950, 0x11959, 1),
            new Range32 (0x11c50, 0x11c59, 1),
            new Range32 (0x11d50, 0x11d59, 1),
            new Range32 (0x11da0, 0x11da9, 1),
            new Range32 (0x11f50, 0x11f59, 1),
            new Range32 (0x16a60, 0x16a69, 1),
            new Range32 (0x16ac0, 0x16ac9, 1),
            new Range32 (0x16b50, 0x16b59, 1),
            new Range32 (0x1d7ce, 0x1d7ff, 1),
            new Range32 (0x1e140, 0x1e149, 1),
            new Range32 (0x1e2f0, 0x1e2f9, 1),
            new Range32 (0x1e4f0, 0x1e4f9, 1),
            new Range32 (0x1e950, 0x1e959, 1),
            new Range32 (0x1fbf0, 0x1fbf9, 1),
                },
                latinOffset: 1
            );

            internal static RangeTable _Nl = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x16ee, 0x16f0, 1),
            new Range16 (0x2160, 0x2182, 1),
            new Range16 (0x2185, 0x2188, 1),
            new Range16 (0x3007, 0x3021, 26),
            new Range16 (0x3022, 0x3029, 1),
            new Range16 (0x3038, 0x303a, 1),
            new Range16 (0xa6e6, 0xa6ef, 1),
                },
                r32: new Range32[] {
            new Range32 (0x10140, 0x10174, 1),
            new Range32 (0x10341, 0x1034a, 9),
            new Range32 (0x103d1, 0x103d5, 1),
            new Range32 (0x12400, 0x1246e, 1),
                }
            );

            internal static RangeTable _No = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x00b2, 0x00b3, 1),
            new Range16 (0x00b9, 0x00bc, 3),
            new Range16 (0x00bd, 0x00be, 1),
            new Range16 (0x09f4, 0x09f9, 1),
            new Range16 (0x0b72, 0x0b77, 1),
            new Range16 (0x0bf0, 0x0bf2, 1),
            new Range16 (0x0c78, 0x0c7e, 1),
            new Range16 (0x0d58, 0x0d5e, 1),
            new Range16 (0x0d70, 0x0d78, 1),
            new Range16 (0x0f2a, 0x0f33, 1),
            new Range16 (0x1369, 0x137c, 1),
            new Range16 (0x17f0, 0x17f9, 1),
            new Range16 (0x19da, 0x2070, 1686),
            new Range16 (0x2074, 0x2079, 1),
            new Range16 (0x2080, 0x2089, 1),
            new Range16 (0x2150, 0x215f, 1),
            new Range16 (0x2189, 0x2460, 727),
            new Range16 (0x2461, 0x249b, 1),
            new Range16 (0x24ea, 0x24ff, 1),
            new Range16 (0x2776, 0x2793, 1),
            new Range16 (0x2cfd, 0x3192, 1173),
            new Range16 (0x3193, 0x3195, 1),
            new Range16 (0x3220, 0x3229, 1),
            new Range16 (0x3248, 0x324f, 1),
            new Range16 (0x3251, 0x325f, 1),
            new Range16 (0x3280, 0x3289, 1),
            new Range16 (0x32b1, 0x32bf, 1),
            new Range16 (0xa830, 0xa835, 1),
                },
                r32: new Range32[] {
            new Range32 (0x10107, 0x10133, 1),
            new Range32 (0x10175, 0x10178, 1),
            new Range32 (0x1018a, 0x1018b, 1),
            new Range32 (0x102e1, 0x102fb, 1),
            new Range32 (0x10320, 0x10323, 1),
            new Range32 (0x10858, 0x1085f, 1),
            new Range32 (0x10879, 0x1087f, 1),
            new Range32 (0x108a7, 0x108af, 1),
            new Range32 (0x108fb, 0x108ff, 1),
            new Range32 (0x10916, 0x1091b, 1),
            new Range32 (0x109bc, 0x109bd, 1),
            new Range32 (0x109c0, 0x109cf, 1),
            new Range32 (0x109d2, 0x109ff, 1),
            new Range32 (0x10a40, 0x10a48, 1),
            new Range32 (0x10a7d, 0x10a7e, 1),
            new Range32 (0x10a9d, 0x10a9f, 1),
            new Range32 (0x10aeb, 0x10aef, 1),
            new Range32 (0x10b58, 0x10b5f, 1),
            new Range32 (0x10b78, 0x10b7f, 1),
            new Range32 (0x10ba9, 0x10baf, 1),
            new Range32 (0x10cfa, 0x10cff, 1),
            new Range32 (0x10e60, 0x10e7e, 1),
            new Range32 (0x10f1d, 0x10f26, 1),
            new Range32 (0x10f51, 0x10f54, 1),
            new Range32 (0x10fc5, 0x10fcb, 1),
            new Range32 (0x11052, 0x11065, 1),
            new Range32 (0x111e1, 0x111f4, 1),
            new Range32 (0x1173a, 0x1173b, 1),
            new Range32 (0x118ea, 0x118f2, 1),
            new Range32 (0x11c5a, 0x11c6c, 1),
            new Range32 (0x11fc0, 0x11fd4, 1),
            new Range32 (0x16b5b, 0x16b61, 1),
            new Range32 (0x16e80, 0x16e96, 1),
            new Range32 (0x1d2c0, 0x1d2d3, 1),
            new Range32 (0x1d2e0, 0x1d2f3, 1),
            new Range32 (0x1d360, 0x1d378, 1),
            new Range32 (0x1e8c7, 0x1e8cf, 1),
            new Range32 (0x1ec71, 0x1ecab, 1),
            new Range32 (0x1ecad, 0x1ecaf, 1),
            new Range32 (0x1ecb1, 0x1ecb4, 1),
            new Range32 (0x1ed01, 0x1ed2d, 1),
            new Range32 (0x1ed2f, 0x1ed3d, 1),
            new Range32 (0x1f100, 0x1f10c, 1),
                },
                latinOffset: 3
            );

            internal static RangeTable _P = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0021, 0x0023, 1),
            new Range16 (0x0025, 0x002a, 1),
            new Range16 (0x002c, 0x002f, 1),
            new Range16 (0x003a, 0x003b, 1),
            new Range16 (0x003f, 0x0040, 1),
            new Range16 (0x005b, 0x005d, 1),
            new Range16 (0x005f, 0x007b, 28),
            new Range16 (0x007d, 0x00a1, 36),
            new Range16 (0x00a7, 0x00ab, 4),
            new Range16 (0x00b6, 0x00b7, 1),
            new Range16 (0x00bb, 0x00bf, 4),
            new Range16 (0x037e, 0x0387, 9),
            new Range16 (0x055a, 0x055f, 1),
            new Range16 (0x0589, 0x058a, 1),
            new Range16 (0x05be, 0x05c0, 2),
            new Range16 (0x05c3, 0x05c6, 3),
            new Range16 (0x05f3, 0x05f4, 1),
            new Range16 (0x0609, 0x060a, 1),
            new Range16 (0x060c, 0x060d, 1),
            new Range16 (0x061b, 0x061d, 2),
            new Range16 (0x061e, 0x061f, 1),
            new Range16 (0x066a, 0x066d, 1),
            new Range16 (0x06d4, 0x0700, 44),
            new Range16 (0x0701, 0x070d, 1),
            new Range16 (0x07f7, 0x07f9, 1),
            new Range16 (0x0830, 0x083e, 1),
            new Range16 (0x085e, 0x0964, 262),
            new Range16 (0x0965, 0x0970, 11),
            new Range16 (0x09fd, 0x0a76, 121),
            new Range16 (0x0af0, 0x0c77, 391),
            new Range16 (0x0c84, 0x0df4, 368),
            new Range16 (0x0e4f, 0x0e5a, 11),
            new Range16 (0x0e5b, 0x0f04, 169),
            new Range16 (0x0f05, 0x0f12, 1),
            new Range16 (0x0f14, 0x0f3a, 38),
            new Range16 (0x0f3b, 0x0f3d, 1),
            new Range16 (0x0f85, 0x0fd0, 75),
            new Range16 (0x0fd1, 0x0fd4, 1),
            new Range16 (0x0fd9, 0x0fda, 1),
            new Range16 (0x104a, 0x104f, 1),
            new Range16 (0x10fb, 0x1360, 613),
            new Range16 (0x1361, 0x1368, 1),
            new Range16 (0x1400, 0x166e, 622),
            new Range16 (0x169b, 0x169c, 1),
            new Range16 (0x16eb, 0x16ed, 1),
            new Range16 (0x1735, 0x1736, 1),
            new Range16 (0x17d4, 0x17d6, 1),
            new Range16 (0x17d8, 0x17da, 1),
            new Range16 (0x1800, 0x180a, 1),
            new Range16 (0x1944, 0x1945, 1),
            new Range16 (0x1a1e, 0x1a1f, 1),
            new Range16 (0x1aa0, 0x1aa6, 1),
            new Range16 (0x1aa8, 0x1aad, 1),
            new Range16 (0x1b5a, 0x1b60, 1),
            new Range16 (0x1b7d, 0x1b7e, 1),
            new Range16 (0x1bfc, 0x1bff, 1),
            new Range16 (0x1c3b, 0x1c3f, 1),
            new Range16 (0x1c7e, 0x1c7f, 1),
            new Range16 (0x1cc0, 0x1cc7, 1),
            new Range16 (0x1cd3, 0x2010, 829),
            new Range16 (0x2011, 0x2027, 1),
            new Range16 (0x2030, 0x2043, 1),
            new Range16 (0x2045, 0x2051, 1),
            new Range16 (0x2053, 0x205e, 1),
            new Range16 (0x207d, 0x207e, 1),
            new Range16 (0x208d, 0x208e, 1),
            new Range16 (0x2308, 0x230b, 1),
            new Range16 (0x2329, 0x232a, 1),
            new Range16 (0x2768, 0x2775, 1),
            new Range16 (0x27c5, 0x27c6, 1),
            new Range16 (0x27e6, 0x27ef, 1),
            new Range16 (0x2983, 0x2998, 1),
            new Range16 (0x29d8, 0x29db, 1),
            new Range16 (0x29fc, 0x29fd, 1),
            new Range16 (0x2cf9, 0x2cfc, 1),
            new Range16 (0x2cfe, 0x2cff, 1),
            new Range16 (0x2d70, 0x2e00, 144),
            new Range16 (0x2e01, 0x2e2e, 1),
            new Range16 (0x2e30, 0x2e4f, 1),
            new Range16 (0x2e52, 0x2e5d, 1),
            new Range16 (0x3001, 0x3003, 1),
            new Range16 (0x3008, 0x3011, 1),
            new Range16 (0x3014, 0x301f, 1),
            new Range16 (0x3030, 0x303d, 13),
            new Range16 (0x30a0, 0x30fb, 91),
            new Range16 (0xa4fe, 0xa4ff, 1),
            new Range16 (0xa60d, 0xa60f, 1),
            new Range16 (0xa673, 0xa67e, 11),
            new Range16 (0xa6f2, 0xa6f7, 1),
            new Range16 (0xa874, 0xa877, 1),
            new Range16 (0xa8ce, 0xa8cf, 1),
            new Range16 (0xa8f8, 0xa8fa, 1),
            new Range16 (0xa8fc, 0xa92e, 50),
            new Range16 (0xa92f, 0xa95f, 48),
            new Range16 (0xa9c1, 0xa9cd, 1),
            new Range16 (0xa9de, 0xa9df, 1),
            new Range16 (0xaa5c, 0xaa5f, 1),
            new Range16 (0xaade, 0xaadf, 1),
            new Range16 (0xaaf0, 0xaaf1, 1),
            new Range16 (0xabeb, 0xfd3e, 20819),
            new Range16 (0xfd3f, 0xfe10, 209),
            new Range16 (0xfe11, 0xfe19, 1),
            new Range16 (0xfe30, 0xfe52, 1),
            new Range16 (0xfe54, 0xfe61, 1),
            new Range16 (0xfe63, 0xfe68, 5),
            new Range16 (0xfe6a, 0xfe6b, 1),
            new Range16 (0xff01, 0xff03, 1),
            new Range16 (0xff05, 0xff0a, 1),
            new Range16 (0xff0c, 0xff0f, 1),
            new Range16 (0xff1a, 0xff1b, 1),
            new Range16 (0xff1f, 0xff20, 1),
            new Range16 (0xff3b, 0xff3d, 1),
            new Range16 (0xff3f, 0xff5b, 28),
            new Range16 (0xff5d, 0xff5f, 2),
            new Range16 (0xff60, 0xff65, 1),
                },
                r32: new Range32[] {
            new Range32 (0x10100, 0x10102, 1),
            new Range32 (0x1039f, 0x103d0, 49),
            new Range32 (0x1056f, 0x10857, 744),
            new Range32 (0x1091f, 0x1093f, 32),
            new Range32 (0x10a50, 0x10a58, 1),
            new Range32 (0x10a7f, 0x10af0, 113),
            new Range32 (0x10af1, 0x10af6, 1),
            new Range32 (0x10b39, 0x10b3f, 1),
            new Range32 (0x10b99, 0x10b9c, 1),
            new Range32 (0x10ead, 0x10f55, 168),
            new Range32 (0x10f56, 0x10f59, 1),
            new Range32 (0x10f86, 0x10f89, 1),
            new Range32 (0x11047, 0x1104d, 1),
            new Range32 (0x110bb, 0x110bc, 1),
            new Range32 (0x110be, 0x110c1, 1),
            new Range32 (0x11140, 0x11143, 1),
            new Range32 (0x11174, 0x11175, 1),
            new Range32 (0x111c5, 0x111c8, 1),
            new Range32 (0x111cd, 0x111db, 14),
            new Range32 (0x111dd, 0x111df, 1),
            new Range32 (0x11238, 0x1123d, 1),
            new Range32 (0x112a9, 0x1144b, 418),
            new Range32 (0x1144c, 0x1144f, 1),
            new Range32 (0x1145a, 0x1145b, 1),
            new Range32 (0x1145d, 0x114c6, 105),
            new Range32 (0x115c1, 0x115d7, 1),
            new Range32 (0x11641, 0x11643, 1),
            new Range32 (0x11660, 0x1166c, 1),
            new Range32 (0x116b9, 0x1173c, 131),
            new Range32 (0x1173d, 0x1173e, 1),
            new Range32 (0x1183b, 0x11944, 265),
            new Range32 (0x11945, 0x11946, 1),
            new Range32 (0x119e2, 0x11a3f, 93),
            new Range32 (0x11a40, 0x11a46, 1),
            new Range32 (0x11a9a, 0x11a9c, 1),
            new Range32 (0x11a9e, 0x11aa2, 1),
            new Range32 (0x11b00, 0x11b09, 1),
            new Range32 (0x11c41, 0x11c45, 1),
            new Range32 (0x11c70, 0x11c71, 1),
            new Range32 (0x11ef7, 0x11ef8, 1),
            new Range32 (0x11f43, 0x11f4f, 1),
            new Range32 (0x11fff, 0x12470, 1137),
            new Range32 (0x12471, 0x12474, 1),
            new Range32 (0x12ff1, 0x12ff2, 1),
            new Range32 (0x16a6e, 0x16a6f, 1),
            new Range32 (0x16af5, 0x16b37, 66),
            new Range32 (0x16b38, 0x16b3b, 1),
            new Range32 (0x16b44, 0x16e97, 851),
            new Range32 (0x16e98, 0x16e9a, 1),
            new Range32 (0x16fe2, 0x1bc9f, 19645),
            new Range32 (0x1da87, 0x1da8b, 1),
            new Range32 (0x1e95e, 0x1e95f, 1),
                },
                latinOffset: 11
            );

            internal static RangeTable _Pc = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x005f, 0x203f, 8160),
            new Range16 (0x2040, 0x2054, 20),
            new Range16 (0xfe33, 0xfe34, 1),
            new Range16 (0xfe4d, 0xfe4f, 1),
            new Range16 (0xff3f, 0xff3f, 1),
                }
            );

            internal static RangeTable _Pd = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x002d, 0x058a, 1373),
            new Range16 (0x05be, 0x1400, 3650),
            new Range16 (0x1806, 0x2010, 2058),
            new Range16 (0x2011, 0x2015, 1),
            new Range16 (0x2e17, 0x2e1a, 3),
            new Range16 (0x2e3a, 0x2e3b, 1),
            new Range16 (0x2e40, 0x2e5d, 29),
            new Range16 (0x301c, 0x3030, 20),
            new Range16 (0x30a0, 0xfe31, 52625),
            new Range16 (0xfe32, 0xfe58, 38),
            new Range16 (0xfe63, 0xff0d, 170),
                },
                r32: new Range32[] {
            new Range32 (0x10ead, 0x10ead, 1),
                }
            );

            internal static RangeTable _Pe = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0029, 0x005d, 52),
            new Range16 (0x007d, 0x0f3b, 3774),
            new Range16 (0x0f3d, 0x169c, 1887),
            new Range16 (0x2046, 0x207e, 56),
            new Range16 (0x208e, 0x2309, 635),
            new Range16 (0x230b, 0x232a, 31),
            new Range16 (0x2769, 0x2775, 2),
            new Range16 (0x27c6, 0x27e7, 33),
            new Range16 (0x27e9, 0x27ef, 2),
            new Range16 (0x2984, 0x2998, 2),
            new Range16 (0x29d9, 0x29db, 2),
            new Range16 (0x29fd, 0x2e23, 1062),
            new Range16 (0x2e25, 0x2e29, 2),
            new Range16 (0x2e56, 0x2e5c, 2),
            new Range16 (0x3009, 0x3011, 2),
            new Range16 (0x3015, 0x301b, 2),
            new Range16 (0x301e, 0x301f, 1),
            new Range16 (0xfd3e, 0xfe18, 218),
            new Range16 (0xfe36, 0xfe44, 2),
            new Range16 (0xfe48, 0xfe5a, 18),
            new Range16 (0xfe5c, 0xfe5e, 2),
            new Range16 (0xff09, 0xff3d, 52),
            new Range16 (0xff5d, 0xff63, 3),
                },
                latinOffset: 1
            );

            internal static RangeTable _Pf = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x00bb, 0x2019, 8030),
            new Range16 (0x201d, 0x203a, 29),
            new Range16 (0x2e03, 0x2e05, 2),
            new Range16 (0x2e0a, 0x2e0d, 3),
            new Range16 (0x2e1d, 0x2e21, 4),
                }
            );

            internal static RangeTable _Pi = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x00ab, 0x2018, 8045),
            new Range16 (0x201b, 0x201c, 1),
            new Range16 (0x201f, 0x2039, 26),
            new Range16 (0x2e02, 0x2e04, 2),
            new Range16 (0x2e09, 0x2e0c, 3),
            new Range16 (0x2e1c, 0x2e20, 4),
                }
            );

            internal static RangeTable _Po = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0021, 0x0023, 1),
            new Range16 (0x0025, 0x0027, 1),
            new Range16 (0x002a, 0x002e, 2),
            new Range16 (0x002f, 0x003a, 11),
            new Range16 (0x003b, 0x003f, 4),
            new Range16 (0x0040, 0x005c, 28),
            new Range16 (0x00a1, 0x00a7, 6),
            new Range16 (0x00b6, 0x00b7, 1),
            new Range16 (0x00bf, 0x037e, 703),
            new Range16 (0x0387, 0x055a, 467),
            new Range16 (0x055b, 0x055f, 1),
            new Range16 (0x0589, 0x05c0, 55),
            new Range16 (0x05c3, 0x05c6, 3),
            new Range16 (0x05f3, 0x05f4, 1),
            new Range16 (0x0609, 0x060a, 1),
            new Range16 (0x060c, 0x060d, 1),
            new Range16 (0x061b, 0x061d, 2),
            new Range16 (0x061e, 0x061f, 1),
            new Range16 (0x066a, 0x066d, 1),
            new Range16 (0x06d4, 0x0700, 44),
            new Range16 (0x0701, 0x070d, 1),
            new Range16 (0x07f7, 0x07f9, 1),
            new Range16 (0x0830, 0x083e, 1),
            new Range16 (0x085e, 0x0964, 262),
            new Range16 (0x0965, 0x0970, 11),
            new Range16 (0x09fd, 0x0a76, 121),
            new Range16 (0x0af0, 0x0c77, 391),
            new Range16 (0x0c84, 0x0df4, 368),
            new Range16 (0x0e4f, 0x0e5a, 11),
            new Range16 (0x0e5b, 0x0f04, 169),
            new Range16 (0x0f05, 0x0f12, 1),
            new Range16 (0x0f14, 0x0f85, 113),
            new Range16 (0x0fd0, 0x0fd4, 1),
            new Range16 (0x0fd9, 0x0fda, 1),
            new Range16 (0x104a, 0x104f, 1),
            new Range16 (0x10fb, 0x1360, 613),
            new Range16 (0x1361, 0x1368, 1),
            new Range16 (0x166e, 0x16eb, 125),
            new Range16 (0x16ec, 0x16ed, 1),
            new Range16 (0x1735, 0x1736, 1),
            new Range16 (0x17d4, 0x17d6, 1),
            new Range16 (0x17d8, 0x17da, 1),
            new Range16 (0x1800, 0x1805, 1),
            new Range16 (0x1807, 0x180a, 1),
            new Range16 (0x1944, 0x1945, 1),
            new Range16 (0x1a1e, 0x1a1f, 1),
            new Range16 (0x1aa0, 0x1aa6, 1),
            new Range16 (0x1aa8, 0x1aad, 1),
            new Range16 (0x1b5a, 0x1b60, 1),
            new Range16 (0x1b7d, 0x1b7e, 1),
            new Range16 (0x1bfc, 0x1bff, 1),
            new Range16 (0x1c3b, 0x1c3f, 1),
            new Range16 (0x1c7e, 0x1c7f, 1),
            new Range16 (0x1cc0, 0x1cc7, 1),
            new Range16 (0x1cd3, 0x2016, 835),
            new Range16 (0x2017, 0x2020, 9),
            new Range16 (0x2021, 0x2027, 1),
            new Range16 (0x2030, 0x2038, 1),
            new Range16 (0x203b, 0x203e, 1),
            new Range16 (0x2041, 0x2043, 1),
            new Range16 (0x2047, 0x2051, 1),
            new Range16 (0x2053, 0x2055, 2),
            new Range16 (0x2056, 0x205e, 1),
            new Range16 (0x2cf9, 0x2cfc, 1),
            new Range16 (0x2cfe, 0x2cff, 1),
            new Range16 (0x2d70, 0x2e00, 144),
            new Range16 (0x2e01, 0x2e06, 5),
            new Range16 (0x2e07, 0x2e08, 1),
            new Range16 (0x2e0b, 0x2e0e, 3),
            new Range16 (0x2e0f, 0x2e16, 1),
            new Range16 (0x2e18, 0x2e19, 1),
            new Range16 (0x2e1b, 0x2e1e, 3),
            new Range16 (0x2e1f, 0x2e2a, 11),
            new Range16 (0x2e2b, 0x2e2e, 1),
            new Range16 (0x2e30, 0x2e39, 1),
            new Range16 (0x2e3c, 0x2e3f, 1),
            new Range16 (0x2e41, 0x2e43, 2),
            new Range16 (0x2e44, 0x2e4f, 1),
            new Range16 (0x2e52, 0x2e54, 1),
            new Range16 (0x3001, 0x3003, 1),
            new Range16 (0x303d, 0x30fb, 190),
            new Range16 (0xa4fe, 0xa4ff, 1),
            new Range16 (0xa60d, 0xa60f, 1),
            new Range16 (0xa673, 0xa67e, 11),
            new Range16 (0xa6f2, 0xa6f7, 1),
            new Range16 (0xa874, 0xa877, 1),
            new Range16 (0xa8ce, 0xa8cf, 1),
            new Range16 (0xa8f8, 0xa8fa, 1),
            new Range16 (0xa8fc, 0xa92e, 50),
            new Range16 (0xa92f, 0xa95f, 48),
            new Range16 (0xa9c1, 0xa9cd, 1),
            new Range16 (0xa9de, 0xa9df, 1),
            new Range16 (0xaa5c, 0xaa5f, 1),
            new Range16 (0xaade, 0xaadf, 1),
            new Range16 (0xaaf0, 0xaaf1, 1),
            new Range16 (0xabeb, 0xfe10, 21029),
            new Range16 (0xfe11, 0xfe16, 1),
            new Range16 (0xfe19, 0xfe30, 23),
            new Range16 (0xfe45, 0xfe46, 1),
            new Range16 (0xfe49, 0xfe4c, 1),
            new Range16 (0xfe50, 0xfe52, 1),
            new Range16 (0xfe54, 0xfe57, 1),
            new Range16 (0xfe5f, 0xfe61, 1),
            new Range16 (0xfe68, 0xfe6a, 2),
            new Range16 (0xfe6b, 0xff01, 150),
            new Range16 (0xff02, 0xff03, 1),
            new Range16 (0xff05, 0xff07, 1),
            new Range16 (0xff0a, 0xff0e, 2),
            new Range16 (0xff0f, 0xff1a, 11),
            new Range16 (0xff1b, 0xff1f, 4),
            new Range16 (0xff20, 0xff3c, 28),
            new Range16 (0xff61, 0xff64, 3),
            new Range16 (0xff65, 0xff65, 1),
                },
                r32: new Range32[] {
            new Range32 (0x10100, 0x10100, 1),
            new Range32 (0x10101, 0x10102, 1),
            new Range32 (0x1039f, 0x103d0, 49),
            new Range32 (0x1056f, 0x10857, 744),
            new Range32 (0x1091f, 0x1093f, 32),
            new Range32 (0x10a50, 0x10a58, 1),
            new Range32 (0x10a7f, 0x10af0, 113),
            new Range32 (0x10af1, 0x10af6, 1),
            new Range32 (0x10b39, 0x10b3f, 1),
            new Range32 (0x10b99, 0x10b9c, 1),
            new Range32 (0x10f55, 0x10f59, 1),
            new Range32 (0x10f86, 0x10f89, 1),
            new Range32 (0x11047, 0x1104d, 1),
            new Range32 (0x110bb, 0x110bc, 1),
            new Range32 (0x110be, 0x110c1, 1),
            new Range32 (0x11140, 0x11143, 1),
            new Range32 (0x11174, 0x11175, 1),
            new Range32 (0x111c5, 0x111c8, 1),
            new Range32 (0x111cd, 0x111db, 14),
            new Range32 (0x111dd, 0x111df, 1),
            new Range32 (0x11238, 0x1123d, 1),
            new Range32 (0x112a9, 0x1144b, 418),
            new Range32 (0x1144c, 0x1144f, 1),
            new Range32 (0x1145a, 0x1145b, 1),
            new Range32 (0x1145d, 0x114c6, 105),
            new Range32 (0x115c1, 0x115d7, 1),
            new Range32 (0x11641, 0x11643, 1),
            new Range32 (0x11660, 0x1166c, 1),
            new Range32 (0x116b9, 0x1173c, 131),
            new Range32 (0x1173d, 0x1173e, 1),
            new Range32 (0x1183b, 0x11944, 265),
            new Range32 (0x11945, 0x11946, 1),
            new Range32 (0x119e2, 0x11a3f, 93),
            new Range32 (0x11a40, 0x11a46, 1),
            new Range32 (0x11a9a, 0x11a9c, 1),
            new Range32 (0x11a9e, 0x11aa2, 1),
            new Range32 (0x11b00, 0x11b09, 1),
            new Range32 (0x11c41, 0x11c45, 1),
            new Range32 (0x11c70, 0x11c71, 1),
            new Range32 (0x11ef7, 0x11ef8, 1),
            new Range32 (0x11f43, 0x11f4f, 1),
            new Range32 (0x11fff, 0x12470, 1137),
            new Range32 (0x12471, 0x12474, 1),
            new Range32 (0x12ff1, 0x12ff2, 1),
            new Range32 (0x16a6e, 0x16a6f, 1),
            new Range32 (0x16af5, 0x16b37, 66),
            new Range32 (0x16b38, 0x16b3b, 1),
            new Range32 (0x16b44, 0x16e97, 851),
            new Range32 (0x16e98, 0x16e9a, 1),
            new Range32 (0x16fe2, 0x1bc9f, 19645),
            new Range32 (0x1da87, 0x1da8b, 1),
            new Range32 (0x1e95e, 0x1e95f, 1),
                },
                latinOffset: 8
            );

            internal static RangeTable _Ps = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0028, 0x005b, 51),
            new Range16 (0x007b, 0x0f3a, 3775),
            new Range16 (0x0f3c, 0x169b, 1887),
            new Range16 (0x201a, 0x201e, 4),
            new Range16 (0x2045, 0x207d, 56),
            new Range16 (0x208d, 0x2308, 635),
            new Range16 (0x230a, 0x2329, 31),
            new Range16 (0x2768, 0x2774, 2),
            new Range16 (0x27c5, 0x27e6, 33),
            new Range16 (0x27e8, 0x27ee, 2),
            new Range16 (0x2983, 0x2997, 2),
            new Range16 (0x29d8, 0x29da, 2),
            new Range16 (0x29fc, 0x2e22, 1062),
            new Range16 (0x2e24, 0x2e28, 2),
            new Range16 (0x2e42, 0x2e55, 19),
            new Range16 (0x2e57, 0x2e5b, 2),
            new Range16 (0x3008, 0x3010, 2),
            new Range16 (0x3014, 0x301a, 2),
            new Range16 (0x301d, 0xfd3f, 52514),
            new Range16 (0xfe17, 0xfe35, 30),
            new Range16 (0xfe37, 0xfe43, 2),
            new Range16 (0xfe47, 0xfe59, 18),
            new Range16 (0xfe5b, 0xfe5d, 2),
            new Range16 (0xff08, 0xff3b, 51),
            new Range16 (0xff5b, 0xff5f, 4),
            new Range16 (0xff62, 0xff62, 1),
                },
                latinOffset: 1
            );

            internal static RangeTable _S = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0024, 0x002b, 7),
            new Range16 (0x003c, 0x003e, 1),
            new Range16 (0x005e, 0x0060, 2),
            new Range16 (0x007c, 0x007e, 2),
            new Range16 (0x00a2, 0x00a6, 1),
            new Range16 (0x00a8, 0x00a9, 1),
            new Range16 (0x00ac, 0x00ae, 2),
            new Range16 (0x00af, 0x00b1, 1),
            new Range16 (0x00b4, 0x00b8, 4),
            new Range16 (0x00d7, 0x00f7, 32),
            new Range16 (0x02c2, 0x02c5, 1),
            new Range16 (0x02d2, 0x02df, 1),
            new Range16 (0x02e5, 0x02eb, 1),
            new Range16 (0x02ed, 0x02ef, 2),
            new Range16 (0x02f0, 0x02ff, 1),
            new Range16 (0x0375, 0x0384, 15),
            new Range16 (0x0385, 0x03f6, 113),
            new Range16 (0x0482, 0x058d, 267),
            new Range16 (0x058e, 0x058f, 1),
            new Range16 (0x0606, 0x0608, 1),
            new Range16 (0x060b, 0x060e, 3),
            new Range16 (0x060f, 0x06de, 207),
            new Range16 (0x06e9, 0x06fd, 20),
            new Range16 (0x06fe, 0x07f6, 248),
            new Range16 (0x07fe, 0x07ff, 1),
            new Range16 (0x0888, 0x09f2, 362),
            new Range16 (0x09f3, 0x09fa, 7),
            new Range16 (0x09fb, 0x0af1, 246),
            new Range16 (0x0b70, 0x0bf3, 131),
            new Range16 (0x0bf4, 0x0bfa, 1),
            new Range16 (0x0c7f, 0x0d4f, 208),
            new Range16 (0x0d79, 0x0e3f, 198),
            new Range16 (0x0f01, 0x0f03, 1),
            new Range16 (0x0f13, 0x0f15, 2),
            new Range16 (0x0f16, 0x0f17, 1),
            new Range16 (0x0f1a, 0x0f1f, 1),
            new Range16 (0x0f34, 0x0f38, 2),
            new Range16 (0x0fbe, 0x0fc5, 1),
            new Range16 (0x0fc7, 0x0fcc, 1),
            new Range16 (0x0fce, 0x0fcf, 1),
            new Range16 (0x0fd5, 0x0fd8, 1),
            new Range16 (0x109e, 0x109f, 1),
            new Range16 (0x1390, 0x1399, 1),
            new Range16 (0x166d, 0x17db, 366),
            new Range16 (0x1940, 0x19de, 158),
            new Range16 (0x19df, 0x19ff, 1),
            new Range16 (0x1b61, 0x1b6a, 1),
            new Range16 (0x1b74, 0x1b7c, 1),
            new Range16 (0x1fbd, 0x1fbf, 2),
            new Range16 (0x1fc0, 0x1fc1, 1),
            new Range16 (0x1fcd, 0x1fcf, 1),
            new Range16 (0x1fdd, 0x1fdf, 1),
            new Range16 (0x1fed, 0x1fef, 1),
            new Range16 (0x1ffd, 0x1ffe, 1),
            new Range16 (0x2044, 0x2052, 14),
            new Range16 (0x207a, 0x207c, 1),
            new Range16 (0x208a, 0x208c, 1),
            new Range16 (0x20a0, 0x20c0, 1),
            new Range16 (0x2100, 0x2101, 1),
            new Range16 (0x2103, 0x2106, 1),
            new Range16 (0x2108, 0x2109, 1),
            new Range16 (0x2114, 0x2116, 2),
            new Range16 (0x2117, 0x2118, 1),
            new Range16 (0x211e, 0x2123, 1),
            new Range16 (0x2125, 0x2129, 2),
            new Range16 (0x212e, 0x213a, 12),
            new Range16 (0x213b, 0x2140, 5),
            new Range16 (0x2141, 0x2144, 1),
            new Range16 (0x214a, 0x214d, 1),
            new Range16 (0x214f, 0x218a, 59),
            new Range16 (0x218b, 0x2190, 5),
            new Range16 (0x2191, 0x2307, 1),
            new Range16 (0x230c, 0x2328, 1),
            new Range16 (0x232b, 0x2426, 1),
            new Range16 (0x2440, 0x244a, 1),
            new Range16 (0x249c, 0x24e9, 1),
            new Range16 (0x2500, 0x2767, 1),
            new Range16 (0x2794, 0x27c4, 1),
            new Range16 (0x27c7, 0x27e5, 1),
            new Range16 (0x27f0, 0x2982, 1),
            new Range16 (0x2999, 0x29d7, 1),
            new Range16 (0x29dc, 0x29fb, 1),
            new Range16 (0x29fe, 0x2b73, 1),
            new Range16 (0x2b76, 0x2b95, 1),
            new Range16 (0x2b97, 0x2bff, 1),
            new Range16 (0x2ce5, 0x2cea, 1),
            new Range16 (0x2e50, 0x2e51, 1),
            new Range16 (0x2e80, 0x2e99, 1),
            new Range16 (0x2e9b, 0x2ef3, 1),
            new Range16 (0x2f00, 0x2fd5, 1),
            new Range16 (0x2ff0, 0x2ffb, 1),
            new Range16 (0x3004, 0x3012, 14),
            new Range16 (0x3013, 0x3020, 13),
            new Range16 (0x3036, 0x3037, 1),
            new Range16 (0x303e, 0x303f, 1),
            new Range16 (0x309b, 0x309c, 1),
            new Range16 (0x3190, 0x3191, 1),
            new Range16 (0x3196, 0x319f, 1),
            new Range16 (0x31c0, 0x31e3, 1),
            new Range16 (0x3200, 0x321e, 1),
            new Range16 (0x322a, 0x3247, 1),
            new Range16 (0x3250, 0x3260, 16),
            new Range16 (0x3261, 0x327f, 1),
            new Range16 (0x328a, 0x32b0, 1),
            new Range16 (0x32c0, 0x33ff, 1),
            new Range16 (0x4dc0, 0x4dff, 1),
            new Range16 (0xa490, 0xa4c6, 1),
            new Range16 (0xa700, 0xa716, 1),
            new Range16 (0xa720, 0xa721, 1),
            new Range16 (0xa789, 0xa78a, 1),
            new Range16 (0xa828, 0xa82b, 1),
            new Range16 (0xa836, 0xa839, 1),
            new Range16 (0xaa77, 0xaa79, 1),
            new Range16 (0xab5b, 0xab6a, 15),
            new Range16 (0xab6b, 0xfb29, 20414),
            new Range16 (0xfbb2, 0xfbc2, 1),
            new Range16 (0xfd40, 0xfd4f, 1),
            new Range16 (0xfdcf, 0xfdfc, 45),
            new Range16 (0xfdfd, 0xfdff, 1),
            new Range16 (0xfe62, 0xfe64, 2),
            new Range16 (0xfe65, 0xfe66, 1),
            new Range16 (0xfe69, 0xff04, 155),
            new Range16 (0xff0b, 0xff1c, 17),
            new Range16 (0xff1d, 0xff1e, 1),
            new Range16 (0xff3e, 0xff40, 2),
            new Range16 (0xff5c, 0xff5e, 2),
            new Range16 (0xffe0, 0xffe6, 1),
            new Range16 (0xffe8, 0xffee, 1),
            new Range16 (0xfffc, 0xfffd, 1),
                },
                r32: new Range32[] {
            new Range32 (0x10137, 0x1013f, 1),
            new Range32 (0x10179, 0x10189, 1),
            new Range32 (0x1018c, 0x1018e, 1),
            new Range32 (0x10190, 0x1019c, 1),
            new Range32 (0x101a0, 0x101d0, 48),
            new Range32 (0x101d1, 0x101fc, 1),
            new Range32 (0x10877, 0x10878, 1),
            new Range32 (0x10ac8, 0x1173f, 3191),
            new Range32 (0x11fd5, 0x11ff1, 1),
            new Range32 (0x16b3c, 0x16b3f, 1),
            new Range32 (0x16b45, 0x1bc9c, 20823),
            new Range32 (0x1cf50, 0x1cfc3, 1),
            new Range32 (0x1d000, 0x1d0f5, 1),
            new Range32 (0x1d100, 0x1d126, 1),
            new Range32 (0x1d129, 0x1d164, 1),
            new Range32 (0x1d16a, 0x1d16c, 1),
            new Range32 (0x1d183, 0x1d184, 1),
            new Range32 (0x1d18c, 0x1d1a9, 1),
            new Range32 (0x1d1ae, 0x1d1ea, 1),
            new Range32 (0x1d200, 0x1d241, 1),
            new Range32 (0x1d245, 0x1d300, 187),
            new Range32 (0x1d301, 0x1d356, 1),
            new Range32 (0x1d6c1, 0x1d6db, 26),
            new Range32 (0x1d6fb, 0x1d715, 26),
            new Range32 (0x1d735, 0x1d74f, 26),
            new Range32 (0x1d76f, 0x1d789, 26),
            new Range32 (0x1d7a9, 0x1d7c3, 26),
            new Range32 (0x1d800, 0x1d9ff, 1),
            new Range32 (0x1da37, 0x1da3a, 1),
            new Range32 (0x1da6d, 0x1da74, 1),
            new Range32 (0x1da76, 0x1da83, 1),
            new Range32 (0x1da85, 0x1da86, 1),
            new Range32 (0x1e14f, 0x1e2ff, 432),
            new Range32 (0x1ecac, 0x1ecb0, 4),
            new Range32 (0x1ed2e, 0x1eef0, 450),
            new Range32 (0x1eef1, 0x1f000, 271),
            new Range32 (0x1f001, 0x1f02b, 1),
            new Range32 (0x1f030, 0x1f093, 1),
            new Range32 (0x1f0a0, 0x1f0ae, 1),
            new Range32 (0x1f0b1, 0x1f0bf, 1),
            new Range32 (0x1f0c1, 0x1f0cf, 1),
            new Range32 (0x1f0d1, 0x1f0f5, 1),
            new Range32 (0x1f10d, 0x1f1ad, 1),
            new Range32 (0x1f1e6, 0x1f202, 1),
            new Range32 (0x1f210, 0x1f23b, 1),
            new Range32 (0x1f240, 0x1f248, 1),
            new Range32 (0x1f250, 0x1f251, 1),
            new Range32 (0x1f260, 0x1f265, 1),
            new Range32 (0x1f300, 0x1f6d7, 1),
            new Range32 (0x1f6dc, 0x1f6ec, 1),
            new Range32 (0x1f6f0, 0x1f6fc, 1),
            new Range32 (0x1f700, 0x1f776, 1),
            new Range32 (0x1f77b, 0x1f7d9, 1),
            new Range32 (0x1f7e0, 0x1f7eb, 1),
            new Range32 (0x1f7f0, 0x1f800, 16),
            new Range32 (0x1f801, 0x1f80b, 1),
            new Range32 (0x1f810, 0x1f847, 1),
            new Range32 (0x1f850, 0x1f859, 1),
            new Range32 (0x1f860, 0x1f887, 1),
            new Range32 (0x1f890, 0x1f8ad, 1),
            new Range32 (0x1f8b0, 0x1f8b1, 1),
            new Range32 (0x1f900, 0x1fa53, 1),
            new Range32 (0x1fa60, 0x1fa6d, 1),
            new Range32 (0x1fa70, 0x1fa7c, 1),
            new Range32 (0x1fa80, 0x1fa88, 1),
            new Range32 (0x1fa90, 0x1fabd, 1),
            new Range32 (0x1fabf, 0x1fac5, 1),
            new Range32 (0x1face, 0x1fadb, 1),
            new Range32 (0x1fae0, 0x1fae8, 1),
            new Range32 (0x1faf0, 0x1faf8, 1),
            new Range32 (0x1fb00, 0x1fb92, 1),
            new Range32 (0x1fb94, 0x1fbca, 1),
                },
                latinOffset: 10
            );

            internal static RangeTable _Sc = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0024, 0x00a2, 126),
            new Range16 (0x00a3, 0x00a5, 1),
            new Range16 (0x058f, 0x060b, 124),
            new Range16 (0x07fe, 0x07ff, 1),
            new Range16 (0x09f2, 0x09f3, 1),
            new Range16 (0x09fb, 0x0af1, 246),
            new Range16 (0x0bf9, 0x0e3f, 582),
            new Range16 (0x17db, 0x20a0, 2245),
            new Range16 (0x20a1, 0x20c0, 1),
            new Range16 (0xa838, 0xfdfc, 21956),
            new Range16 (0xfe69, 0xff04, 155),
            new Range16 (0xffe0, 0xffe1, 1),
            new Range16 (0xffe5, 0xffe6, 1),
                },
                r32: new Range32[] {
            new Range32 (0x11fdd, 0x11fe0, 1),
            new Range32 (0x1e2ff, 0x1ecb0, 2481),
                },
                latinOffset: 2
            );

            internal static RangeTable _Sk = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x005e, 0x0060, 2),
            new Range16 (0x00a8, 0x00af, 7),
            new Range16 (0x00b4, 0x00b8, 4),
            new Range16 (0x02c2, 0x02c5, 1),
            new Range16 (0x02d2, 0x02df, 1),
            new Range16 (0x02e5, 0x02eb, 1),
            new Range16 (0x02ed, 0x02ef, 2),
            new Range16 (0x02f0, 0x02ff, 1),
            new Range16 (0x0375, 0x0384, 15),
            new Range16 (0x0385, 0x0888, 1283),
            new Range16 (0x1fbd, 0x1fbf, 2),
            new Range16 (0x1fc0, 0x1fc1, 1),
            new Range16 (0x1fcd, 0x1fcf, 1),
            new Range16 (0x1fdd, 0x1fdf, 1),
            new Range16 (0x1fed, 0x1fef, 1),
            new Range16 (0x1ffd, 0x1ffe, 1),
            new Range16 (0x309b, 0x309c, 1),
            new Range16 (0xa700, 0xa716, 1),
            new Range16 (0xa720, 0xa721, 1),
            new Range16 (0xa789, 0xa78a, 1),
            new Range16 (0xab5b, 0xab6a, 15),
            new Range16 (0xab6b, 0xfbb2, 20551),
            new Range16 (0xfbb3, 0xfbc2, 1),
            new Range16 (0xff3e, 0xff40, 2),
            new Range16 (0xffe3, 0xffe3, 1),
                },
                r32: new Range32[] {
            new Range32 (0x1f3fb, 0x1f3fb, 1),
            new Range32 (0x1f3fc, 0x1f3ff, 1),
                },
                latinOffset: 3
            );

            internal static RangeTable _Sm = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x002b, 0x003c, 17),
            new Range16 (0x003d, 0x003e, 1),
            new Range16 (0x007c, 0x007e, 2),
            new Range16 (0x00ac, 0x00b1, 5),
            new Range16 (0x00d7, 0x00f7, 32),
            new Range16 (0x03f6, 0x0606, 528),
            new Range16 (0x0607, 0x0608, 1),
            new Range16 (0x2044, 0x2052, 14),
            new Range16 (0x207a, 0x207c, 1),
            new Range16 (0x208a, 0x208c, 1),
            new Range16 (0x2118, 0x2140, 40),
            new Range16 (0x2141, 0x2144, 1),
            new Range16 (0x214b, 0x2190, 69),
            new Range16 (0x2191, 0x2194, 1),
            new Range16 (0x219a, 0x219b, 1),
            new Range16 (0x21a0, 0x21a6, 3),
            new Range16 (0x21ae, 0x21ce, 32),
            new Range16 (0x21cf, 0x21d2, 3),
            new Range16 (0x21d4, 0x21f4, 32),
            new Range16 (0x21f5, 0x22ff, 1),
            new Range16 (0x2320, 0x2321, 1),
            new Range16 (0x237c, 0x239b, 31),
            new Range16 (0x239c, 0x23b3, 1),
            new Range16 (0x23dc, 0x23e1, 1),
            new Range16 (0x25b7, 0x25c1, 10),
            new Range16 (0x25f8, 0x25ff, 1),
            new Range16 (0x266f, 0x27c0, 337),
            new Range16 (0x27c1, 0x27c4, 1),
            new Range16 (0x27c7, 0x27e5, 1),
            new Range16 (0x27f0, 0x27ff, 1),
            new Range16 (0x2900, 0x2982, 1),
            new Range16 (0x2999, 0x29d7, 1),
            new Range16 (0x29dc, 0x29fb, 1),
            new Range16 (0x29fe, 0x2aff, 1),
            new Range16 (0x2b30, 0x2b44, 1),
            new Range16 (0x2b47, 0x2b4c, 1),
            new Range16 (0xfb29, 0xfe62, 825),
            new Range16 (0xfe64, 0xfe66, 1),
            new Range16 (0xff0b, 0xff1c, 17),
            new Range16 (0xff1d, 0xff1e, 1),
            new Range16 (0xff5c, 0xff5e, 2),
            new Range16 (0xffe2, 0xffe9, 7),
            new Range16 (0xffea, 0xffec, 1),
                },
                r32: new Range32[] {
            new Range32 (0x1d6c1, 0x1d6db, 26),
            new Range32 (0x1d6fb, 0x1d715, 26),
            new Range32 (0x1d735, 0x1d74f, 26),
            new Range32 (0x1d76f, 0x1d789, 26),
            new Range32 (0x1d7a9, 0x1d7c3, 26),
            new Range32 (0x1eef0, 0x1eef1, 1),
                },
                latinOffset: 5
            );

            internal static RangeTable _So = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x00a6, 0x00a9, 3),
            new Range16 (0x00ae, 0x00b0, 2),
            new Range16 (0x0482, 0x058d, 267),
            new Range16 (0x058e, 0x060e, 128),
            new Range16 (0x060f, 0x06de, 207),
            new Range16 (0x06e9, 0x06fd, 20),
            new Range16 (0x06fe, 0x07f6, 248),
            new Range16 (0x09fa, 0x0b70, 374),
            new Range16 (0x0bf3, 0x0bf8, 1),
            new Range16 (0x0bfa, 0x0c7f, 133),
            new Range16 (0x0d4f, 0x0d79, 42),
            new Range16 (0x0f01, 0x0f03, 1),
            new Range16 (0x0f13, 0x0f15, 2),
            new Range16 (0x0f16, 0x0f17, 1),
            new Range16 (0x0f1a, 0x0f1f, 1),
            new Range16 (0x0f34, 0x0f38, 2),
            new Range16 (0x0fbe, 0x0fc5, 1),
            new Range16 (0x0fc7, 0x0fcc, 1),
            new Range16 (0x0fce, 0x0fcf, 1),
            new Range16 (0x0fd5, 0x0fd8, 1),
            new Range16 (0x109e, 0x109f, 1),
            new Range16 (0x1390, 0x1399, 1),
            new Range16 (0x166d, 0x1940, 723),
            new Range16 (0x19de, 0x19ff, 1),
            new Range16 (0x1b61, 0x1b6a, 1),
            new Range16 (0x1b74, 0x1b7c, 1),
            new Range16 (0x2100, 0x2101, 1),
            new Range16 (0x2103, 0x2106, 1),
            new Range16 (0x2108, 0x2109, 1),
            new Range16 (0x2114, 0x2116, 2),
            new Range16 (0x2117, 0x211e, 7),
            new Range16 (0x211f, 0x2123, 1),
            new Range16 (0x2125, 0x2129, 2),
            new Range16 (0x212e, 0x213a, 12),
            new Range16 (0x213b, 0x214a, 15),
            new Range16 (0x214c, 0x214d, 1),
            new Range16 (0x214f, 0x218a, 59),
            new Range16 (0x218b, 0x2195, 10),
            new Range16 (0x2196, 0x2199, 1),
            new Range16 (0x219c, 0x219f, 1),
            new Range16 (0x21a1, 0x21a2, 1),
            new Range16 (0x21a4, 0x21a5, 1),
            new Range16 (0x21a7, 0x21ad, 1),
            new Range16 (0x21af, 0x21cd, 1),
            new Range16 (0x21d0, 0x21d1, 1),
            new Range16 (0x21d3, 0x21d5, 2),
            new Range16 (0x21d6, 0x21f3, 1),
            new Range16 (0x2300, 0x2307, 1),
            new Range16 (0x230c, 0x231f, 1),
            new Range16 (0x2322, 0x2328, 1),
            new Range16 (0x232b, 0x237b, 1),
            new Range16 (0x237d, 0x239a, 1),
            new Range16 (0x23b4, 0x23db, 1),
            new Range16 (0x23e2, 0x2426, 1),
            new Range16 (0x2440, 0x244a, 1),
            new Range16 (0x249c, 0x24e9, 1),
            new Range16 (0x2500, 0x25b6, 1),
            new Range16 (0x25b8, 0x25c0, 1),
            new Range16 (0x25c2, 0x25f7, 1),
            new Range16 (0x2600, 0x266e, 1),
            new Range16 (0x2670, 0x2767, 1),
            new Range16 (0x2794, 0x27bf, 1),
            new Range16 (0x2800, 0x28ff, 1),
            new Range16 (0x2b00, 0x2b2f, 1),
            new Range16 (0x2b45, 0x2b46, 1),
            new Range16 (0x2b4d, 0x2b73, 1),
            new Range16 (0x2b76, 0x2b95, 1),
            new Range16 (0x2b97, 0x2bff, 1),
            new Range16 (0x2ce5, 0x2cea, 1),
            new Range16 (0x2e50, 0x2e51, 1),
            new Range16 (0x2e80, 0x2e99, 1),
            new Range16 (0x2e9b, 0x2ef3, 1),
            new Range16 (0x2f00, 0x2fd5, 1),
            new Range16 (0x2ff0, 0x2ffb, 1),
            new Range16 (0x3004, 0x3012, 14),
            new Range16 (0x3013, 0x3020, 13),
            new Range16 (0x3036, 0x3037, 1),
            new Range16 (0x303e, 0x303f, 1),
            new Range16 (0x3190, 0x3191, 1),
            new Range16 (0x3196, 0x319f, 1),
            new Range16 (0x31c0, 0x31e3, 1),
            new Range16 (0x3200, 0x321e, 1),
            new Range16 (0x322a, 0x3247, 1),
            new Range16 (0x3250, 0x3260, 16),
            new Range16 (0x3261, 0x327f, 1),
            new Range16 (0x328a, 0x32b0, 1),
            new Range16 (0x32c0, 0x33ff, 1),
            new Range16 (0x4dc0, 0x4dff, 1),
            new Range16 (0xa490, 0xa4c6, 1),
            new Range16 (0xa828, 0xa82b, 1),
            new Range16 (0xa836, 0xa837, 1),
            new Range16 (0xa839, 0xaa77, 574),
            new Range16 (0xaa78, 0xaa79, 1),
            new Range16 (0xfd40, 0xfd4f, 1),
            new Range16 (0xfdcf, 0xfdfd, 46),
            new Range16 (0xfdfe, 0xfdff, 1),
            new Range16 (0xffe4, 0xffe8, 4),
            new Range16 (0xffed, 0xffee, 1),
            new Range16 (0xfffc, 0xfffd, 1),
                },
                r32: new Range32[] {
            new Range32 (0x10137, 0x1013f, 1),
            new Range32 (0x10179, 0x10189, 1),
            new Range32 (0x1018c, 0x1018e, 1),
            new Range32 (0x10190, 0x1019c, 1),
            new Range32 (0x101a0, 0x101d0, 48),
            new Range32 (0x101d1, 0x101fc, 1),
            new Range32 (0x10877, 0x10878, 1),
            new Range32 (0x10ac8, 0x1173f, 3191),
            new Range32 (0x11fd5, 0x11fdc, 1),
            new Range32 (0x11fe1, 0x11ff1, 1),
            new Range32 (0x16b3c, 0x16b3f, 1),
            new Range32 (0x16b45, 0x1bc9c, 20823),
            new Range32 (0x1cf50, 0x1cfc3, 1),
            new Range32 (0x1d000, 0x1d0f5, 1),
            new Range32 (0x1d100, 0x1d126, 1),
            new Range32 (0x1d129, 0x1d164, 1),
            new Range32 (0x1d16a, 0x1d16c, 1),
            new Range32 (0x1d183, 0x1d184, 1),
            new Range32 (0x1d18c, 0x1d1a9, 1),
            new Range32 (0x1d1ae, 0x1d1ea, 1),
            new Range32 (0x1d200, 0x1d241, 1),
            new Range32 (0x1d245, 0x1d300, 187),
            new Range32 (0x1d301, 0x1d356, 1),
            new Range32 (0x1d800, 0x1d9ff, 1),
            new Range32 (0x1da37, 0x1da3a, 1),
            new Range32 (0x1da6d, 0x1da74, 1),
            new Range32 (0x1da76, 0x1da83, 1),
            new Range32 (0x1da85, 0x1da86, 1),
            new Range32 (0x1e14f, 0x1ecac, 2909),
            new Range32 (0x1ed2e, 0x1f000, 722),
            new Range32 (0x1f001, 0x1f02b, 1),
            new Range32 (0x1f030, 0x1f093, 1),
            new Range32 (0x1f0a0, 0x1f0ae, 1),
            new Range32 (0x1f0b1, 0x1f0bf, 1),
            new Range32 (0x1f0c1, 0x1f0cf, 1),
            new Range32 (0x1f0d1, 0x1f0f5, 1),
            new Range32 (0x1f10d, 0x1f1ad, 1),
            new Range32 (0x1f1e6, 0x1f202, 1),
            new Range32 (0x1f210, 0x1f23b, 1),
            new Range32 (0x1f240, 0x1f248, 1),
            new Range32 (0x1f250, 0x1f251, 1),
            new Range32 (0x1f260, 0x1f265, 1),
            new Range32 (0x1f300, 0x1f3fa, 1),
            new Range32 (0x1f400, 0x1f6d7, 1),
            new Range32 (0x1f6dc, 0x1f6ec, 1),
            new Range32 (0x1f6f0, 0x1f6fc, 1),
            new Range32 (0x1f700, 0x1f776, 1),
            new Range32 (0x1f77b, 0x1f7d9, 1),
            new Range32 (0x1f7e0, 0x1f7eb, 1),
            new Range32 (0x1f7f0, 0x1f800, 16),
            new Range32 (0x1f801, 0x1f80b, 1),
            new Range32 (0x1f810, 0x1f847, 1),
            new Range32 (0x1f850, 0x1f859, 1),
            new Range32 (0x1f860, 0x1f887, 1),
            new Range32 (0x1f890, 0x1f8ad, 1),
            new Range32 (0x1f8b0, 0x1f8b1, 1),
            new Range32 (0x1f900, 0x1fa53, 1),
            new Range32 (0x1fa60, 0x1fa6d, 1),
            new Range32 (0x1fa70, 0x1fa7c, 1),
            new Range32 (0x1fa80, 0x1fa88, 1),
            new Range32 (0x1fa90, 0x1fabd, 1),
            new Range32 (0x1fabf, 0x1fac5, 1),
            new Range32 (0x1face, 0x1fadb, 1),
            new Range32 (0x1fae0, 0x1fae8, 1),
            new Range32 (0x1faf0, 0x1faf8, 1),
            new Range32 (0x1fb00, 0x1fb92, 1),
            new Range32 (0x1fb94, 0x1fbca, 1),
                },
                latinOffset: 2
            );

            internal static RangeTable _Z = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0020, 0x00a0, 128),
            new Range16 (0x1680, 0x2000, 2432),
            new Range16 (0x2001, 0x200a, 1),
            new Range16 (0x2028, 0x2029, 1),
            new Range16 (0x202f, 0x205f, 48),
            new Range16 (0x3000, 0x3000, 1),
                },
                latinOffset: 1
            );

            internal static RangeTable _Zl = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x2028, 0x2028, 1),
                }
            );

            internal static RangeTable _Zp = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x2029, 0x2029, 1),
                }
            );

            internal static RangeTable _Zs = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0020, 0x00a0, 128),
            new Range16 (0x1680, 0x2000, 2432),
            new Range16 (0x2001, 0x200a, 1),
            new Range16 (0x202f, 0x205f, 48),
            new Range16 (0x3000, 0x3000, 1),
                },
                latinOffset: 1
            );

            /// <summary>Cc is the set of Unicode characters in category Cc.</summary>
            public static RangeTable Cc => _Cc;
            /// <summary>Cf is the set of Unicode characters in category Cf.</summary>
            public static RangeTable Cf => _Cf;
            /// <summary>Co is the set of Unicode characters in category Co.</summary>
            public static RangeTable Co => _Co;
            /// <summary>Cs is the set of Unicode characters in category Cs.</summary>
            public static RangeTable Cs => _Cs;
            /// <summary>Digit is the set of Unicode characters with the "decimal digit" property.</summary>
            public static RangeTable Digit => _Nd;
            /// <summary>Nd is the set of Unicode characters in category Nd.</summary>
            public static RangeTable Nd => _Nd;
            /// <summary>Letter/L is the set of Unicode letters, category L.</summary>
            public static RangeTable Letter => _L;
            /// <summary>Letter/L is the set of Unicode letters, category L.</summary>
            public static RangeTable L => _L;
            /// <summary>Lm is the set of Unicode characters in category Lm.</summary>
            public static RangeTable Lm => _Lm;
            /// <summary>Lo is the set of Unicode characters in category Lo.</summary>
            public static RangeTable Lo => _Lo;
            /// <summary>Lower is the set of Unicode lower case letters.</summary>;
            public static RangeTable Lower => _Ll;
            /// <summary>Ll is the set of Unicode characters in category Ll.</summary>
            public static RangeTable Ll => _Ll;
            /// <summary>Mark/M is the set of Unicode mark characters, category M.</summary>
            public static RangeTable Mark => _M;
            /// <summary>Mark/M is the set of Unicode mark characters, category M.</summary>;
            public static RangeTable M => _M;
            /// <summary>Mc is the set of Unicode characters in category Mc.</summary>
            public static RangeTable Mc => _Mc;
            /// <summary>Me is the set of Unicode characters in category Me.</summary>
            public static RangeTable Me => _Me;
            /// <summary>Mn is the set of Unicode characters in category Mn.</summary>
            public static RangeTable Mn => _Mn;
            /// <summary>Nl is the set of Unicode characters in category Nl.</summary>
            public static RangeTable Nl => _Nl;
            /// <summary>No is the set of Unicode characters in category No.</summary>
            public static RangeTable No => _No;
            /// <summary>Number/N is the set of Unicode number characters, category N.</summary>
            public static RangeTable Number => _N;
            /// <summary>Number/N is the set of Unicode number characters, category N.</summary>;
            public static RangeTable N => _N;
            /// <summary>Other/C is the set of Unicode control and special characters, category C.</summary>
            public static RangeTable Other => _C;
            /// <summary>Other/C is the set of Unicode control and special characters, category C.</summary>
            public static RangeTable C => _C;
            /// <summary>Pc is the set of Unicode characters in category Pc.</summary>
            public static RangeTable Pc => _Pc;
            /// <summary>Pd is the set of Unicode characters in category Pd.</summary>
            public static RangeTable Pd => _Pd;
            /// <summary>Pe is the set of Unicode characters in category Pe.</summary>
            public static RangeTable Pe => _Pe;
            /// <summary>Pf is the set of Unicode characters in category Pf.</summary>
            public static RangeTable Pf => _Pf;
            /// <summary>Pi is the set of Unicode characters in category Pi.</summary>
            public static RangeTable Pi => _Pi;
            /// <summary>Po is the set of Unicode characters in category Po.</summary>
            public static RangeTable Po => _Po;
            /// <summary>Ps is the set of Unicode characters in category Ps.</summary>
            public static RangeTable Ps => _Ps;
            /// <summary>Punct/P is the set of Unicode punctuation characters, category P.</summary>
            public static RangeTable Punct => _P;
            /// <summary>Punct/P is the set of Unicode punctuation characters, category P.</summary>;
            public static RangeTable P => _P;
            /// <summary>Sc is the set of Unicode characters in category Sc.</summary>
            public static RangeTable Sc => _Sc;
            /// <summary>Sk is the set of Unicode characters in category Sk.</summary>
            public static RangeTable Sk => _Sk;
            /// <summary>Sm is the set of Unicode characters in category Sm.</summary>
            public static RangeTable Sm => _Sm;
            /// <summary>So is the set of Unicode characters in category So.</summary>
            public static RangeTable So => _So;
            /// <summary>Space/Z is the set of Unicode space characters, category Z.</summary>
            public static RangeTable Space => _Z;
            /// <summary>Space/Z is the set of Unicode space characters, category Z.</summary>;
            public static RangeTable Z => _Z;
            /// <summary>Symbol/S is the set of Unicode symbol characters, category S.</summary>
            public static RangeTable Symbol => _S;
            /// <summary>Symbol/S is the set of Unicode symbol characters, category S.</summary>;
            public static RangeTable S => _S;
            /// <summary>Title is the set of Unicode title case letters.</summary>;
            public static RangeTable Title => _Lt;
            /// <summary>Lt is the set of Unicode characters in category Lt.</summary>
            public static RangeTable Lt => _Lt;
            /// <summary>Upper is the set of Unicode upper case letters.</summary>;
            public static RangeTable Upper => _Lu;
            /// <summary>Lu is the set of Unicode characters in category Lu.</summary>
            public static RangeTable Lu => _Lu;
            /// <summary>Zl is the set of Unicode characters in category Zl.</summary>
            public static RangeTable Zl => _Zl;
            /// <summary>Zp is the set of Unicode characters in category Zp.</summary>
            public static RangeTable Zp => _Zp;
            /// <summary>Zs is the set of Unicode characters in category Zs.</summary>
            public static RangeTable Zs => _Zs;
        }

        // Generated by running
        //	maketables --scripts=all --url=https://www.unicode.org/Public/15.0.0/ucd/
        // DO NOT EDIT

        /// <summary>Static class containing the Unicode script tables.</summary>
        /// <remarks><para>There are static properties that can be used to fetch a specific category, or you can use the <see cref="T:NStack.Unicode.Script.Get"/> method in this class to retrieve the range table by its script name</para></remarks>
        public static class Script
        {
            /// <summary>Retrieves the specified RangeTable from the Unicode script name.</summary>
            /// <param name="scriptName">The unicode script name</param>
            public static RangeTable Get(string scriptName) => Scripts[scriptName];
            // Scripts is the set of Unicode script tables.
            static Dictionary<string, RangeTable> Scripts = new Dictionary<string, RangeTable>(){
            { "Adlam", Adlam },
            { "Ahom", Ahom },
            { "Anatolian_Hieroglyphs", Anatolian_Hieroglyphs },
            { "Arabic", Arabic },
            { "Armenian", Armenian },
            { "Avestan", Avestan },
            { "Balinese", Balinese },
            { "Bamum", Bamum },
            { "Bassa_Vah", Bassa_Vah },
            { "Batak", Batak },
            { "Bengali", Bengali },
            { "Bhaiksuki", Bhaiksuki },
            { "Bopomofo", Bopomofo },
            { "Brahmi", Brahmi },
            { "Braille", Braille },
            { "Buginese", Buginese },
            { "Buhid", Buhid },
            { "Canadian_Aboriginal", Canadian_Aboriginal },
            { "Carian", Carian },
            { "Caucasian_Albanian", Caucasian_Albanian },
            { "Chakma", Chakma },
            { "Cham", Cham },
            { "Cherokee", Cherokee },
            { "Chorasmian", Chorasmian },
            { "Common", Common },
            { "Coptic", Coptic },
            { "Cuneiform", Cuneiform },
            { "Cypriot", Cypriot },
            { "Cypro_Minoan", Cypro_Minoan },
            { "Cyrillic", Cyrillic },
            { "Deseret", Deseret },
            { "Devanagari", Devanagari },
            { "Dives_Akuru", Dives_Akuru },
            { "Dogra", Dogra },
            { "Duployan", Duployan },
            { "Egyptian_Hieroglyphs", Egyptian_Hieroglyphs },
            { "Elbasan", Elbasan },
            { "Elymaic", Elymaic },
            { "Ethiopic", Ethiopic },
            { "Georgian", Georgian },
            { "Glagolitic", Glagolitic },
            { "Gothic", Gothic },
            { "Grantha", Grantha },
            { "Greek", Greek },
            { "Gujarati", Gujarati },
            { "Gunjala_Gondi", Gunjala_Gondi },
            { "Gurmukhi", Gurmukhi },
            { "Han", Han },
            { "Hangul", Hangul },
            { "Hanifi_Rohingya", Hanifi_Rohingya },
            { "Hanunoo", Hanunoo },
            { "Hatran", Hatran },
            { "Hebrew", Hebrew },
            { "Hiragana", Hiragana },
            { "Imperial_Aramaic", Imperial_Aramaic },
            { "Inherited", Inherited },
            { "Inscriptional_Pahlavi", Inscriptional_Pahlavi },
            { "Inscriptional_Parthian", Inscriptional_Parthian },
            { "Javanese", Javanese },
            { "Kaithi", Kaithi },
            { "Kannada", Kannada },
            { "Katakana", Katakana },
            { "Kawi", Kawi },
            { "Kayah_Li", Kayah_Li },
            { "Kharoshthi", Kharoshthi },
            { "Khitan_Small_Script", Khitan_Small_Script },
            { "Khmer", Khmer },
            { "Khojki", Khojki },
            { "Khudawadi", Khudawadi },
            { "Lao", Lao },
            { "Latin", Latin },
            { "Lepcha", Lepcha },
            { "Limbu", Limbu },
            { "Linear_A", Linear_A },
            { "Linear_B", Linear_B },
            { "Lisu", Lisu },
            { "Lycian", Lycian },
            { "Lydian", Lydian },
            { "Mahajani", Mahajani },
            { "Makasar", Makasar },
            { "Malayalam", Malayalam },
            { "Mandaic", Mandaic },
            { "Manichaean", Manichaean },
            { "Marchen", Marchen },
            { "Masaram_Gondi", Masaram_Gondi },
            { "Medefaidrin", Medefaidrin },
            { "Meetei_Mayek", Meetei_Mayek },
            { "Mende_Kikakui", Mende_Kikakui },
            { "Meroitic_Cursive", Meroitic_Cursive },
            { "Meroitic_Hieroglyphs", Meroitic_Hieroglyphs },
            { "Miao", Miao },
            { "Modi", Modi },
            { "Mongolian", Mongolian },
            { "Mro", Mro },
            { "Multani", Multani },
            { "Myanmar", Myanmar },
            { "Nabataean", Nabataean },
            { "Nag_Mundari", Nag_Mundari },
            { "Nandinagari", Nandinagari },
            { "New_Tai_Lue", New_Tai_Lue },
            { "Newa", Newa },
            { "Nko", Nko },
            { "Nushu", Nushu },
            { "Nyiakeng_Puachue_Hmong", Nyiakeng_Puachue_Hmong },
            { "Ogham", Ogham },
            { "Ol_Chiki", Ol_Chiki },
            { "Old_Hungarian", Old_Hungarian },
            { "Old_Italic", Old_Italic },
            { "Old_North_Arabian", Old_North_Arabian },
            { "Old_Permic", Old_Permic },
            { "Old_Persian", Old_Persian },
            { "Old_Sogdian", Old_Sogdian },
            { "Old_South_Arabian", Old_South_Arabian },
            { "Old_Turkic", Old_Turkic },
            { "Old_Uyghur", Old_Uyghur },
            { "Oriya", Oriya },
            { "Osage", Osage },
            { "Osmanya", Osmanya },
            { "Pahawh_Hmong", Pahawh_Hmong },
            { "Palmyrene", Palmyrene },
            { "Pau_Cin_Hau", Pau_Cin_Hau },
            { "Phags_Pa", Phags_Pa },
            { "Phoenician", Phoenician },
            { "Psalter_Pahlavi", Psalter_Pahlavi },
            { "Rejang", Rejang },
            { "Runic", Runic },
            { "Samaritan", Samaritan },
            { "Saurashtra", Saurashtra },
            { "Sharada", Sharada },
            { "Shavian", Shavian },
            { "Siddham", Siddham },
            { "SignWriting", SignWriting },
            { "Sinhala", Sinhala },
            { "Sogdian", Sogdian },
            { "Sora_Sompeng", Sora_Sompeng },
            { "Soyombo", Soyombo },
            { "Sundanese", Sundanese },
            { "Syloti_Nagri", Syloti_Nagri },
            { "Syriac", Syriac },
            { "Tagalog", Tagalog },
            { "Tagbanwa", Tagbanwa },
            { "Tai_Le", Tai_Le },
            { "Tai_Tham", Tai_Tham },
            { "Tai_Viet", Tai_Viet },
            { "Takri", Takri },
            { "Tamil", Tamil },
            { "Tangsa", Tangsa },
            { "Tangut", Tangut },
            { "Telugu", Telugu },
            { "Thaana", Thaana },
            { "Thai", Thai },
            { "Tibetan", Tibetan },
            { "Tifinagh", Tifinagh },
            { "Tirhuta", Tirhuta },
            { "Toto", Toto },
            { "Ugaritic", Ugaritic },
            { "Vai", Vai },
            { "Vithkuqi", Vithkuqi },
            { "Wancho", Wancho },
            { "Warang_Citi", Warang_Citi },
            { "Yezidi", Yezidi },
            { "Yi", Yi },
            { "Zanabazar_Square", Zanabazar_Square },
        };

            internal static RangeTable _Adlam = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x1e900, 0x1e94b, 1),
            new Range32 (0x1e950, 0x1e959, 1),
            new Range32 (0x1e95e, 0x1e95f, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Ahom = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x11700, 0x1171a, 1),
            new Range32 (0x1171d, 0x1172b, 1),
            new Range32 (0x11730, 0x11746, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Anatolian_Hieroglyphs = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x14400, 0x14646, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Arabic = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0600, 0x0604, 1),
            new Range16 (0x0606, 0x060b, 1),
            new Range16 (0x060d, 0x061a, 1),
            new Range16 (0x061c, 0x061e, 1),
            new Range16 (0x0620, 0x063f, 1),
            new Range16 (0x0641, 0x064a, 1),
            new Range16 (0x0656, 0x066f, 1),
            new Range16 (0x0671, 0x06dc, 1),
            new Range16 (0x06de, 0x06ff, 1),
            new Range16 (0x0750, 0x077f, 1),
            new Range16 (0x0870, 0x088e, 1),
            new Range16 (0x0890, 0x0891, 1),
            new Range16 (0x0898, 0x08e1, 1),
            new Range16 (0x08e3, 0x08ff, 1),
            new Range16 (0xfb50, 0xfbc2, 1),
            new Range16 (0xfbd3, 0xfd3d, 1),
            new Range16 (0xfd40, 0xfd8f, 1),
            new Range16 (0xfd92, 0xfdc7, 1),
            new Range16 (0xfdcf, 0xfdcf, 1),
            new Range16 (0xfdf0, 0xfdff, 1),
            new Range16 (0xfe70, 0xfe74, 1),
            new Range16 (0xfe76, 0xfefc, 1),
                },
                r32: new Range32[] {
            new Range32 (0x10e60, 0x10e7e, 1),
            new Range32 (0x10efd, 0x10eff, 1),
            new Range32 (0x1ee00, 0x1ee03, 1),
            new Range32 (0x1ee05, 0x1ee1f, 1),
            new Range32 (0x1ee21, 0x1ee22, 1),
            new Range32 (0x1ee24, 0x1ee24, 1),
            new Range32 (0x1ee27, 0x1ee27, 1),
            new Range32 (0x1ee29, 0x1ee32, 1),
            new Range32 (0x1ee34, 0x1ee37, 1),
            new Range32 (0x1ee39, 0x1ee39, 1),
            new Range32 (0x1ee3b, 0x1ee3b, 1),
            new Range32 (0x1ee42, 0x1ee42, 1),
            new Range32 (0x1ee47, 0x1ee47, 1),
            new Range32 (0x1ee49, 0x1ee49, 1),
            new Range32 (0x1ee4b, 0x1ee4b, 1),
            new Range32 (0x1ee4d, 0x1ee4f, 1),
            new Range32 (0x1ee51, 0x1ee52, 1),
            new Range32 (0x1ee54, 0x1ee54, 1),
            new Range32 (0x1ee57, 0x1ee57, 1),
            new Range32 (0x1ee59, 0x1ee59, 1),
            new Range32 (0x1ee5b, 0x1ee5b, 1),
            new Range32 (0x1ee5d, 0x1ee5d, 1),
            new Range32 (0x1ee5f, 0x1ee5f, 1),
            new Range32 (0x1ee61, 0x1ee62, 1),
            new Range32 (0x1ee64, 0x1ee64, 1),
            new Range32 (0x1ee67, 0x1ee6a, 1),
            new Range32 (0x1ee6c, 0x1ee72, 1),
            new Range32 (0x1ee74, 0x1ee77, 1),
            new Range32 (0x1ee79, 0x1ee7c, 1),
            new Range32 (0x1ee7e, 0x1ee7e, 1),
            new Range32 (0x1ee80, 0x1ee89, 1),
            new Range32 (0x1ee8b, 0x1ee9b, 1),
            new Range32 (0x1eea1, 0x1eea3, 1),
            new Range32 (0x1eea5, 0x1eea9, 1),
            new Range32 (0x1eeab, 0x1eebb, 1),
            new Range32 (0x1eef0, 0x1eef1, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Armenian = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0531, 0x0556, 1),
            new Range16 (0x0559, 0x058a, 1),
            new Range16 (0x058d, 0x058f, 1),
            new Range16 (0xfb13, 0xfb17, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Avestan = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x10b00, 0x10b35, 1),
            new Range32 (0x10b39, 0x10b3f, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Balinese = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x1b00, 0x1b4c, 1),
            new Range16 (0x1b50, 0x1b7e, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Bamum = new RangeTable(
                r16: new Range16[] {
            new Range16 (0xa6a0, 0xa6f7, 1),
                },
                r32: new Range32[] {
            new Range32 (0x16800, 0x16a38, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Bassa_Vah = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x16ad0, 0x16aed, 1),
            new Range32 (0x16af0, 0x16af5, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Batak = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x1bc0, 0x1bf3, 1),
            new Range16 (0x1bfc, 0x1bff, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Bengali = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0980, 0x0983, 1),
            new Range16 (0x0985, 0x098c, 1),
            new Range16 (0x098f, 0x0990, 1),
            new Range16 (0x0993, 0x09a8, 1),
            new Range16 (0x09aa, 0x09b0, 1),
            new Range16 (0x09b2, 0x09b2, 1),
            new Range16 (0x09b6, 0x09b9, 1),
            new Range16 (0x09bc, 0x09c4, 1),
            new Range16 (0x09c7, 0x09c8, 1),
            new Range16 (0x09cb, 0x09ce, 1),
            new Range16 (0x09d7, 0x09d7, 1),
            new Range16 (0x09dc, 0x09dd, 1),
            new Range16 (0x09df, 0x09e3, 1),
            new Range16 (0x09e6, 0x09fe, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Bhaiksuki = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x11c00, 0x11c08, 1),
            new Range32 (0x11c0a, 0x11c36, 1),
            new Range32 (0x11c38, 0x11c45, 1),
            new Range32 (0x11c50, 0x11c6c, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Bopomofo = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x02ea, 0x02eb, 1),
            new Range16 (0x3105, 0x312f, 1),
            new Range16 (0x31a0, 0x31bf, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Brahmi = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x11000, 0x1104d, 1),
            new Range32 (0x11052, 0x11075, 1),
            new Range32 (0x1107f, 0x1107f, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Braille = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x2800, 0x28ff, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Buginese = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x1a00, 0x1a1b, 1),
            new Range16 (0x1a1e, 0x1a1f, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Buhid = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x1740, 0x1753, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Canadian_Aboriginal = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x1400, 0x167f, 1),
            new Range16 (0x18b0, 0x18f5, 1),
                },
                r32: new Range32[] {
            new Range32 (0x11ab0, 0x11abf, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Carian = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x102a0, 0x102d0, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Caucasian_Albanian = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x10530, 0x10563, 1),
            new Range32 (0x1056f, 0x1056f, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Chakma = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x11100, 0x11134, 1),
            new Range32 (0x11136, 0x11147, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Cham = new RangeTable(
                r16: new Range16[] {
            new Range16 (0xaa00, 0xaa36, 1),
            new Range16 (0xaa40, 0xaa4d, 1),
            new Range16 (0xaa50, 0xaa59, 1),
            new Range16 (0xaa5c, 0xaa5f, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Cherokee = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x13a0, 0x13f5, 1),
            new Range16 (0x13f8, 0x13fd, 1),
            new Range16 (0xab70, 0xabbf, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Chorasmian = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x10fb0, 0x10fcb, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Common = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0000, 0x0040, 1),
            new Range16 (0x005b, 0x0060, 1),
            new Range16 (0x007b, 0x00a9, 1),
            new Range16 (0x00ab, 0x00b9, 1),
            new Range16 (0x00bb, 0x00bf, 1),
            new Range16 (0x00d7, 0x00d7, 1),
            new Range16 (0x00f7, 0x00f7, 1),
            new Range16 (0x02b9, 0x02df, 1),
            new Range16 (0x02e5, 0x02e9, 1),
            new Range16 (0x02ec, 0x02ff, 1),
            new Range16 (0x0374, 0x0374, 1),
            new Range16 (0x037e, 0x037e, 1),
            new Range16 (0x0385, 0x0385, 1),
            new Range16 (0x0387, 0x0387, 1),
            new Range16 (0x0605, 0x0605, 1),
            new Range16 (0x060c, 0x060c, 1),
            new Range16 (0x061b, 0x061b, 1),
            new Range16 (0x061f, 0x061f, 1),
            new Range16 (0x0640, 0x0640, 1),
            new Range16 (0x06dd, 0x06dd, 1),
            new Range16 (0x08e2, 0x08e2, 1),
            new Range16 (0x0964, 0x0965, 1),
            new Range16 (0x0e3f, 0x0e3f, 1),
            new Range16 (0x0fd5, 0x0fd8, 1),
            new Range16 (0x10fb, 0x10fb, 1),
            new Range16 (0x16eb, 0x16ed, 1),
            new Range16 (0x1735, 0x1736, 1),
            new Range16 (0x1802, 0x1803, 1),
            new Range16 (0x1805, 0x1805, 1),
            new Range16 (0x1cd3, 0x1cd3, 1),
            new Range16 (0x1ce1, 0x1ce1, 1),
            new Range16 (0x1ce9, 0x1cec, 1),
            new Range16 (0x1cee, 0x1cf3, 1),
            new Range16 (0x1cf5, 0x1cf7, 1),
            new Range16 (0x1cfa, 0x1cfa, 1),
            new Range16 (0x2000, 0x200b, 1),
            new Range16 (0x200e, 0x2064, 1),
            new Range16 (0x2066, 0x2070, 1),
            new Range16 (0x2074, 0x207e, 1),
            new Range16 (0x2080, 0x208e, 1),
            new Range16 (0x20a0, 0x20c0, 1),
            new Range16 (0x2100, 0x2125, 1),
            new Range16 (0x2127, 0x2129, 1),
            new Range16 (0x212c, 0x2131, 1),
            new Range16 (0x2133, 0x214d, 1),
            new Range16 (0x214f, 0x215f, 1),
            new Range16 (0x2189, 0x218b, 1),
            new Range16 (0x2190, 0x2426, 1),
            new Range16 (0x2440, 0x244a, 1),
            new Range16 (0x2460, 0x27ff, 1),
            new Range16 (0x2900, 0x2b73, 1),
            new Range16 (0x2b76, 0x2b95, 1),
            new Range16 (0x2b97, 0x2bff, 1),
            new Range16 (0x2e00, 0x2e5d, 1),
            new Range16 (0x2ff0, 0x2ffb, 1),
            new Range16 (0x3000, 0x3004, 1),
            new Range16 (0x3006, 0x3006, 1),
            new Range16 (0x3008, 0x3020, 1),
            new Range16 (0x3030, 0x3037, 1),
            new Range16 (0x303c, 0x303f, 1),
            new Range16 (0x309b, 0x309c, 1),
            new Range16 (0x30a0, 0x30a0, 1),
            new Range16 (0x30fb, 0x30fc, 1),
            new Range16 (0x3190, 0x319f, 1),
            new Range16 (0x31c0, 0x31e3, 1),
            new Range16 (0x3220, 0x325f, 1),
            new Range16 (0x327f, 0x32cf, 1),
            new Range16 (0x32ff, 0x32ff, 1),
            new Range16 (0x3358, 0x33ff, 1),
            new Range16 (0x4dc0, 0x4dff, 1),
            new Range16 (0xa700, 0xa721, 1),
            new Range16 (0xa788, 0xa78a, 1),
            new Range16 (0xa830, 0xa839, 1),
            new Range16 (0xa92e, 0xa92e, 1),
            new Range16 (0xa9cf, 0xa9cf, 1),
            new Range16 (0xab5b, 0xab5b, 1),
            new Range16 (0xab6a, 0xab6b, 1),
            new Range16 (0xfd3e, 0xfd3f, 1),
            new Range16 (0xfe10, 0xfe19, 1),
            new Range16 (0xfe30, 0xfe52, 1),
            new Range16 (0xfe54, 0xfe66, 1),
            new Range16 (0xfe68, 0xfe6b, 1),
            new Range16 (0xfeff, 0xfeff, 1),
            new Range16 (0xff01, 0xff20, 1),
            new Range16 (0xff3b, 0xff40, 1),
            new Range16 (0xff5b, 0xff65, 1),
            new Range16 (0xff70, 0xff70, 1),
            new Range16 (0xff9e, 0xff9f, 1),
            new Range16 (0xffe0, 0xffe6, 1),
            new Range16 (0xffe8, 0xffee, 1),
            new Range16 (0xfff9, 0xfffd, 1),
                },
                r32: new Range32[] {
            new Range32 (0x10100, 0x10102, 1),
            new Range32 (0x10107, 0x10133, 1),
            new Range32 (0x10137, 0x1013f, 1),
            new Range32 (0x10190, 0x1019c, 1),
            new Range32 (0x101d0, 0x101fc, 1),
            new Range32 (0x102e1, 0x102fb, 1),
            new Range32 (0x1bca0, 0x1bca3, 1),
            new Range32 (0x1cf50, 0x1cfc3, 1),
            new Range32 (0x1d000, 0x1d0f5, 1),
            new Range32 (0x1d100, 0x1d126, 1),
            new Range32 (0x1d129, 0x1d166, 1),
            new Range32 (0x1d16a, 0x1d17a, 1),
            new Range32 (0x1d183, 0x1d184, 1),
            new Range32 (0x1d18c, 0x1d1a9, 1),
            new Range32 (0x1d1ae, 0x1d1ea, 1),
            new Range32 (0x1d2c0, 0x1d2d3, 1),
            new Range32 (0x1d2e0, 0x1d2f3, 1),
            new Range32 (0x1d300, 0x1d356, 1),
            new Range32 (0x1d360, 0x1d378, 1),
            new Range32 (0x1d400, 0x1d454, 1),
            new Range32 (0x1d456, 0x1d49c, 1),
            new Range32 (0x1d49e, 0x1d49f, 1),
            new Range32 (0x1d4a2, 0x1d4a2, 1),
            new Range32 (0x1d4a5, 0x1d4a6, 1),
            new Range32 (0x1d4a9, 0x1d4ac, 1),
            new Range32 (0x1d4ae, 0x1d4b9, 1),
            new Range32 (0x1d4bb, 0x1d4bb, 1),
            new Range32 (0x1d4bd, 0x1d4c3, 1),
            new Range32 (0x1d4c5, 0x1d505, 1),
            new Range32 (0x1d507, 0x1d50a, 1),
            new Range32 (0x1d50d, 0x1d514, 1),
            new Range32 (0x1d516, 0x1d51c, 1),
            new Range32 (0x1d51e, 0x1d539, 1),
            new Range32 (0x1d53b, 0x1d53e, 1),
            new Range32 (0x1d540, 0x1d544, 1),
            new Range32 (0x1d546, 0x1d546, 1),
            new Range32 (0x1d54a, 0x1d550, 1),
            new Range32 (0x1d552, 0x1d6a5, 1),
            new Range32 (0x1d6a8, 0x1d7cb, 1),
            new Range32 (0x1d7ce, 0x1d7ff, 1),
            new Range32 (0x1ec71, 0x1ecb4, 1),
            new Range32 (0x1ed01, 0x1ed3d, 1),
            new Range32 (0x1f000, 0x1f02b, 1),
            new Range32 (0x1f030, 0x1f093, 1),
            new Range32 (0x1f0a0, 0x1f0ae, 1),
            new Range32 (0x1f0b1, 0x1f0bf, 1),
            new Range32 (0x1f0c1, 0x1f0cf, 1),
            new Range32 (0x1f0d1, 0x1f0f5, 1),
            new Range32 (0x1f100, 0x1f1ad, 1),
            new Range32 (0x1f1e6, 0x1f1ff, 1),
            new Range32 (0x1f201, 0x1f202, 1),
            new Range32 (0x1f210, 0x1f23b, 1),
            new Range32 (0x1f240, 0x1f248, 1),
            new Range32 (0x1f250, 0x1f251, 1),
            new Range32 (0x1f260, 0x1f265, 1),
            new Range32 (0x1f300, 0x1f6d7, 1),
            new Range32 (0x1f6dc, 0x1f6ec, 1),
            new Range32 (0x1f6f0, 0x1f6fc, 1),
            new Range32 (0x1f700, 0x1f776, 1),
            new Range32 (0x1f77b, 0x1f7d9, 1),
            new Range32 (0x1f7e0, 0x1f7eb, 1),
            new Range32 (0x1f7f0, 0x1f7f0, 1),
            new Range32 (0x1f800, 0x1f80b, 1),
            new Range32 (0x1f810, 0x1f847, 1),
            new Range32 (0x1f850, 0x1f859, 1),
            new Range32 (0x1f860, 0x1f887, 1),
            new Range32 (0x1f890, 0x1f8ad, 1),
            new Range32 (0x1f8b0, 0x1f8b1, 1),
            new Range32 (0x1f900, 0x1fa53, 1),
            new Range32 (0x1fa60, 0x1fa6d, 1),
            new Range32 (0x1fa70, 0x1fa7c, 1),
            new Range32 (0x1fa80, 0x1fa88, 1),
            new Range32 (0x1fa90, 0x1fabd, 1),
            new Range32 (0x1fabf, 0x1fac5, 1),
            new Range32 (0x1face, 0x1fadb, 1),
            new Range32 (0x1fae0, 0x1fae8, 1),
            new Range32 (0x1faf0, 0x1faf8, 1),
            new Range32 (0x1fb00, 0x1fb92, 1),
            new Range32 (0x1fb94, 0x1fbca, 1),
            new Range32 (0x1fbf0, 0x1fbf9, 1),
            new Range32 (0xe0001, 0xe0001, 1),
            new Range32 (0xe0020, 0xe007f, 1),
                },
                latinOffset: 7
            ); /* RangeTable */

            internal static RangeTable _Coptic = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x03e2, 0x03ef, 1),
            new Range16 (0x2c80, 0x2cf3, 1),
            new Range16 (0x2cf9, 0x2cff, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Cuneiform = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x12000, 0x12399, 1),
            new Range32 (0x12400, 0x1246e, 1),
            new Range32 (0x12470, 0x12474, 1),
            new Range32 (0x12480, 0x12543, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Cypriot = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x10800, 0x10805, 1),
            new Range32 (0x10808, 0x10808, 1),
            new Range32 (0x1080a, 0x10835, 1),
            new Range32 (0x10837, 0x10838, 1),
            new Range32 (0x1083c, 0x1083c, 1),
            new Range32 (0x1083f, 0x1083f, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Cypro_Minoan = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x12f90, 0x12ff2, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Cyrillic = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0400, 0x0484, 1),
            new Range16 (0x0487, 0x052f, 1),
            new Range16 (0x1c80, 0x1c88, 1),
            new Range16 (0x1d2b, 0x1d2b, 1),
            new Range16 (0x1d78, 0x1d78, 1),
            new Range16 (0x2de0, 0x2dff, 1),
            new Range16 (0xa640, 0xa69f, 1),
            new Range16 (0xfe2e, 0xfe2f, 1),
                },
                r32: new Range32[] {
            new Range32 (0x1e030, 0x1e06d, 1),
            new Range32 (0x1e08f, 0x1e08f, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Deseret = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x10400, 0x1044f, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Devanagari = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0900, 0x0950, 1),
            new Range16 (0x0955, 0x0963, 1),
            new Range16 (0x0966, 0x097f, 1),
            new Range16 (0xa8e0, 0xa8ff, 1),
                },
                r32: new Range32[] {
            new Range32 (0x11b00, 0x11b09, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Dives_Akuru = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x11900, 0x11906, 1),
            new Range32 (0x11909, 0x11909, 1),
            new Range32 (0x1190c, 0x11913, 1),
            new Range32 (0x11915, 0x11916, 1),
            new Range32 (0x11918, 0x11935, 1),
            new Range32 (0x11937, 0x11938, 1),
            new Range32 (0x1193b, 0x11946, 1),
            new Range32 (0x11950, 0x11959, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Dogra = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x11800, 0x1183b, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Duployan = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x1bc00, 0x1bc6a, 1),
            new Range32 (0x1bc70, 0x1bc7c, 1),
            new Range32 (0x1bc80, 0x1bc88, 1),
            new Range32 (0x1bc90, 0x1bc99, 1),
            new Range32 (0x1bc9c, 0x1bc9f, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Egyptian_Hieroglyphs = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x13000, 0x13455, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Elbasan = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x10500, 0x10527, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Elymaic = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x10fe0, 0x10ff6, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Ethiopic = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x1200, 0x1248, 1),
            new Range16 (0x124a, 0x124d, 1),
            new Range16 (0x1250, 0x1256, 1),
            new Range16 (0x1258, 0x1258, 1),
            new Range16 (0x125a, 0x125d, 1),
            new Range16 (0x1260, 0x1288, 1),
            new Range16 (0x128a, 0x128d, 1),
            new Range16 (0x1290, 0x12b0, 1),
            new Range16 (0x12b2, 0x12b5, 1),
            new Range16 (0x12b8, 0x12be, 1),
            new Range16 (0x12c0, 0x12c0, 1),
            new Range16 (0x12c2, 0x12c5, 1),
            new Range16 (0x12c8, 0x12d6, 1),
            new Range16 (0x12d8, 0x1310, 1),
            new Range16 (0x1312, 0x1315, 1),
            new Range16 (0x1318, 0x135a, 1),
            new Range16 (0x135d, 0x137c, 1),
            new Range16 (0x1380, 0x1399, 1),
            new Range16 (0x2d80, 0x2d96, 1),
            new Range16 (0x2da0, 0x2da6, 1),
            new Range16 (0x2da8, 0x2dae, 1),
            new Range16 (0x2db0, 0x2db6, 1),
            new Range16 (0x2db8, 0x2dbe, 1),
            new Range16 (0x2dc0, 0x2dc6, 1),
            new Range16 (0x2dc8, 0x2dce, 1),
            new Range16 (0x2dd0, 0x2dd6, 1),
            new Range16 (0x2dd8, 0x2dde, 1),
            new Range16 (0xab01, 0xab06, 1),
            new Range16 (0xab09, 0xab0e, 1),
            new Range16 (0xab11, 0xab16, 1),
            new Range16 (0xab20, 0xab26, 1),
            new Range16 (0xab28, 0xab2e, 1),
                },
                r32: new Range32[] {
            new Range32 (0x1e7e0, 0x1e7e6, 1),
            new Range32 (0x1e7e8, 0x1e7eb, 1),
            new Range32 (0x1e7ed, 0x1e7ee, 1),
            new Range32 (0x1e7f0, 0x1e7fe, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Georgian = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x10a0, 0x10c5, 1),
            new Range16 (0x10c7, 0x10c7, 1),
            new Range16 (0x10cd, 0x10cd, 1),
            new Range16 (0x10d0, 0x10fa, 1),
            new Range16 (0x10fc, 0x10ff, 1),
            new Range16 (0x1c90, 0x1cba, 1),
            new Range16 (0x1cbd, 0x1cbf, 1),
            new Range16 (0x2d00, 0x2d25, 1),
            new Range16 (0x2d27, 0x2d27, 1),
            new Range16 (0x2d2d, 0x2d2d, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Glagolitic = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x2c00, 0x2c5f, 1),
                },
                r32: new Range32[] {
            new Range32 (0x1e000, 0x1e006, 1),
            new Range32 (0x1e008, 0x1e018, 1),
            new Range32 (0x1e01b, 0x1e021, 1),
            new Range32 (0x1e023, 0x1e024, 1),
            new Range32 (0x1e026, 0x1e02a, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Gothic = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x10330, 0x1034a, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Grantha = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x11300, 0x11303, 1),
            new Range32 (0x11305, 0x1130c, 1),
            new Range32 (0x1130f, 0x11310, 1),
            new Range32 (0x11313, 0x11328, 1),
            new Range32 (0x1132a, 0x11330, 1),
            new Range32 (0x11332, 0x11333, 1),
            new Range32 (0x11335, 0x11339, 1),
            new Range32 (0x1133c, 0x11344, 1),
            new Range32 (0x11347, 0x11348, 1),
            new Range32 (0x1134b, 0x1134d, 1),
            new Range32 (0x11350, 0x11350, 1),
            new Range32 (0x11357, 0x11357, 1),
            new Range32 (0x1135d, 0x11363, 1),
            new Range32 (0x11366, 0x1136c, 1),
            new Range32 (0x11370, 0x11374, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Greek = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0370, 0x0373, 1),
            new Range16 (0x0375, 0x0377, 1),
            new Range16 (0x037a, 0x037d, 1),
            new Range16 (0x037f, 0x037f, 1),
            new Range16 (0x0384, 0x0384, 1),
            new Range16 (0x0386, 0x0386, 1),
            new Range16 (0x0388, 0x038a, 1),
            new Range16 (0x038c, 0x038c, 1),
            new Range16 (0x038e, 0x03a1, 1),
            new Range16 (0x03a3, 0x03e1, 1),
            new Range16 (0x03f0, 0x03ff, 1),
            new Range16 (0x1d26, 0x1d2a, 1),
            new Range16 (0x1d5d, 0x1d61, 1),
            new Range16 (0x1d66, 0x1d6a, 1),
            new Range16 (0x1dbf, 0x1dbf, 1),
            new Range16 (0x1f00, 0x1f15, 1),
            new Range16 (0x1f18, 0x1f1d, 1),
            new Range16 (0x1f20, 0x1f45, 1),
            new Range16 (0x1f48, 0x1f4d, 1),
            new Range16 (0x1f50, 0x1f57, 1),
            new Range16 (0x1f59, 0x1f59, 1),
            new Range16 (0x1f5b, 0x1f5b, 1),
            new Range16 (0x1f5d, 0x1f5d, 1),
            new Range16 (0x1f5f, 0x1f7d, 1),
            new Range16 (0x1f80, 0x1fb4, 1),
            new Range16 (0x1fb6, 0x1fc4, 1),
            new Range16 (0x1fc6, 0x1fd3, 1),
            new Range16 (0x1fd6, 0x1fdb, 1),
            new Range16 (0x1fdd, 0x1fef, 1),
            new Range16 (0x1ff2, 0x1ff4, 1),
            new Range16 (0x1ff6, 0x1ffe, 1),
            new Range16 (0x2126, 0x2126, 1),
            new Range16 (0xab65, 0xab65, 1),
                },
                r32: new Range32[] {
            new Range32 (0x10140, 0x1018e, 1),
            new Range32 (0x101a0, 0x101a0, 1),
            new Range32 (0x1d200, 0x1d245, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Gujarati = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0a81, 0x0a83, 1),
            new Range16 (0x0a85, 0x0a8d, 1),
            new Range16 (0x0a8f, 0x0a91, 1),
            new Range16 (0x0a93, 0x0aa8, 1),
            new Range16 (0x0aaa, 0x0ab0, 1),
            new Range16 (0x0ab2, 0x0ab3, 1),
            new Range16 (0x0ab5, 0x0ab9, 1),
            new Range16 (0x0abc, 0x0ac5, 1),
            new Range16 (0x0ac7, 0x0ac9, 1),
            new Range16 (0x0acb, 0x0acd, 1),
            new Range16 (0x0ad0, 0x0ad0, 1),
            new Range16 (0x0ae0, 0x0ae3, 1),
            new Range16 (0x0ae6, 0x0af1, 1),
            new Range16 (0x0af9, 0x0aff, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Gunjala_Gondi = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x11d60, 0x11d65, 1),
            new Range32 (0x11d67, 0x11d68, 1),
            new Range32 (0x11d6a, 0x11d8e, 1),
            new Range32 (0x11d90, 0x11d91, 1),
            new Range32 (0x11d93, 0x11d98, 1),
            new Range32 (0x11da0, 0x11da9, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Gurmukhi = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0a01, 0x0a03, 1),
            new Range16 (0x0a05, 0x0a0a, 1),
            new Range16 (0x0a0f, 0x0a10, 1),
            new Range16 (0x0a13, 0x0a28, 1),
            new Range16 (0x0a2a, 0x0a30, 1),
            new Range16 (0x0a32, 0x0a33, 1),
            new Range16 (0x0a35, 0x0a36, 1),
            new Range16 (0x0a38, 0x0a39, 1),
            new Range16 (0x0a3c, 0x0a3c, 1),
            new Range16 (0x0a3e, 0x0a42, 1),
            new Range16 (0x0a47, 0x0a48, 1),
            new Range16 (0x0a4b, 0x0a4d, 1),
            new Range16 (0x0a51, 0x0a51, 1),
            new Range16 (0x0a59, 0x0a5c, 1),
            new Range16 (0x0a5e, 0x0a5e, 1),
            new Range16 (0x0a66, 0x0a76, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Han = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x2e80, 0x2e99, 1),
            new Range16 (0x2e9b, 0x2ef3, 1),
            new Range16 (0x2f00, 0x2fd5, 1),
            new Range16 (0x3005, 0x3005, 1),
            new Range16 (0x3007, 0x3007, 1),
            new Range16 (0x3021, 0x3029, 1),
            new Range16 (0x3038, 0x303b, 1),
            new Range16 (0x3400, 0x4dbf, 1),
            new Range16 (0x4e00, 0x9fff, 1),
            new Range16 (0xf900, 0xfa6d, 1),
            new Range16 (0xfa70, 0xfad9, 1),
                },
                r32: new Range32[] {
            new Range32 (0x16fe2, 0x16fe3, 1),
            new Range32 (0x16ff0, 0x16ff1, 1),
            new Range32 (0x20000, 0x2a6df, 1),
            new Range32 (0x2a700, 0x2b739, 1),
            new Range32 (0x2b740, 0x2b81d, 1),
            new Range32 (0x2b820, 0x2cea1, 1),
            new Range32 (0x2ceb0, 0x2ebe0, 1),
            new Range32 (0x2f800, 0x2fa1d, 1),
            new Range32 (0x30000, 0x3134a, 1),
            new Range32 (0x31350, 0x323af, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Hangul = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x1100, 0x11ff, 1),
            new Range16 (0x302e, 0x302f, 1),
            new Range16 (0x3131, 0x318e, 1),
            new Range16 (0x3200, 0x321e, 1),
            new Range16 (0x3260, 0x327e, 1),
            new Range16 (0xa960, 0xa97c, 1),
            new Range16 (0xac00, 0xd7a3, 1),
            new Range16 (0xd7b0, 0xd7c6, 1),
            new Range16 (0xd7cb, 0xd7fb, 1),
            new Range16 (0xffa0, 0xffbe, 1),
            new Range16 (0xffc2, 0xffc7, 1),
            new Range16 (0xffca, 0xffcf, 1),
            new Range16 (0xffd2, 0xffd7, 1),
            new Range16 (0xffda, 0xffdc, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Hanifi_Rohingya = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x10d00, 0x10d27, 1),
            new Range32 (0x10d30, 0x10d39, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Hanunoo = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x1720, 0x1734, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Hatran = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x108e0, 0x108f2, 1),
            new Range32 (0x108f4, 0x108f5, 1),
            new Range32 (0x108fb, 0x108ff, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Hebrew = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0591, 0x05c7, 1),
            new Range16 (0x05d0, 0x05ea, 1),
            new Range16 (0x05ef, 0x05f4, 1),
            new Range16 (0xfb1d, 0xfb36, 1),
            new Range16 (0xfb38, 0xfb3c, 1),
            new Range16 (0xfb3e, 0xfb3e, 1),
            new Range16 (0xfb40, 0xfb41, 1),
            new Range16 (0xfb43, 0xfb44, 1),
            new Range16 (0xfb46, 0xfb4f, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Hiragana = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x3041, 0x3096, 1),
            new Range16 (0x309d, 0x309f, 1),
                },
                r32: new Range32[] {
            new Range32 (0x1b001, 0x1b11f, 1),
            new Range32 (0x1b132, 0x1b132, 1),
            new Range32 (0x1b150, 0x1b152, 1),
            new Range32 (0x1f200, 0x1f200, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Imperial_Aramaic = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x10840, 0x10855, 1),
            new Range32 (0x10857, 0x1085f, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Inherited = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0300, 0x036f, 1),
            new Range16 (0x0485, 0x0486, 1),
            new Range16 (0x064b, 0x0655, 1),
            new Range16 (0x0670, 0x0670, 1),
            new Range16 (0x0951, 0x0954, 1),
            new Range16 (0x1ab0, 0x1ace, 1),
            new Range16 (0x1cd0, 0x1cd2, 1),
            new Range16 (0x1cd4, 0x1ce0, 1),
            new Range16 (0x1ce2, 0x1ce8, 1),
            new Range16 (0x1ced, 0x1ced, 1),
            new Range16 (0x1cf4, 0x1cf4, 1),
            new Range16 (0x1cf8, 0x1cf9, 1),
            new Range16 (0x1dc0, 0x1dff, 1),
            new Range16 (0x200c, 0x200d, 1),
            new Range16 (0x20d0, 0x20f0, 1),
            new Range16 (0x302a, 0x302d, 1),
            new Range16 (0x3099, 0x309a, 1),
            new Range16 (0xfe00, 0xfe0f, 1),
            new Range16 (0xfe20, 0xfe2d, 1),
                },
                r32: new Range32[] {
            new Range32 (0x101fd, 0x101fd, 1),
            new Range32 (0x102e0, 0x102e0, 1),
            new Range32 (0x1133b, 0x1133b, 1),
            new Range32 (0x1cf00, 0x1cf2d, 1),
            new Range32 (0x1cf30, 0x1cf46, 1),
            new Range32 (0x1d167, 0x1d169, 1),
            new Range32 (0x1d17b, 0x1d182, 1),
            new Range32 (0x1d185, 0x1d18b, 1),
            new Range32 (0x1d1aa, 0x1d1ad, 1),
            new Range32 (0xe0100, 0xe01ef, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Inscriptional_Pahlavi = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x10b60, 0x10b72, 1),
            new Range32 (0x10b78, 0x10b7f, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Inscriptional_Parthian = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x10b40, 0x10b55, 1),
            new Range32 (0x10b58, 0x10b5f, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Javanese = new RangeTable(
                r16: new Range16[] {
            new Range16 (0xa980, 0xa9cd, 1),
            new Range16 (0xa9d0, 0xa9d9, 1),
            new Range16 (0xa9de, 0xa9df, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Kaithi = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x11080, 0x110c2, 1),
            new Range32 (0x110cd, 0x110cd, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Kannada = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0c80, 0x0c8c, 1),
            new Range16 (0x0c8e, 0x0c90, 1),
            new Range16 (0x0c92, 0x0ca8, 1),
            new Range16 (0x0caa, 0x0cb3, 1),
            new Range16 (0x0cb5, 0x0cb9, 1),
            new Range16 (0x0cbc, 0x0cc4, 1),
            new Range16 (0x0cc6, 0x0cc8, 1),
            new Range16 (0x0cca, 0x0ccd, 1),
            new Range16 (0x0cd5, 0x0cd6, 1),
            new Range16 (0x0cdd, 0x0cde, 1),
            new Range16 (0x0ce0, 0x0ce3, 1),
            new Range16 (0x0ce6, 0x0cef, 1),
            new Range16 (0x0cf1, 0x0cf3, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Katakana = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x30a1, 0x30fa, 1),
            new Range16 (0x30fd, 0x30ff, 1),
            new Range16 (0x31f0, 0x31ff, 1),
            new Range16 (0x32d0, 0x32fe, 1),
            new Range16 (0x3300, 0x3357, 1),
            new Range16 (0xff66, 0xff6f, 1),
            new Range16 (0xff71, 0xff9d, 1),
                },
                r32: new Range32[] {
            new Range32 (0x1aff0, 0x1aff3, 1),
            new Range32 (0x1aff5, 0x1affb, 1),
            new Range32 (0x1affd, 0x1affe, 1),
            new Range32 (0x1b000, 0x1b000, 1),
            new Range32 (0x1b120, 0x1b122, 1),
            new Range32 (0x1b155, 0x1b155, 1),
            new Range32 (0x1b164, 0x1b167, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Kawi = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x11f00, 0x11f10, 1),
            new Range32 (0x11f12, 0x11f3a, 1),
            new Range32 (0x11f3e, 0x11f59, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Kayah_Li = new RangeTable(
                r16: new Range16[] {
            new Range16 (0xa900, 0xa92d, 1),
            new Range16 (0xa92f, 0xa92f, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Kharoshthi = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x10a00, 0x10a03, 1),
            new Range32 (0x10a05, 0x10a06, 1),
            new Range32 (0x10a0c, 0x10a13, 1),
            new Range32 (0x10a15, 0x10a17, 1),
            new Range32 (0x10a19, 0x10a35, 1),
            new Range32 (0x10a38, 0x10a3a, 1),
            new Range32 (0x10a3f, 0x10a48, 1),
            new Range32 (0x10a50, 0x10a58, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Khitan_Small_Script = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x16fe4, 0x16fe4, 1),
            new Range32 (0x18b00, 0x18cd5, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Khmer = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x1780, 0x17dd, 1),
            new Range16 (0x17e0, 0x17e9, 1),
            new Range16 (0x17f0, 0x17f9, 1),
            new Range16 (0x19e0, 0x19ff, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Khojki = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x11200, 0x11211, 1),
            new Range32 (0x11213, 0x11241, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Khudawadi = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x112b0, 0x112ea, 1),
            new Range32 (0x112f0, 0x112f9, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Lao = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0e81, 0x0e82, 1),
            new Range16 (0x0e84, 0x0e84, 1),
            new Range16 (0x0e86, 0x0e8a, 1),
            new Range16 (0x0e8c, 0x0ea3, 1),
            new Range16 (0x0ea5, 0x0ea5, 1),
            new Range16 (0x0ea7, 0x0ebd, 1),
            new Range16 (0x0ec0, 0x0ec4, 1),
            new Range16 (0x0ec6, 0x0ec6, 1),
            new Range16 (0x0ec8, 0x0ece, 1),
            new Range16 (0x0ed0, 0x0ed9, 1),
            new Range16 (0x0edc, 0x0edf, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Latin = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0041, 0x005a, 1),
            new Range16 (0x0061, 0x007a, 1),
            new Range16 (0x00aa, 0x00aa, 1),
            new Range16 (0x00ba, 0x00ba, 1),
            new Range16 (0x00c0, 0x00d6, 1),
            new Range16 (0x00d8, 0x00f6, 1),
            new Range16 (0x00f8, 0x02b8, 1),
            new Range16 (0x02e0, 0x02e4, 1),
            new Range16 (0x1d00, 0x1d25, 1),
            new Range16 (0x1d2c, 0x1d5c, 1),
            new Range16 (0x1d62, 0x1d65, 1),
            new Range16 (0x1d6b, 0x1d77, 1),
            new Range16 (0x1d79, 0x1dbe, 1),
            new Range16 (0x1e00, 0x1eff, 1),
            new Range16 (0x2071, 0x2071, 1),
            new Range16 (0x207f, 0x207f, 1),
            new Range16 (0x2090, 0x209c, 1),
            new Range16 (0x212a, 0x212b, 1),
            new Range16 (0x2132, 0x2132, 1),
            new Range16 (0x214e, 0x214e, 1),
            new Range16 (0x2160, 0x2188, 1),
            new Range16 (0x2c60, 0x2c7f, 1),
            new Range16 (0xa722, 0xa787, 1),
            new Range16 (0xa78b, 0xa7ca, 1),
            new Range16 (0xa7d0, 0xa7d1, 1),
            new Range16 (0xa7d3, 0xa7d3, 1),
            new Range16 (0xa7d5, 0xa7d9, 1),
            new Range16 (0xa7f2, 0xa7ff, 1),
            new Range16 (0xab30, 0xab5a, 1),
            new Range16 (0xab5c, 0xab64, 1),
            new Range16 (0xab66, 0xab69, 1),
            new Range16 (0xfb00, 0xfb06, 1),
            new Range16 (0xff21, 0xff3a, 1),
            new Range16 (0xff41, 0xff5a, 1),
                },
                r32: new Range32[] {
            new Range32 (0x10780, 0x10785, 1),
            new Range32 (0x10787, 0x107b0, 1),
            new Range32 (0x107b2, 0x107ba, 1),
            new Range32 (0x1df00, 0x1df1e, 1),
            new Range32 (0x1df25, 0x1df2a, 1),
                },
                latinOffset: 6
            ); /* RangeTable */

            internal static RangeTable _Lepcha = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x1c00, 0x1c37, 1),
            new Range16 (0x1c3b, 0x1c49, 1),
            new Range16 (0x1c4d, 0x1c4f, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Limbu = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x1900, 0x191e, 1),
            new Range16 (0x1920, 0x192b, 1),
            new Range16 (0x1930, 0x193b, 1),
            new Range16 (0x1940, 0x1940, 1),
            new Range16 (0x1944, 0x194f, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Linear_A = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x10600, 0x10736, 1),
            new Range32 (0x10740, 0x10755, 1),
            new Range32 (0x10760, 0x10767, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Linear_B = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x10000, 0x1000b, 1),
            new Range32 (0x1000d, 0x10026, 1),
            new Range32 (0x10028, 0x1003a, 1),
            new Range32 (0x1003c, 0x1003d, 1),
            new Range32 (0x1003f, 0x1004d, 1),
            new Range32 (0x10050, 0x1005d, 1),
            new Range32 (0x10080, 0x100fa, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Lisu = new RangeTable(
                r16: new Range16[] {
            new Range16 (0xa4d0, 0xa4ff, 1),
                },
                r32: new Range32[] {
            new Range32 (0x11fb0, 0x11fb0, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Lycian = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x10280, 0x1029c, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Lydian = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x10920, 0x10939, 1),
            new Range32 (0x1093f, 0x1093f, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Mahajani = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x11150, 0x11176, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Makasar = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x11ee0, 0x11ef8, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Malayalam = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0d00, 0x0d0c, 1),
            new Range16 (0x0d0e, 0x0d10, 1),
            new Range16 (0x0d12, 0x0d44, 1),
            new Range16 (0x0d46, 0x0d48, 1),
            new Range16 (0x0d4a, 0x0d4f, 1),
            new Range16 (0x0d54, 0x0d63, 1),
            new Range16 (0x0d66, 0x0d7f, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Mandaic = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0840, 0x085b, 1),
            new Range16 (0x085e, 0x085e, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Manichaean = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x10ac0, 0x10ae6, 1),
            new Range32 (0x10aeb, 0x10af6, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Marchen = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x11c70, 0x11c8f, 1),
            new Range32 (0x11c92, 0x11ca7, 1),
            new Range32 (0x11ca9, 0x11cb6, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Masaram_Gondi = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x11d00, 0x11d06, 1),
            new Range32 (0x11d08, 0x11d09, 1),
            new Range32 (0x11d0b, 0x11d36, 1),
            new Range32 (0x11d3a, 0x11d3a, 1),
            new Range32 (0x11d3c, 0x11d3d, 1),
            new Range32 (0x11d3f, 0x11d47, 1),
            new Range32 (0x11d50, 0x11d59, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Medefaidrin = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x16e40, 0x16e9a, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Meetei_Mayek = new RangeTable(
                r16: new Range16[] {
            new Range16 (0xaae0, 0xaaf6, 1),
            new Range16 (0xabc0, 0xabed, 1),
            new Range16 (0xabf0, 0xabf9, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Mende_Kikakui = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x1e800, 0x1e8c4, 1),
            new Range32 (0x1e8c7, 0x1e8d6, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Meroitic_Cursive = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x109a0, 0x109b7, 1),
            new Range32 (0x109bc, 0x109cf, 1),
            new Range32 (0x109d2, 0x109ff, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Meroitic_Hieroglyphs = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x10980, 0x1099f, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Miao = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x16f00, 0x16f4a, 1),
            new Range32 (0x16f4f, 0x16f87, 1),
            new Range32 (0x16f8f, 0x16f9f, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Modi = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x11600, 0x11644, 1),
            new Range32 (0x11650, 0x11659, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Mongolian = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x1800, 0x1801, 1),
            new Range16 (0x1804, 0x1804, 1),
            new Range16 (0x1806, 0x1819, 1),
            new Range16 (0x1820, 0x1878, 1),
            new Range16 (0x1880, 0x18aa, 1),
                },
                r32: new Range32[] {
            new Range32 (0x11660, 0x1166c, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Mro = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x16a40, 0x16a5e, 1),
            new Range32 (0x16a60, 0x16a69, 1),
            new Range32 (0x16a6e, 0x16a6f, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Multani = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x11280, 0x11286, 1),
            new Range32 (0x11288, 0x11288, 1),
            new Range32 (0x1128a, 0x1128d, 1),
            new Range32 (0x1128f, 0x1129d, 1),
            new Range32 (0x1129f, 0x112a9, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Myanmar = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x1000, 0x109f, 1),
            new Range16 (0xa9e0, 0xa9fe, 1),
            new Range16 (0xaa60, 0xaa7f, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Nabataean = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x10880, 0x1089e, 1),
            new Range32 (0x108a7, 0x108af, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Nag_Mundari = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x1e4d0, 0x1e4f9, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Nandinagari = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x119a0, 0x119a7, 1),
            new Range32 (0x119aa, 0x119d7, 1),
            new Range32 (0x119da, 0x119e4, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _New_Tai_Lue = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x1980, 0x19ab, 1),
            new Range16 (0x19b0, 0x19c9, 1),
            new Range16 (0x19d0, 0x19da, 1),
            new Range16 (0x19de, 0x19df, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Newa = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x11400, 0x1145b, 1),
            new Range32 (0x1145d, 0x11461, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Nko = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x07c0, 0x07fa, 1),
            new Range16 (0x07fd, 0x07ff, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Nushu = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x16fe1, 0x16fe1, 1),
            new Range32 (0x1b170, 0x1b2fb, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Nyiakeng_Puachue_Hmong = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x1e100, 0x1e12c, 1),
            new Range32 (0x1e130, 0x1e13d, 1),
            new Range32 (0x1e140, 0x1e149, 1),
            new Range32 (0x1e14e, 0x1e14f, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Ogham = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x1680, 0x169c, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Ol_Chiki = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x1c50, 0x1c7f, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Old_Hungarian = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x10c80, 0x10cb2, 1),
            new Range32 (0x10cc0, 0x10cf2, 1),
            new Range32 (0x10cfa, 0x10cff, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Old_Italic = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x10300, 0x10323, 1),
            new Range32 (0x1032d, 0x1032f, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Old_North_Arabian = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x10a80, 0x10a9f, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Old_Permic = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x10350, 0x1037a, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Old_Persian = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x103a0, 0x103c3, 1),
            new Range32 (0x103c8, 0x103d5, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Old_Sogdian = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x10f00, 0x10f27, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Old_South_Arabian = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x10a60, 0x10a7f, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Old_Turkic = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x10c00, 0x10c48, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Old_Uyghur = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x10f70, 0x10f89, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Oriya = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0b01, 0x0b03, 1),
            new Range16 (0x0b05, 0x0b0c, 1),
            new Range16 (0x0b0f, 0x0b10, 1),
            new Range16 (0x0b13, 0x0b28, 1),
            new Range16 (0x0b2a, 0x0b30, 1),
            new Range16 (0x0b32, 0x0b33, 1),
            new Range16 (0x0b35, 0x0b39, 1),
            new Range16 (0x0b3c, 0x0b44, 1),
            new Range16 (0x0b47, 0x0b48, 1),
            new Range16 (0x0b4b, 0x0b4d, 1),
            new Range16 (0x0b55, 0x0b57, 1),
            new Range16 (0x0b5c, 0x0b5d, 1),
            new Range16 (0x0b5f, 0x0b63, 1),
            new Range16 (0x0b66, 0x0b77, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Osage = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x104b0, 0x104d3, 1),
            new Range32 (0x104d8, 0x104fb, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Osmanya = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x10480, 0x1049d, 1),
            new Range32 (0x104a0, 0x104a9, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Pahawh_Hmong = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x16b00, 0x16b45, 1),
            new Range32 (0x16b50, 0x16b59, 1),
            new Range32 (0x16b5b, 0x16b61, 1),
            new Range32 (0x16b63, 0x16b77, 1),
            new Range32 (0x16b7d, 0x16b8f, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Palmyrene = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x10860, 0x1087f, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Pau_Cin_Hau = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x11ac0, 0x11af8, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Phags_Pa = new RangeTable(
                r16: new Range16[] {
            new Range16 (0xa840, 0xa877, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Phoenician = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x10900, 0x1091b, 1),
            new Range32 (0x1091f, 0x1091f, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Psalter_Pahlavi = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x10b80, 0x10b91, 1),
            new Range32 (0x10b99, 0x10b9c, 1),
            new Range32 (0x10ba9, 0x10baf, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Rejang = new RangeTable(
                r16: new Range16[] {
            new Range16 (0xa930, 0xa953, 1),
            new Range16 (0xa95f, 0xa95f, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Runic = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x16a0, 0x16ea, 1),
            new Range16 (0x16ee, 0x16f8, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Samaritan = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0800, 0x082d, 1),
            new Range16 (0x0830, 0x083e, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Saurashtra = new RangeTable(
                r16: new Range16[] {
            new Range16 (0xa880, 0xa8c5, 1),
            new Range16 (0xa8ce, 0xa8d9, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Sharada = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x11180, 0x111df, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Shavian = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x10450, 0x1047f, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Siddham = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x11580, 0x115b5, 1),
            new Range32 (0x115b8, 0x115dd, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _SignWriting = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x1d800, 0x1da8b, 1),
            new Range32 (0x1da9b, 0x1da9f, 1),
            new Range32 (0x1daa1, 0x1daaf, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Sinhala = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0d81, 0x0d83, 1),
            new Range16 (0x0d85, 0x0d96, 1),
            new Range16 (0x0d9a, 0x0db1, 1),
            new Range16 (0x0db3, 0x0dbb, 1),
            new Range16 (0x0dbd, 0x0dbd, 1),
            new Range16 (0x0dc0, 0x0dc6, 1),
            new Range16 (0x0dca, 0x0dca, 1),
            new Range16 (0x0dcf, 0x0dd4, 1),
            new Range16 (0x0dd6, 0x0dd6, 1),
            new Range16 (0x0dd8, 0x0ddf, 1),
            new Range16 (0x0de6, 0x0def, 1),
            new Range16 (0x0df2, 0x0df4, 1),
                },
                r32: new Range32[] {
            new Range32 (0x111e1, 0x111f4, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Sogdian = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x10f30, 0x10f59, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Sora_Sompeng = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x110d0, 0x110e8, 1),
            new Range32 (0x110f0, 0x110f9, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Soyombo = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x11a50, 0x11aa2, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Sundanese = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x1b80, 0x1bbf, 1),
            new Range16 (0x1cc0, 0x1cc7, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Syloti_Nagri = new RangeTable(
                r16: new Range16[] {
            new Range16 (0xa800, 0xa82c, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Syriac = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0700, 0x070d, 1),
            new Range16 (0x070f, 0x074a, 1),
            new Range16 (0x074d, 0x074f, 1),
            new Range16 (0x0860, 0x086a, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Tagalog = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x1700, 0x1715, 1),
            new Range16 (0x171f, 0x171f, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Tagbanwa = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x1760, 0x176c, 1),
            new Range16 (0x176e, 0x1770, 1),
            new Range16 (0x1772, 0x1773, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Tai_Le = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x1950, 0x196d, 1),
            new Range16 (0x1970, 0x1974, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Tai_Tham = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x1a20, 0x1a5e, 1),
            new Range16 (0x1a60, 0x1a7c, 1),
            new Range16 (0x1a7f, 0x1a89, 1),
            new Range16 (0x1a90, 0x1a99, 1),
            new Range16 (0x1aa0, 0x1aad, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Tai_Viet = new RangeTable(
                r16: new Range16[] {
            new Range16 (0xaa80, 0xaac2, 1),
            new Range16 (0xaadb, 0xaadf, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Takri = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x11680, 0x116b9, 1),
            new Range32 (0x116c0, 0x116c9, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Tamil = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0b82, 0x0b83, 1),
            new Range16 (0x0b85, 0x0b8a, 1),
            new Range16 (0x0b8e, 0x0b90, 1),
            new Range16 (0x0b92, 0x0b95, 1),
            new Range16 (0x0b99, 0x0b9a, 1),
            new Range16 (0x0b9c, 0x0b9c, 1),
            new Range16 (0x0b9e, 0x0b9f, 1),
            new Range16 (0x0ba3, 0x0ba4, 1),
            new Range16 (0x0ba8, 0x0baa, 1),
            new Range16 (0x0bae, 0x0bb9, 1),
            new Range16 (0x0bbe, 0x0bc2, 1),
            new Range16 (0x0bc6, 0x0bc8, 1),
            new Range16 (0x0bca, 0x0bcd, 1),
            new Range16 (0x0bd0, 0x0bd0, 1),
            new Range16 (0x0bd7, 0x0bd7, 1),
            new Range16 (0x0be6, 0x0bfa, 1),
                },
                r32: new Range32[] {
            new Range32 (0x11fc0, 0x11ff1, 1),
            new Range32 (0x11fff, 0x11fff, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Tangsa = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x16a70, 0x16abe, 1),
            new Range32 (0x16ac0, 0x16ac9, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Tangut = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x16fe0, 0x16fe0, 1),
            new Range32 (0x17000, 0x187f7, 1),
            new Range32 (0x18800, 0x18aff, 1),
            new Range32 (0x18d00, 0x18d08, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Telugu = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0c00, 0x0c0c, 1),
            new Range16 (0x0c0e, 0x0c10, 1),
            new Range16 (0x0c12, 0x0c28, 1),
            new Range16 (0x0c2a, 0x0c39, 1),
            new Range16 (0x0c3c, 0x0c44, 1),
            new Range16 (0x0c46, 0x0c48, 1),
            new Range16 (0x0c4a, 0x0c4d, 1),
            new Range16 (0x0c55, 0x0c56, 1),
            new Range16 (0x0c58, 0x0c5a, 1),
            new Range16 (0x0c5d, 0x0c5d, 1),
            new Range16 (0x0c60, 0x0c63, 1),
            new Range16 (0x0c66, 0x0c6f, 1),
            new Range16 (0x0c77, 0x0c7f, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Thaana = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0780, 0x07b1, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Thai = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0e01, 0x0e3a, 1),
            new Range16 (0x0e40, 0x0e5b, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Tibetan = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0f00, 0x0f47, 1),
            new Range16 (0x0f49, 0x0f6c, 1),
            new Range16 (0x0f71, 0x0f97, 1),
            new Range16 (0x0f99, 0x0fbc, 1),
            new Range16 (0x0fbe, 0x0fcc, 1),
            new Range16 (0x0fce, 0x0fd4, 1),
            new Range16 (0x0fd9, 0x0fda, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Tifinagh = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x2d30, 0x2d67, 1),
            new Range16 (0x2d6f, 0x2d70, 1),
            new Range16 (0x2d7f, 0x2d7f, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Tirhuta = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x11480, 0x114c7, 1),
            new Range32 (0x114d0, 0x114d9, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Toto = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x1e290, 0x1e2ae, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Ugaritic = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x10380, 0x1039d, 1),
            new Range32 (0x1039f, 0x1039f, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Vai = new RangeTable(
                r16: new Range16[] {
            new Range16 (0xa500, 0xa62b, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Vithkuqi = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x10570, 0x1057a, 1),
            new Range32 (0x1057c, 0x1058a, 1),
            new Range32 (0x1058c, 0x10592, 1),
            new Range32 (0x10594, 0x10595, 1),
            new Range32 (0x10597, 0x105a1, 1),
            new Range32 (0x105a3, 0x105b1, 1),
            new Range32 (0x105b3, 0x105b9, 1),
            new Range32 (0x105bb, 0x105bc, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Wancho = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x1e2c0, 0x1e2f9, 1),
            new Range32 (0x1e2ff, 0x1e2ff, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Warang_Citi = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x118a0, 0x118f2, 1),
            new Range32 (0x118ff, 0x118ff, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Yezidi = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x10e80, 0x10ea9, 1),
            new Range32 (0x10eab, 0x10ead, 1),
            new Range32 (0x10eb0, 0x10eb1, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Yi = new RangeTable(
                r16: new Range16[] {
            new Range16 (0xa000, 0xa48c, 1),
            new Range16 (0xa490, 0xa4c6, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Zanabazar_Square = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x11a00, 0x11a47, 1),
                }
            ); /* RangeTable */

            /// <summary>Adlam is the set of Unicode characters in script Adlam.</summary>
            public static RangeTable Adlam => _Adlam;
            /// <summary>Ahom is the set of Unicode characters in script Ahom.</summary>
            public static RangeTable Ahom => _Ahom;
            /// <summary>Anatolian_Hieroglyphs is the set of Unicode characters in script Anatolian_Hieroglyphs.</summary>
            public static RangeTable Anatolian_Hieroglyphs => _Anatolian_Hieroglyphs;
            /// <summary>Arabic is the set of Unicode characters in script Arabic.</summary>
            public static RangeTable Arabic => _Arabic;
            /// <summary>Armenian is the set of Unicode characters in script Armenian.</summary>
            public static RangeTable Armenian => _Armenian;
            /// <summary>Avestan is the set of Unicode characters in script Avestan.</summary>
            public static RangeTable Avestan => _Avestan;
            /// <summary>Balinese is the set of Unicode characters in script Balinese.</summary>
            public static RangeTable Balinese => _Balinese;
            /// <summary>Bamum is the set of Unicode characters in script Bamum.</summary>
            public static RangeTable Bamum => _Bamum;
            /// <summary>Bassa_Vah is the set of Unicode characters in script Bassa_Vah.</summary>
            public static RangeTable Bassa_Vah => _Bassa_Vah;
            /// <summary>Batak is the set of Unicode characters in script Batak.</summary>
            public static RangeTable Batak => _Batak;
            /// <summary>Bengali is the set of Unicode characters in script Bengali.</summary>
            public static RangeTable Bengali => _Bengali;
            /// <summary>Bhaiksuki is the set of Unicode characters in script Bhaiksuki.</summary>
            public static RangeTable Bhaiksuki => _Bhaiksuki;
            /// <summary>Bopomofo is the set of Unicode characters in script Bopomofo.</summary>
            public static RangeTable Bopomofo => _Bopomofo;
            /// <summary>Brahmi is the set of Unicode characters in script Brahmi.</summary>
            public static RangeTable Brahmi => _Brahmi;
            /// <summary>Braille is the set of Unicode characters in script Braille.</summary>
            public static RangeTable Braille => _Braille;
            /// <summary>Buginese is the set of Unicode characters in script Buginese.</summary>
            public static RangeTable Buginese => _Buginese;
            /// <summary>Buhid is the set of Unicode characters in script Buhid.</summary>
            public static RangeTable Buhid => _Buhid;
            /// <summary>Canadian_Aboriginal is the set of Unicode characters in script Canadian_Aboriginal.</summary>
            public static RangeTable Canadian_Aboriginal => _Canadian_Aboriginal;
            /// <summary>Carian is the set of Unicode characters in script Carian.</summary>
            public static RangeTable Carian => _Carian;
            /// <summary>Caucasian_Albanian is the set of Unicode characters in script Caucasian_Albanian.</summary>
            public static RangeTable Caucasian_Albanian => _Caucasian_Albanian;
            /// <summary>Chakma is the set of Unicode characters in script Chakma.</summary>
            public static RangeTable Chakma => _Chakma;
            /// <summary>Cham is the set of Unicode characters in script Cham.</summary>
            public static RangeTable Cham => _Cham;
            /// <summary>Cherokee is the set of Unicode characters in script Cherokee.</summary>
            public static RangeTable Cherokee => _Cherokee;
            /// <summary>Chorasmian is the set of Unicode characters in script Chorasmian.</summary>
            public static RangeTable Chorasmian => _Chorasmian;
            /// <summary>Common is the set of Unicode characters in script Common.</summary>
            public static RangeTable Common => _Common;
            /// <summary>Coptic is the set of Unicode characters in script Coptic.</summary>
            public static RangeTable Coptic => _Coptic;
            /// <summary>Cuneiform is the set of Unicode characters in script Cuneiform.</summary>
            public static RangeTable Cuneiform => _Cuneiform;
            /// <summary>Cypriot is the set of Unicode characters in script Cypriot.</summary>
            public static RangeTable Cypriot => _Cypriot;
            /// <summary>Cypro_Minoan is the set of Unicode characters in script Cypro_Minoan.</summary>
            public static RangeTable Cypro_Minoan => _Cypro_Minoan;
            /// <summary>Cyrillic is the set of Unicode characters in script Cyrillic.</summary>
            public static RangeTable Cyrillic => _Cyrillic;
            /// <summary>Deseret is the set of Unicode characters in script Deseret.</summary>
            public static RangeTable Deseret => _Deseret;
            /// <summary>Devanagari is the set of Unicode characters in script Devanagari.</summary>
            public static RangeTable Devanagari => _Devanagari;
            /// <summary>Dives_Akuru is the set of Unicode characters in script Dives_Akuru.</summary>
            public static RangeTable Dives_Akuru => _Dives_Akuru;
            /// <summary>Dogra is the set of Unicode characters in script Dogra.</summary>
            public static RangeTable Dogra => _Dogra;
            /// <summary>Duployan is the set of Unicode characters in script Duployan.</summary>
            public static RangeTable Duployan => _Duployan;
            /// <summary>Egyptian_Hieroglyphs is the set of Unicode characters in script Egyptian_Hieroglyphs.</summary>
            public static RangeTable Egyptian_Hieroglyphs => _Egyptian_Hieroglyphs;
            /// <summary>Elbasan is the set of Unicode characters in script Elbasan.</summary>
            public static RangeTable Elbasan => _Elbasan;
            /// <summary>Elymaic is the set of Unicode characters in script Elymaic.</summary>
            public static RangeTable Elymaic => _Elymaic;
            /// <summary>Ethiopic is the set of Unicode characters in script Ethiopic.</summary>
            public static RangeTable Ethiopic => _Ethiopic;
            /// <summary>Georgian is the set of Unicode characters in script Georgian.</summary>
            public static RangeTable Georgian => _Georgian;
            /// <summary>Glagolitic is the set of Unicode characters in script Glagolitic.</summary>
            public static RangeTable Glagolitic => _Glagolitic;
            /// <summary>Gothic is the set of Unicode characters in script Gothic.</summary>
            public static RangeTable Gothic => _Gothic;
            /// <summary>Grantha is the set of Unicode characters in script Grantha.</summary>
            public static RangeTable Grantha => _Grantha;
            /// <summary>Greek is the set of Unicode characters in script Greek.</summary>
            public static RangeTable Greek => _Greek;
            /// <summary>Gujarati is the set of Unicode characters in script Gujarati.</summary>
            public static RangeTable Gujarati => _Gujarati;
            /// <summary>Gunjala_Gondi is the set of Unicode characters in script Gunjala_Gondi.</summary>
            public static RangeTable Gunjala_Gondi => _Gunjala_Gondi;
            /// <summary>Gurmukhi is the set of Unicode characters in script Gurmukhi.</summary>
            public static RangeTable Gurmukhi => _Gurmukhi;
            /// <summary>Han is the set of Unicode characters in script Han.</summary>
            public static RangeTable Han => _Han;
            /// <summary>Hangul is the set of Unicode characters in script Hangul.</summary>
            public static RangeTable Hangul => _Hangul;
            /// <summary>Hanifi_Rohingya is the set of Unicode characters in script Hanifi_Rohingya.</summary>
            public static RangeTable Hanifi_Rohingya => _Hanifi_Rohingya;
            /// <summary>Hanunoo is the set of Unicode characters in script Hanunoo.</summary>
            public static RangeTable Hanunoo => _Hanunoo;
            /// <summary>Hatran is the set of Unicode characters in script Hatran.</summary>
            public static RangeTable Hatran => _Hatran;
            /// <summary>Hebrew is the set of Unicode characters in script Hebrew.</summary>
            public static RangeTable Hebrew => _Hebrew;
            /// <summary>Hiragana is the set of Unicode characters in script Hiragana.</summary>
            public static RangeTable Hiragana => _Hiragana;
            /// <summary>Imperial_Aramaic is the set of Unicode characters in script Imperial_Aramaic.</summary>
            public static RangeTable Imperial_Aramaic => _Imperial_Aramaic;
            /// <summary>Inherited is the set of Unicode characters in script Inherited.</summary>
            public static RangeTable Inherited => _Inherited;
            /// <summary>Inscriptional_Pahlavi is the set of Unicode characters in script Inscriptional_Pahlavi.</summary>
            public static RangeTable Inscriptional_Pahlavi => _Inscriptional_Pahlavi;
            /// <summary>Inscriptional_Parthian is the set of Unicode characters in script Inscriptional_Parthian.</summary>
            public static RangeTable Inscriptional_Parthian => _Inscriptional_Parthian;
            /// <summary>Javanese is the set of Unicode characters in script Javanese.</summary>
            public static RangeTable Javanese => _Javanese;
            /// <summary>Kaithi is the set of Unicode characters in script Kaithi.</summary>
            public static RangeTable Kaithi => _Kaithi;
            /// <summary>Kannada is the set of Unicode characters in script Kannada.</summary>
            public static RangeTable Kannada => _Kannada;
            /// <summary>Katakana is the set of Unicode characters in script Katakana.</summary>
            public static RangeTable Katakana => _Katakana;
            /// <summary>Kawi is the set of Unicode characters in script Kawi.</summary>
            public static RangeTable Kawi => _Kawi;
            /// <summary>Kayah_Li is the set of Unicode characters in script Kayah_Li.</summary>
            public static RangeTable Kayah_Li => _Kayah_Li;
            /// <summary>Kharoshthi is the set of Unicode characters in script Kharoshthi.</summary>
            public static RangeTable Kharoshthi => _Kharoshthi;
            /// <summary>Khitan_Small_Script is the set of Unicode characters in script Khitan_Small_Script.</summary>
            public static RangeTable Khitan_Small_Script => _Khitan_Small_Script;
            /// <summary>Khmer is the set of Unicode characters in script Khmer.</summary>
            public static RangeTable Khmer => _Khmer;
            /// <summary>Khojki is the set of Unicode characters in script Khojki.</summary>
            public static RangeTable Khojki => _Khojki;
            /// <summary>Khudawadi is the set of Unicode characters in script Khudawadi.</summary>
            public static RangeTable Khudawadi => _Khudawadi;
            /// <summary>Lao is the set of Unicode characters in script Lao.</summary>
            public static RangeTable Lao => _Lao;
            /// <summary>Latin is the set of Unicode characters in script Latin.</summary>
            public static RangeTable Latin => _Latin;
            /// <summary>Lepcha is the set of Unicode characters in script Lepcha.</summary>
            public static RangeTable Lepcha => _Lepcha;
            /// <summary>Limbu is the set of Unicode characters in script Limbu.</summary>
            public static RangeTable Limbu => _Limbu;
            /// <summary>Linear_A is the set of Unicode characters in script Linear_A.</summary>
            public static RangeTable Linear_A => _Linear_A;
            /// <summary>Linear_B is the set of Unicode characters in script Linear_B.</summary>
            public static RangeTable Linear_B => _Linear_B;
            /// <summary>Lisu is the set of Unicode characters in script Lisu.</summary>
            public static RangeTable Lisu => _Lisu;
            /// <summary>Lycian is the set of Unicode characters in script Lycian.</summary>
            public static RangeTable Lycian => _Lycian;
            /// <summary>Lydian is the set of Unicode characters in script Lydian.</summary>
            public static RangeTable Lydian => _Lydian;
            /// <summary>Mahajani is the set of Unicode characters in script Mahajani.</summary>
            public static RangeTable Mahajani => _Mahajani;
            /// <summary>Makasar is the set of Unicode characters in script Makasar.</summary>
            public static RangeTable Makasar => _Makasar;
            /// <summary>Malayalam is the set of Unicode characters in script Malayalam.</summary>
            public static RangeTable Malayalam => _Malayalam;
            /// <summary>Mandaic is the set of Unicode characters in script Mandaic.</summary>
            public static RangeTable Mandaic => _Mandaic;
            /// <summary>Manichaean is the set of Unicode characters in script Manichaean.</summary>
            public static RangeTable Manichaean => _Manichaean;
            /// <summary>Marchen is the set of Unicode characters in script Marchen.</summary>
            public static RangeTable Marchen => _Marchen;
            /// <summary>Masaram_Gondi is the set of Unicode characters in script Masaram_Gondi.</summary>
            public static RangeTable Masaram_Gondi => _Masaram_Gondi;
            /// <summary>Medefaidrin is the set of Unicode characters in script Medefaidrin.</summary>
            public static RangeTable Medefaidrin => _Medefaidrin;
            /// <summary>Meetei_Mayek is the set of Unicode characters in script Meetei_Mayek.</summary>
            public static RangeTable Meetei_Mayek => _Meetei_Mayek;
            /// <summary>Mende_Kikakui is the set of Unicode characters in script Mende_Kikakui.</summary>
            public static RangeTable Mende_Kikakui => _Mende_Kikakui;
            /// <summary>Meroitic_Cursive is the set of Unicode characters in script Meroitic_Cursive.</summary>
            public static RangeTable Meroitic_Cursive => _Meroitic_Cursive;
            /// <summary>Meroitic_Hieroglyphs is the set of Unicode characters in script Meroitic_Hieroglyphs.</summary>
            public static RangeTable Meroitic_Hieroglyphs => _Meroitic_Hieroglyphs;
            /// <summary>Miao is the set of Unicode characters in script Miao.</summary>
            public static RangeTable Miao => _Miao;
            /// <summary>Modi is the set of Unicode characters in script Modi.</summary>
            public static RangeTable Modi => _Modi;
            /// <summary>Mongolian is the set of Unicode characters in script Mongolian.</summary>
            public static RangeTable Mongolian => _Mongolian;
            /// <summary>Mro is the set of Unicode characters in script Mro.</summary>
            public static RangeTable Mro => _Mro;
            /// <summary>Multani is the set of Unicode characters in script Multani.</summary>
            public static RangeTable Multani => _Multani;
            /// <summary>Myanmar is the set of Unicode characters in script Myanmar.</summary>
            public static RangeTable Myanmar => _Myanmar;
            /// <summary>Nabataean is the set of Unicode characters in script Nabataean.</summary>
            public static RangeTable Nabataean => _Nabataean;
            /// <summary>Nag_Mundari is the set of Unicode characters in script Nag_Mundari.</summary>
            public static RangeTable Nag_Mundari => _Nag_Mundari;
            /// <summary>Nandinagari is the set of Unicode characters in script Nandinagari.</summary>
            public static RangeTable Nandinagari => _Nandinagari;
            /// <summary>New_Tai_Lue is the set of Unicode characters in script New_Tai_Lue.</summary>
            public static RangeTable New_Tai_Lue => _New_Tai_Lue;
            /// <summary>Newa is the set of Unicode characters in script Newa.</summary>
            public static RangeTable Newa => _Newa;
            /// <summary>Nko is the set of Unicode characters in script Nko.</summary>
            public static RangeTable Nko => _Nko;
            /// <summary>Nushu is the set of Unicode characters in script Nushu.</summary>
            public static RangeTable Nushu => _Nushu;
            /// <summary>Nyiakeng_Puachue_Hmong is the set of Unicode characters in script Nyiakeng_Puachue_Hmong.</summary>
            public static RangeTable Nyiakeng_Puachue_Hmong => _Nyiakeng_Puachue_Hmong;
            /// <summary>Ogham is the set of Unicode characters in script Ogham.</summary>
            public static RangeTable Ogham => _Ogham;
            /// <summary>Ol_Chiki is the set of Unicode characters in script Ol_Chiki.</summary>
            public static RangeTable Ol_Chiki => _Ol_Chiki;
            /// <summary>Old_Hungarian is the set of Unicode characters in script Old_Hungarian.</summary>
            public static RangeTable Old_Hungarian => _Old_Hungarian;
            /// <summary>Old_Italic is the set of Unicode characters in script Old_Italic.</summary>
            public static RangeTable Old_Italic => _Old_Italic;
            /// <summary>Old_North_Arabian is the set of Unicode characters in script Old_North_Arabian.</summary>
            public static RangeTable Old_North_Arabian => _Old_North_Arabian;
            /// <summary>Old_Permic is the set of Unicode characters in script Old_Permic.</summary>
            public static RangeTable Old_Permic => _Old_Permic;
            /// <summary>Old_Persian is the set of Unicode characters in script Old_Persian.</summary>
            public static RangeTable Old_Persian => _Old_Persian;
            /// <summary>Old_Sogdian is the set of Unicode characters in script Old_Sogdian.</summary>
            public static RangeTable Old_Sogdian => _Old_Sogdian;
            /// <summary>Old_South_Arabian is the set of Unicode characters in script Old_South_Arabian.</summary>
            public static RangeTable Old_South_Arabian => _Old_South_Arabian;
            /// <summary>Old_Turkic is the set of Unicode characters in script Old_Turkic.</summary>
            public static RangeTable Old_Turkic => _Old_Turkic;
            /// <summary>Old_Uyghur is the set of Unicode characters in script Old_Uyghur.</summary>
            public static RangeTable Old_Uyghur => _Old_Uyghur;
            /// <summary>Oriya is the set of Unicode characters in script Oriya.</summary>
            public static RangeTable Oriya => _Oriya;
            /// <summary>Osage is the set of Unicode characters in script Osage.</summary>
            public static RangeTable Osage => _Osage;
            /// <summary>Osmanya is the set of Unicode characters in script Osmanya.</summary>
            public static RangeTable Osmanya => _Osmanya;
            /// <summary>Pahawh_Hmong is the set of Unicode characters in script Pahawh_Hmong.</summary>
            public static RangeTable Pahawh_Hmong => _Pahawh_Hmong;
            /// <summary>Palmyrene is the set of Unicode characters in script Palmyrene.</summary>
            public static RangeTable Palmyrene => _Palmyrene;
            /// <summary>Pau_Cin_Hau is the set of Unicode characters in script Pau_Cin_Hau.</summary>
            public static RangeTable Pau_Cin_Hau => _Pau_Cin_Hau;
            /// <summary>Phags_Pa is the set of Unicode characters in script Phags_Pa.</summary>
            public static RangeTable Phags_Pa => _Phags_Pa;
            /// <summary>Phoenician is the set of Unicode characters in script Phoenician.</summary>
            public static RangeTable Phoenician => _Phoenician;
            /// <summary>Psalter_Pahlavi is the set of Unicode characters in script Psalter_Pahlavi.</summary>
            public static RangeTable Psalter_Pahlavi => _Psalter_Pahlavi;
            /// <summary>Rejang is the set of Unicode characters in script Rejang.</summary>
            public static RangeTable Rejang => _Rejang;
            /// <summary>Runic is the set of Unicode characters in script Runic.</summary>
            public static RangeTable Runic => _Runic;
            /// <summary>Samaritan is the set of Unicode characters in script Samaritan.</summary>
            public static RangeTable Samaritan => _Samaritan;
            /// <summary>Saurashtra is the set of Unicode characters in script Saurashtra.</summary>
            public static RangeTable Saurashtra => _Saurashtra;
            /// <summary>Sharada is the set of Unicode characters in script Sharada.</summary>
            public static RangeTable Sharada => _Sharada;
            /// <summary>Shavian is the set of Unicode characters in script Shavian.</summary>
            public static RangeTable Shavian => _Shavian;
            /// <summary>Siddham is the set of Unicode characters in script Siddham.</summary>
            public static RangeTable Siddham => _Siddham;
            /// <summary>SignWriting is the set of Unicode characters in script SignWriting.</summary>
            public static RangeTable SignWriting => _SignWriting;
            /// <summary>Sinhala is the set of Unicode characters in script Sinhala.</summary>
            public static RangeTable Sinhala => _Sinhala;
            /// <summary>Sogdian is the set of Unicode characters in script Sogdian.</summary>
            public static RangeTable Sogdian => _Sogdian;
            /// <summary>Sora_Sompeng is the set of Unicode characters in script Sora_Sompeng.</summary>
            public static RangeTable Sora_Sompeng => _Sora_Sompeng;
            /// <summary>Soyombo is the set of Unicode characters in script Soyombo.</summary>
            public static RangeTable Soyombo => _Soyombo;
            /// <summary>Sundanese is the set of Unicode characters in script Sundanese.</summary>
            public static RangeTable Sundanese => _Sundanese;
            /// <summary>Syloti_Nagri is the set of Unicode characters in script Syloti_Nagri.</summary>
            public static RangeTable Syloti_Nagri => _Syloti_Nagri;
            /// <summary>Syriac is the set of Unicode characters in script Syriac.</summary>
            public static RangeTable Syriac => _Syriac;
            /// <summary>Tagalog is the set of Unicode characters in script Tagalog.</summary>
            public static RangeTable Tagalog => _Tagalog;
            /// <summary>Tagbanwa is the set of Unicode characters in script Tagbanwa.</summary>
            public static RangeTable Tagbanwa => _Tagbanwa;
            /// <summary>Tai_Le is the set of Unicode characters in script Tai_Le.</summary>
            public static RangeTable Tai_Le => _Tai_Le;
            /// <summary>Tai_Tham is the set of Unicode characters in script Tai_Tham.</summary>
            public static RangeTable Tai_Tham => _Tai_Tham;
            /// <summary>Tai_Viet is the set of Unicode characters in script Tai_Viet.</summary>
            public static RangeTable Tai_Viet => _Tai_Viet;
            /// <summary>Takri is the set of Unicode characters in script Takri.</summary>
            public static RangeTable Takri => _Takri;
            /// <summary>Tamil is the set of Unicode characters in script Tamil.</summary>
            public static RangeTable Tamil => _Tamil;
            /// <summary>Tangsa is the set of Unicode characters in script Tangsa.</summary>
            public static RangeTable Tangsa => _Tangsa;
            /// <summary>Tangut is the set of Unicode characters in script Tangut.</summary>
            public static RangeTable Tangut => _Tangut;
            /// <summary>Telugu is the set of Unicode characters in script Telugu.</summary>
            public static RangeTable Telugu => _Telugu;
            /// <summary>Thaana is the set of Unicode characters in script Thaana.</summary>
            public static RangeTable Thaana => _Thaana;
            /// <summary>Thai is the set of Unicode characters in script Thai.</summary>
            public static RangeTable Thai => _Thai;
            /// <summary>Tibetan is the set of Unicode characters in script Tibetan.</summary>
            public static RangeTable Tibetan => _Tibetan;
            /// <summary>Tifinagh is the set of Unicode characters in script Tifinagh.</summary>
            public static RangeTable Tifinagh => _Tifinagh;
            /// <summary>Tirhuta is the set of Unicode characters in script Tirhuta.</summary>
            public static RangeTable Tirhuta => _Tirhuta;
            /// <summary>Toto is the set of Unicode characters in script Toto.</summary>
            public static RangeTable Toto => _Toto;
            /// <summary>Ugaritic is the set of Unicode characters in script Ugaritic.</summary>
            public static RangeTable Ugaritic => _Ugaritic;
            /// <summary>Vai is the set of Unicode characters in script Vai.</summary>
            public static RangeTable Vai => _Vai;
            /// <summary>Vithkuqi is the set of Unicode characters in script Vithkuqi.</summary>
            public static RangeTable Vithkuqi => _Vithkuqi;
            /// <summary>Wancho is the set of Unicode characters in script Wancho.</summary>
            public static RangeTable Wancho => _Wancho;
            /// <summary>Warang_Citi is the set of Unicode characters in script Warang_Citi.</summary>
            public static RangeTable Warang_Citi => _Warang_Citi;
            /// <summary>Yezidi is the set of Unicode characters in script Yezidi.</summary>
            public static RangeTable Yezidi => _Yezidi;
            /// <summary>Yi is the set of Unicode characters in script Yi.</summary>
            public static RangeTable Yi => _Yi;
            /// <summary>Zanabazar_Square is the set of Unicode characters in script Zanabazar_Square.</summary>
            public static RangeTable Zanabazar_Square => _Zanabazar_Square;
        }

        // Generated by running
        //	maketables --props=all --url=https://www.unicode.org/Public/15.0.0/ucd/
        // DO NOT EDIT

        /// <summary>Static class containing the proeprty-based tables.</summary>
        /// <remarks><para>There are static properties that can be used to fetch RangeTables that identify characters that have a specific property, or you can use the <see cref="T:NStack.Unicode.Property.Get"/> method in this class to retrieve the range table by the property name</para></remarks>
        public static class Property
        {
            /// <summary>Retrieves the specified RangeTable having that property.</summary>
            /// <param name="propertyName">The property name.</param>
            public static RangeTable Get(string propertyName) => Properties[propertyName];
            // Properties is the set of Unicode property tables.
            static Dictionary<string, RangeTable> Properties = new Dictionary<string, RangeTable>(){
            { "ASCII_Hex_Digit", ASCII_Hex_Digit },
            { "Bidi_Control", Bidi_Control },
            { "Dash", Dash },
            { "Deprecated", Deprecated },
            { "Diacritic", Diacritic },
            { "Extender", Extender },
            { "Hex_Digit", Hex_Digit },
            { "Hyphen", Hyphen },
            { "IDS_Binary_Operator", IDS_Binary_Operator },
            { "IDS_Trinary_Operator", IDS_Trinary_Operator },
            { "Ideographic", Ideographic },
            { "Join_Control", Join_Control },
            { "Logical_Order_Exception", Logical_Order_Exception },
            { "Noncharacter_Code_Point", Noncharacter_Code_Point },
            { "Other_Alphabetic", Other_Alphabetic },
            { "Other_Default_Ignorable_Code_Point", Other_Default_Ignorable_Code_Point },
            { "Other_Grapheme_Extend", Other_Grapheme_Extend },
            { "Other_ID_Continue", Other_ID_Continue },
            { "Other_ID_Start", Other_ID_Start },
            { "Other_Lowercase", Other_Lowercase },
            { "Other_Math", Other_Math },
            { "Other_Uppercase", Other_Uppercase },
            { "Pattern_Syntax", Pattern_Syntax },
            { "Pattern_White_Space", Pattern_White_Space },
            { "Prepended_Concatenation_Mark", Prepended_Concatenation_Mark },
            { "Quotation_Mark", Quotation_Mark },
            { "Radical", Radical },
            { "Regional_Indicator", Regional_Indicator },
            { "Sentence_Terminal", Sentence_Terminal },
            { "STerm", Sentence_Terminal },
            { "Soft_Dotted", Soft_Dotted },
            { "Terminal_Punctuation", Terminal_Punctuation },
            { "Unified_Ideograph", Unified_Ideograph },
            { "Variation_Selector", Variation_Selector },
            { "White_Space", White_Space },
        };

            internal static RangeTable _ASCII_Hex_Digit = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0030, 0x0039, 1),
            new Range16 (0x0041, 0x0046, 1),
            new Range16 (0x0061, 0x0066, 1),
                },
                latinOffset: 3
            ); /* RangeTable */

            internal static RangeTable _Bidi_Control = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x061c, 0x061c, 1),
            new Range16 (0x200e, 0x200f, 1),
            new Range16 (0x202a, 0x202e, 1),
            new Range16 (0x2066, 0x2069, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Dash = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x002d, 0x002d, 1),
            new Range16 (0x058a, 0x058a, 1),
            new Range16 (0x05be, 0x05be, 1),
            new Range16 (0x1400, 0x1400, 1),
            new Range16 (0x1806, 0x1806, 1),
            new Range16 (0x2010, 0x2015, 1),
            new Range16 (0x2053, 0x2053, 1),
            new Range16 (0x207b, 0x207b, 1),
            new Range16 (0x208b, 0x208b, 1),
            new Range16 (0x2212, 0x2212, 1),
            new Range16 (0x2e17, 0x2e17, 1),
            new Range16 (0x2e1a, 0x2e1a, 1),
            new Range16 (0x2e3a, 0x2e3b, 1),
            new Range16 (0x2e40, 0x2e40, 1),
            new Range16 (0x2e5d, 0x2e5d, 1),
            new Range16 (0x301c, 0x301c, 1),
            new Range16 (0x3030, 0x3030, 1),
            new Range16 (0x30a0, 0x30a0, 1),
            new Range16 (0xfe31, 0xfe32, 1),
            new Range16 (0xfe58, 0xfe58, 1),
            new Range16 (0xfe63, 0xfe63, 1),
            new Range16 (0xff0d, 0xff0d, 1),
                },
                r32: new Range32[] {
            new Range32 (0x10ead, 0x10ead, 1),
                },
                latinOffset: 1
            ); /* RangeTable */

            internal static RangeTable _Deprecated = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0149, 0x0149, 1),
            new Range16 (0x0673, 0x0673, 1),
            new Range16 (0x0f77, 0x0f77, 1),
            new Range16 (0x0f79, 0x0f79, 1),
            new Range16 (0x17a3, 0x17a4, 1),
            new Range16 (0x206a, 0x206f, 1),
            new Range16 (0x2329, 0x232a, 1),
                },
                r32: new Range32[] {
            new Range32 (0xe0001, 0xe0001, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Diacritic = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x005e, 0x005e, 1),
            new Range16 (0x0060, 0x0060, 1),
            new Range16 (0x00a8, 0x00a8, 1),
            new Range16 (0x00af, 0x00af, 1),
            new Range16 (0x00b4, 0x00b4, 1),
            new Range16 (0x00b7, 0x00b8, 1),
            new Range16 (0x02b0, 0x034e, 1),
            new Range16 (0x0350, 0x0357, 1),
            new Range16 (0x035d, 0x0362, 1),
            new Range16 (0x0374, 0x0375, 1),
            new Range16 (0x037a, 0x037a, 1),
            new Range16 (0x0384, 0x0385, 1),
            new Range16 (0x0483, 0x0487, 1),
            new Range16 (0x0559, 0x0559, 1),
            new Range16 (0x0591, 0x05a1, 1),
            new Range16 (0x05a3, 0x05bd, 1),
            new Range16 (0x05bf, 0x05bf, 1),
            new Range16 (0x05c1, 0x05c2, 1),
            new Range16 (0x05c4, 0x05c4, 1),
            new Range16 (0x064b, 0x0652, 1),
            new Range16 (0x0657, 0x0658, 1),
            new Range16 (0x06df, 0x06e0, 1),
            new Range16 (0x06e5, 0x06e6, 1),
            new Range16 (0x06ea, 0x06ec, 1),
            new Range16 (0x0730, 0x074a, 1),
            new Range16 (0x07a6, 0x07b0, 1),
            new Range16 (0x07eb, 0x07f5, 1),
            new Range16 (0x0818, 0x0819, 1),
            new Range16 (0x0898, 0x089f, 1),
            new Range16 (0x08c9, 0x08d2, 1),
            new Range16 (0x08e3, 0x08fe, 1),
            new Range16 (0x093c, 0x093c, 1),
            new Range16 (0x094d, 0x094d, 1),
            new Range16 (0x0951, 0x0954, 1),
            new Range16 (0x0971, 0x0971, 1),
            new Range16 (0x09bc, 0x09bc, 1),
            new Range16 (0x09cd, 0x09cd, 1),
            new Range16 (0x0a3c, 0x0a3c, 1),
            new Range16 (0x0a4d, 0x0a4d, 1),
            new Range16 (0x0abc, 0x0abc, 1),
            new Range16 (0x0acd, 0x0acd, 1),
            new Range16 (0x0afd, 0x0aff, 1),
            new Range16 (0x0b3c, 0x0b3c, 1),
            new Range16 (0x0b4d, 0x0b4d, 1),
            new Range16 (0x0b55, 0x0b55, 1),
            new Range16 (0x0bcd, 0x0bcd, 1),
            new Range16 (0x0c3c, 0x0c3c, 1),
            new Range16 (0x0c4d, 0x0c4d, 1),
            new Range16 (0x0cbc, 0x0cbc, 1),
            new Range16 (0x0ccd, 0x0ccd, 1),
            new Range16 (0x0d3b, 0x0d3c, 1),
            new Range16 (0x0d4d, 0x0d4d, 1),
            new Range16 (0x0dca, 0x0dca, 1),
            new Range16 (0x0e47, 0x0e4c, 1),
            new Range16 (0x0e4e, 0x0e4e, 1),
            new Range16 (0x0eba, 0x0eba, 1),
            new Range16 (0x0ec8, 0x0ecc, 1),
            new Range16 (0x0f18, 0x0f19, 1),
            new Range16 (0x0f35, 0x0f35, 1),
            new Range16 (0x0f37, 0x0f37, 1),
            new Range16 (0x0f39, 0x0f39, 1),
            new Range16 (0x0f3e, 0x0f3f, 1),
            new Range16 (0x0f82, 0x0f84, 1),
            new Range16 (0x0f86, 0x0f87, 1),
            new Range16 (0x0fc6, 0x0fc6, 1),
            new Range16 (0x1037, 0x1037, 1),
            new Range16 (0x1039, 0x103a, 1),
            new Range16 (0x1063, 0x1064, 1),
            new Range16 (0x1069, 0x106d, 1),
            new Range16 (0x1087, 0x108d, 1),
            new Range16 (0x108f, 0x108f, 1),
            new Range16 (0x109a, 0x109b, 1),
            new Range16 (0x135d, 0x135f, 1),
            new Range16 (0x1714, 0x1715, 1),
            new Range16 (0x17c9, 0x17d3, 1),
            new Range16 (0x17dd, 0x17dd, 1),
            new Range16 (0x1939, 0x193b, 1),
            new Range16 (0x1a75, 0x1a7c, 1),
            new Range16 (0x1a7f, 0x1a7f, 1),
            new Range16 (0x1ab0, 0x1abe, 1),
            new Range16 (0x1ac1, 0x1acb, 1),
            new Range16 (0x1b34, 0x1b34, 1),
            new Range16 (0x1b44, 0x1b44, 1),
            new Range16 (0x1b6b, 0x1b73, 1),
            new Range16 (0x1baa, 0x1bab, 1),
            new Range16 (0x1c36, 0x1c37, 1),
            new Range16 (0x1c78, 0x1c7d, 1),
            new Range16 (0x1cd0, 0x1ce8, 1),
            new Range16 (0x1ced, 0x1ced, 1),
            new Range16 (0x1cf4, 0x1cf4, 1),
            new Range16 (0x1cf7, 0x1cf9, 1),
            new Range16 (0x1d2c, 0x1d6a, 1),
            new Range16 (0x1dc4, 0x1dcf, 1),
            new Range16 (0x1df5, 0x1dff, 1),
            new Range16 (0x1fbd, 0x1fbd, 1),
            new Range16 (0x1fbf, 0x1fc1, 1),
            new Range16 (0x1fcd, 0x1fcf, 1),
            new Range16 (0x1fdd, 0x1fdf, 1),
            new Range16 (0x1fed, 0x1fef, 1),
            new Range16 (0x1ffd, 0x1ffe, 1),
            new Range16 (0x2cef, 0x2cf1, 1),
            new Range16 (0x2e2f, 0x2e2f, 1),
            new Range16 (0x302a, 0x302f, 1),
            new Range16 (0x3099, 0x309c, 1),
            new Range16 (0x30fc, 0x30fc, 1),
            new Range16 (0xa66f, 0xa66f, 1),
            new Range16 (0xa67c, 0xa67d, 1),
            new Range16 (0xa67f, 0xa67f, 1),
            new Range16 (0xa69c, 0xa69d, 1),
            new Range16 (0xa6f0, 0xa6f1, 1),
            new Range16 (0xa700, 0xa721, 1),
            new Range16 (0xa788, 0xa78a, 1),
            new Range16 (0xa7f8, 0xa7f9, 1),
            new Range16 (0xa8c4, 0xa8c4, 1),
            new Range16 (0xa8e0, 0xa8f1, 1),
            new Range16 (0xa92b, 0xa92e, 1),
            new Range16 (0xa953, 0xa953, 1),
            new Range16 (0xa9b3, 0xa9b3, 1),
            new Range16 (0xa9c0, 0xa9c0, 1),
            new Range16 (0xa9e5, 0xa9e5, 1),
            new Range16 (0xaa7b, 0xaa7d, 1),
            new Range16 (0xaabf, 0xaac2, 1),
            new Range16 (0xaaf6, 0xaaf6, 1),
            new Range16 (0xab5b, 0xab5f, 1),
            new Range16 (0xab69, 0xab6b, 1),
            new Range16 (0xabec, 0xabed, 1),
            new Range16 (0xfb1e, 0xfb1e, 1),
            new Range16 (0xfe20, 0xfe2f, 1),
            new Range16 (0xff3e, 0xff3e, 1),
            new Range16 (0xff40, 0xff40, 1),
            new Range16 (0xff70, 0xff70, 1),
            new Range16 (0xff9e, 0xff9f, 1),
            new Range16 (0xffe3, 0xffe3, 1),
                },
                r32: new Range32[] {
            new Range32 (0x102e0, 0x102e0, 1),
            new Range32 (0x10780, 0x10785, 1),
            new Range32 (0x10787, 0x107b0, 1),
            new Range32 (0x107b2, 0x107ba, 1),
            new Range32 (0x10ae5, 0x10ae6, 1),
            new Range32 (0x10d22, 0x10d27, 1),
            new Range32 (0x10efd, 0x10eff, 1),
            new Range32 (0x10f46, 0x10f50, 1),
            new Range32 (0x10f82, 0x10f85, 1),
            new Range32 (0x11046, 0x11046, 1),
            new Range32 (0x11070, 0x11070, 1),
            new Range32 (0x110b9, 0x110ba, 1),
            new Range32 (0x11133, 0x11134, 1),
            new Range32 (0x11173, 0x11173, 1),
            new Range32 (0x111c0, 0x111c0, 1),
            new Range32 (0x111ca, 0x111cc, 1),
            new Range32 (0x11235, 0x11236, 1),
            new Range32 (0x112e9, 0x112ea, 1),
            new Range32 (0x1133c, 0x1133c, 1),
            new Range32 (0x1134d, 0x1134d, 1),
            new Range32 (0x11366, 0x1136c, 1),
            new Range32 (0x11370, 0x11374, 1),
            new Range32 (0x11442, 0x11442, 1),
            new Range32 (0x11446, 0x11446, 1),
            new Range32 (0x114c2, 0x114c3, 1),
            new Range32 (0x115bf, 0x115c0, 1),
            new Range32 (0x1163f, 0x1163f, 1),
            new Range32 (0x116b6, 0x116b7, 1),
            new Range32 (0x1172b, 0x1172b, 1),
            new Range32 (0x11839, 0x1183a, 1),
            new Range32 (0x1193d, 0x1193e, 1),
            new Range32 (0x11943, 0x11943, 1),
            new Range32 (0x119e0, 0x119e0, 1),
            new Range32 (0x11a34, 0x11a34, 1),
            new Range32 (0x11a47, 0x11a47, 1),
            new Range32 (0x11a99, 0x11a99, 1),
            new Range32 (0x11c3f, 0x11c3f, 1),
            new Range32 (0x11d42, 0x11d42, 1),
            new Range32 (0x11d44, 0x11d45, 1),
            new Range32 (0x11d97, 0x11d97, 1),
            new Range32 (0x13447, 0x13455, 1),
            new Range32 (0x16af0, 0x16af4, 1),
            new Range32 (0x16b30, 0x16b36, 1),
            new Range32 (0x16f8f, 0x16f9f, 1),
            new Range32 (0x16ff0, 0x16ff1, 1),
            new Range32 (0x1aff0, 0x1aff3, 1),
            new Range32 (0x1aff5, 0x1affb, 1),
            new Range32 (0x1affd, 0x1affe, 1),
            new Range32 (0x1cf00, 0x1cf2d, 1),
            new Range32 (0x1cf30, 0x1cf46, 1),
            new Range32 (0x1d167, 0x1d169, 1),
            new Range32 (0x1d16d, 0x1d172, 1),
            new Range32 (0x1d17b, 0x1d182, 1),
            new Range32 (0x1d185, 0x1d18b, 1),
            new Range32 (0x1d1aa, 0x1d1ad, 1),
            new Range32 (0x1e030, 0x1e06d, 1),
            new Range32 (0x1e130, 0x1e136, 1),
            new Range32 (0x1e2ae, 0x1e2ae, 1),
            new Range32 (0x1e2ec, 0x1e2ef, 1),
            new Range32 (0x1e8d0, 0x1e8d6, 1),
            new Range32 (0x1e944, 0x1e946, 1),
            new Range32 (0x1e948, 0x1e94a, 1),
                },
                latinOffset: 6
            ); /* RangeTable */

            internal static RangeTable _Extender = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x00b7, 0x00b7, 1),
            new Range16 (0x02d0, 0x02d1, 1),
            new Range16 (0x0640, 0x0640, 1),
            new Range16 (0x07fa, 0x07fa, 1),
            new Range16 (0x0b55, 0x0b55, 1),
            new Range16 (0x0e46, 0x0e46, 1),
            new Range16 (0x0ec6, 0x0ec6, 1),
            new Range16 (0x180a, 0x180a, 1),
            new Range16 (0x1843, 0x1843, 1),
            new Range16 (0x1aa7, 0x1aa7, 1),
            new Range16 (0x1c36, 0x1c36, 1),
            new Range16 (0x1c7b, 0x1c7b, 1),
            new Range16 (0x3005, 0x3005, 1),
            new Range16 (0x3031, 0x3035, 1),
            new Range16 (0x309d, 0x309e, 1),
            new Range16 (0x30fc, 0x30fe, 1),
            new Range16 (0xa015, 0xa015, 1),
            new Range16 (0xa60c, 0xa60c, 1),
            new Range16 (0xa9cf, 0xa9cf, 1),
            new Range16 (0xa9e6, 0xa9e6, 1),
            new Range16 (0xaa70, 0xaa70, 1),
            new Range16 (0xaadd, 0xaadd, 1),
            new Range16 (0xaaf3, 0xaaf4, 1),
            new Range16 (0xff70, 0xff70, 1),
                },
                r32: new Range32[] {
            new Range32 (0x10781, 0x10782, 1),
            new Range32 (0x1135d, 0x1135d, 1),
            new Range32 (0x115c6, 0x115c8, 1),
            new Range32 (0x11a98, 0x11a98, 1),
            new Range32 (0x16b42, 0x16b43, 1),
            new Range32 (0x16fe0, 0x16fe1, 1),
            new Range32 (0x16fe3, 0x16fe3, 1),
            new Range32 (0x1e13c, 0x1e13d, 1),
            new Range32 (0x1e944, 0x1e946, 1),
                },
                latinOffset: 1
            ); /* RangeTable */

            internal static RangeTable _Hex_Digit = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0030, 0x0039, 1),
            new Range16 (0x0041, 0x0046, 1),
            new Range16 (0x0061, 0x0066, 1),
            new Range16 (0xff10, 0xff19, 1),
            new Range16 (0xff21, 0xff26, 1),
            new Range16 (0xff41, 0xff46, 1),
                },
                latinOffset: 3
            ); /* RangeTable */

            internal static RangeTable _Hyphen = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x002d, 0x002d, 1),
            new Range16 (0x00ad, 0x00ad, 1),
            new Range16 (0x058a, 0x058a, 1),
            new Range16 (0x1806, 0x1806, 1),
            new Range16 (0x2010, 0x2011, 1),
            new Range16 (0x2e17, 0x2e17, 1),
            new Range16 (0x30fb, 0x30fb, 1),
            new Range16 (0xfe63, 0xfe63, 1),
            new Range16 (0xff0d, 0xff0d, 1),
            new Range16 (0xff65, 0xff65, 1),
                },
                latinOffset: 2
            ); /* RangeTable */

            internal static RangeTable _IDS_Binary_Operator = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x2ff0, 0x2ff1, 1),
            new Range16 (0x2ff4, 0x2ffb, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _IDS_Trinary_Operator = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x2ff2, 0x2ff3, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Ideographic = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x3006, 0x3007, 1),
            new Range16 (0x3021, 0x3029, 1),
            new Range16 (0x3038, 0x303a, 1),
            new Range16 (0x3400, 0x4dbf, 1),
            new Range16 (0x4e00, 0x9fff, 1),
            new Range16 (0xf900, 0xfa6d, 1),
            new Range16 (0xfa70, 0xfad9, 1),
                },
                r32: new Range32[] {
            new Range32 (0x16fe4, 0x16fe4, 1),
            new Range32 (0x17000, 0x187f7, 1),
            new Range32 (0x18800, 0x18cd5, 1),
            new Range32 (0x18d00, 0x18d08, 1),
            new Range32 (0x1b170, 0x1b2fb, 1),
            new Range32 (0x20000, 0x2a6df, 1),
            new Range32 (0x2a700, 0x2b739, 1),
            new Range32 (0x2b740, 0x2b81d, 1),
            new Range32 (0x2b820, 0x2cea1, 1),
            new Range32 (0x2ceb0, 0x2ebe0, 1),
            new Range32 (0x2f800, 0x2fa1d, 1),
            new Range32 (0x30000, 0x3134a, 1),
            new Range32 (0x31350, 0x323af, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Join_Control = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x200c, 0x200d, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Logical_Order_Exception = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0e40, 0x0e44, 1),
            new Range16 (0x0ec0, 0x0ec4, 1),
            new Range16 (0x19b5, 0x19b7, 1),
            new Range16 (0x19ba, 0x19ba, 1),
            new Range16 (0xaab5, 0xaab6, 1),
            new Range16 (0xaab9, 0xaab9, 1),
            new Range16 (0xaabb, 0xaabc, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Noncharacter_Code_Point = new RangeTable(
                r16: new Range16[] {
            new Range16 (0xfdd0, 0xfdef, 1),
            new Range16 (0xfffe, 0xffff, 1),
                },
                r32: new Range32[] {
            new Range32 (0x1fffe, 0x1ffff, 1),
            new Range32 (0x2fffe, 0x2ffff, 1),
            new Range32 (0x3fffe, 0x3ffff, 1),
            new Range32 (0x4fffe, 0x4ffff, 1),
            new Range32 (0x5fffe, 0x5ffff, 1),
            new Range32 (0x6fffe, 0x6ffff, 1),
            new Range32 (0x7fffe, 0x7ffff, 1),
            new Range32 (0x8fffe, 0x8ffff, 1),
            new Range32 (0x9fffe, 0x9ffff, 1),
            new Range32 (0xafffe, 0xaffff, 1),
            new Range32 (0xbfffe, 0xbffff, 1),
            new Range32 (0xcfffe, 0xcffff, 1),
            new Range32 (0xdfffe, 0xdffff, 1),
            new Range32 (0xefffe, 0xeffff, 1),
            new Range32 (0xffffe, 0xfffff, 1),
            new Range32 (0x10fffe, 0x10ffff, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Other_Alphabetic = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0345, 0x0345, 1),
            new Range16 (0x05b0, 0x05bd, 1),
            new Range16 (0x05bf, 0x05bf, 1),
            new Range16 (0x05c1, 0x05c2, 1),
            new Range16 (0x05c4, 0x05c5, 1),
            new Range16 (0x05c7, 0x05c7, 1),
            new Range16 (0x0610, 0x061a, 1),
            new Range16 (0x064b, 0x0657, 1),
            new Range16 (0x0659, 0x065f, 1),
            new Range16 (0x0670, 0x0670, 1),
            new Range16 (0x06d6, 0x06dc, 1),
            new Range16 (0x06e1, 0x06e4, 1),
            new Range16 (0x06e7, 0x06e8, 1),
            new Range16 (0x06ed, 0x06ed, 1),
            new Range16 (0x0711, 0x0711, 1),
            new Range16 (0x0730, 0x073f, 1),
            new Range16 (0x07a6, 0x07b0, 1),
            new Range16 (0x0816, 0x0817, 1),
            new Range16 (0x081b, 0x0823, 1),
            new Range16 (0x0825, 0x0827, 1),
            new Range16 (0x0829, 0x082c, 1),
            new Range16 (0x08d4, 0x08df, 1),
            new Range16 (0x08e3, 0x08e9, 1),
            new Range16 (0x08f0, 0x0903, 1),
            new Range16 (0x093a, 0x093b, 1),
            new Range16 (0x093e, 0x094c, 1),
            new Range16 (0x094e, 0x094f, 1),
            new Range16 (0x0955, 0x0957, 1),
            new Range16 (0x0962, 0x0963, 1),
            new Range16 (0x0981, 0x0983, 1),
            new Range16 (0x09be, 0x09c4, 1),
            new Range16 (0x09c7, 0x09c8, 1),
            new Range16 (0x09cb, 0x09cc, 1),
            new Range16 (0x09d7, 0x09d7, 1),
            new Range16 (0x09e2, 0x09e3, 1),
            new Range16 (0x0a01, 0x0a03, 1),
            new Range16 (0x0a3e, 0x0a42, 1),
            new Range16 (0x0a47, 0x0a48, 1),
            new Range16 (0x0a4b, 0x0a4c, 1),
            new Range16 (0x0a51, 0x0a51, 1),
            new Range16 (0x0a70, 0x0a71, 1),
            new Range16 (0x0a75, 0x0a75, 1),
            new Range16 (0x0a81, 0x0a83, 1),
            new Range16 (0x0abe, 0x0ac5, 1),
            new Range16 (0x0ac7, 0x0ac9, 1),
            new Range16 (0x0acb, 0x0acc, 1),
            new Range16 (0x0ae2, 0x0ae3, 1),
            new Range16 (0x0afa, 0x0afc, 1),
            new Range16 (0x0b01, 0x0b03, 1),
            new Range16 (0x0b3e, 0x0b44, 1),
            new Range16 (0x0b47, 0x0b48, 1),
            new Range16 (0x0b4b, 0x0b4c, 1),
            new Range16 (0x0b56, 0x0b57, 1),
            new Range16 (0x0b62, 0x0b63, 1),
            new Range16 (0x0b82, 0x0b82, 1),
            new Range16 (0x0bbe, 0x0bc2, 1),
            new Range16 (0x0bc6, 0x0bc8, 1),
            new Range16 (0x0bca, 0x0bcc, 1),
            new Range16 (0x0bd7, 0x0bd7, 1),
            new Range16 (0x0c00, 0x0c04, 1),
            new Range16 (0x0c3e, 0x0c44, 1),
            new Range16 (0x0c46, 0x0c48, 1),
            new Range16 (0x0c4a, 0x0c4c, 1),
            new Range16 (0x0c55, 0x0c56, 1),
            new Range16 (0x0c62, 0x0c63, 1),
            new Range16 (0x0c81, 0x0c83, 1),
            new Range16 (0x0cbe, 0x0cc4, 1),
            new Range16 (0x0cc6, 0x0cc8, 1),
            new Range16 (0x0cca, 0x0ccc, 1),
            new Range16 (0x0cd5, 0x0cd6, 1),
            new Range16 (0x0ce2, 0x0ce3, 1),
            new Range16 (0x0cf3, 0x0cf3, 1),
            new Range16 (0x0d00, 0x0d03, 1),
            new Range16 (0x0d3e, 0x0d44, 1),
            new Range16 (0x0d46, 0x0d48, 1),
            new Range16 (0x0d4a, 0x0d4c, 1),
            new Range16 (0x0d57, 0x0d57, 1),
            new Range16 (0x0d62, 0x0d63, 1),
            new Range16 (0x0d81, 0x0d83, 1),
            new Range16 (0x0dcf, 0x0dd4, 1),
            new Range16 (0x0dd6, 0x0dd6, 1),
            new Range16 (0x0dd8, 0x0ddf, 1),
            new Range16 (0x0df2, 0x0df3, 1),
            new Range16 (0x0e31, 0x0e31, 1),
            new Range16 (0x0e34, 0x0e3a, 1),
            new Range16 (0x0e4d, 0x0e4d, 1),
            new Range16 (0x0eb1, 0x0eb1, 1),
            new Range16 (0x0eb4, 0x0eb9, 1),
            new Range16 (0x0ebb, 0x0ebc, 1),
            new Range16 (0x0ecd, 0x0ecd, 1),
            new Range16 (0x0f71, 0x0f83, 1),
            new Range16 (0x0f8d, 0x0f97, 1),
            new Range16 (0x0f99, 0x0fbc, 1),
            new Range16 (0x102b, 0x1036, 1),
            new Range16 (0x1038, 0x1038, 1),
            new Range16 (0x103b, 0x103e, 1),
            new Range16 (0x1056, 0x1059, 1),
            new Range16 (0x105e, 0x1060, 1),
            new Range16 (0x1062, 0x1064, 1),
            new Range16 (0x1067, 0x106d, 1),
            new Range16 (0x1071, 0x1074, 1),
            new Range16 (0x1082, 0x108d, 1),
            new Range16 (0x108f, 0x108f, 1),
            new Range16 (0x109a, 0x109d, 1),
            new Range16 (0x1712, 0x1713, 1),
            new Range16 (0x1732, 0x1733, 1),
            new Range16 (0x1752, 0x1753, 1),
            new Range16 (0x1772, 0x1773, 1),
            new Range16 (0x17b6, 0x17c8, 1),
            new Range16 (0x1885, 0x1886, 1),
            new Range16 (0x18a9, 0x18a9, 1),
            new Range16 (0x1920, 0x192b, 1),
            new Range16 (0x1930, 0x1938, 1),
            new Range16 (0x1a17, 0x1a1b, 1),
            new Range16 (0x1a55, 0x1a5e, 1),
            new Range16 (0x1a61, 0x1a74, 1),
            new Range16 (0x1abf, 0x1ac0, 1),
            new Range16 (0x1acc, 0x1ace, 1),
            new Range16 (0x1b00, 0x1b04, 1),
            new Range16 (0x1b35, 0x1b43, 1),
            new Range16 (0x1b80, 0x1b82, 1),
            new Range16 (0x1ba1, 0x1ba9, 1),
            new Range16 (0x1bac, 0x1bad, 1),
            new Range16 (0x1be7, 0x1bf1, 1),
            new Range16 (0x1c24, 0x1c36, 1),
            new Range16 (0x1de7, 0x1df4, 1),
            new Range16 (0x24b6, 0x24e9, 1),
            new Range16 (0x2de0, 0x2dff, 1),
            new Range16 (0xa674, 0xa67b, 1),
            new Range16 (0xa69e, 0xa69f, 1),
            new Range16 (0xa802, 0xa802, 1),
            new Range16 (0xa80b, 0xa80b, 1),
            new Range16 (0xa823, 0xa827, 1),
            new Range16 (0xa880, 0xa881, 1),
            new Range16 (0xa8b4, 0xa8c3, 1),
            new Range16 (0xa8c5, 0xa8c5, 1),
            new Range16 (0xa8ff, 0xa8ff, 1),
            new Range16 (0xa926, 0xa92a, 1),
            new Range16 (0xa947, 0xa952, 1),
            new Range16 (0xa980, 0xa983, 1),
            new Range16 (0xa9b4, 0xa9bf, 1),
            new Range16 (0xa9e5, 0xa9e5, 1),
            new Range16 (0xaa29, 0xaa36, 1),
            new Range16 (0xaa43, 0xaa43, 1),
            new Range16 (0xaa4c, 0xaa4d, 1),
            new Range16 (0xaa7b, 0xaa7d, 1),
            new Range16 (0xaab0, 0xaab0, 1),
            new Range16 (0xaab2, 0xaab4, 1),
            new Range16 (0xaab7, 0xaab8, 1),
            new Range16 (0xaabe, 0xaabe, 1),
            new Range16 (0xaaeb, 0xaaef, 1),
            new Range16 (0xaaf5, 0xaaf5, 1),
            new Range16 (0xabe3, 0xabea, 1),
            new Range16 (0xfb1e, 0xfb1e, 1),
                },
                r32: new Range32[] {
            new Range32 (0x10376, 0x1037a, 1),
            new Range32 (0x10a01, 0x10a03, 1),
            new Range32 (0x10a05, 0x10a06, 1),
            new Range32 (0x10a0c, 0x10a0f, 1),
            new Range32 (0x10d24, 0x10d27, 1),
            new Range32 (0x10eab, 0x10eac, 1),
            new Range32 (0x11000, 0x11002, 1),
            new Range32 (0x11038, 0x11045, 1),
            new Range32 (0x11073, 0x11074, 1),
            new Range32 (0x11080, 0x11082, 1),
            new Range32 (0x110b0, 0x110b8, 1),
            new Range32 (0x110c2, 0x110c2, 1),
            new Range32 (0x11100, 0x11102, 1),
            new Range32 (0x11127, 0x11132, 1),
            new Range32 (0x11145, 0x11146, 1),
            new Range32 (0x11180, 0x11182, 1),
            new Range32 (0x111b3, 0x111bf, 1),
            new Range32 (0x111ce, 0x111cf, 1),
            new Range32 (0x1122c, 0x11234, 1),
            new Range32 (0x11237, 0x11237, 1),
            new Range32 (0x1123e, 0x1123e, 1),
            new Range32 (0x11241, 0x11241, 1),
            new Range32 (0x112df, 0x112e8, 1),
            new Range32 (0x11300, 0x11303, 1),
            new Range32 (0x1133e, 0x11344, 1),
            new Range32 (0x11347, 0x11348, 1),
            new Range32 (0x1134b, 0x1134c, 1),
            new Range32 (0x11357, 0x11357, 1),
            new Range32 (0x11362, 0x11363, 1),
            new Range32 (0x11435, 0x11441, 1),
            new Range32 (0x11443, 0x11445, 1),
            new Range32 (0x114b0, 0x114c1, 1),
            new Range32 (0x115af, 0x115b5, 1),
            new Range32 (0x115b8, 0x115be, 1),
            new Range32 (0x115dc, 0x115dd, 1),
            new Range32 (0x11630, 0x1163e, 1),
            new Range32 (0x11640, 0x11640, 1),
            new Range32 (0x116ab, 0x116b5, 1),
            new Range32 (0x1171d, 0x1172a, 1),
            new Range32 (0x1182c, 0x11838, 1),
            new Range32 (0x11930, 0x11935, 1),
            new Range32 (0x11937, 0x11938, 1),
            new Range32 (0x1193b, 0x1193c, 1),
            new Range32 (0x11940, 0x11940, 1),
            new Range32 (0x11942, 0x11942, 1),
            new Range32 (0x119d1, 0x119d7, 1),
            new Range32 (0x119da, 0x119df, 1),
            new Range32 (0x119e4, 0x119e4, 1),
            new Range32 (0x11a01, 0x11a0a, 1),
            new Range32 (0x11a35, 0x11a39, 1),
            new Range32 (0x11a3b, 0x11a3e, 1),
            new Range32 (0x11a51, 0x11a5b, 1),
            new Range32 (0x11a8a, 0x11a97, 1),
            new Range32 (0x11c2f, 0x11c36, 1),
            new Range32 (0x11c38, 0x11c3e, 1),
            new Range32 (0x11c92, 0x11ca7, 1),
            new Range32 (0x11ca9, 0x11cb6, 1),
            new Range32 (0x11d31, 0x11d36, 1),
            new Range32 (0x11d3a, 0x11d3a, 1),
            new Range32 (0x11d3c, 0x11d3d, 1),
            new Range32 (0x11d3f, 0x11d41, 1),
            new Range32 (0x11d43, 0x11d43, 1),
            new Range32 (0x11d47, 0x11d47, 1),
            new Range32 (0x11d8a, 0x11d8e, 1),
            new Range32 (0x11d90, 0x11d91, 1),
            new Range32 (0x11d93, 0x11d96, 1),
            new Range32 (0x11ef3, 0x11ef6, 1),
            new Range32 (0x11f00, 0x11f01, 1),
            new Range32 (0x11f03, 0x11f03, 1),
            new Range32 (0x11f34, 0x11f3a, 1),
            new Range32 (0x11f3e, 0x11f40, 1),
            new Range32 (0x16f4f, 0x16f4f, 1),
            new Range32 (0x16f51, 0x16f87, 1),
            new Range32 (0x16f8f, 0x16f92, 1),
            new Range32 (0x16ff0, 0x16ff1, 1),
            new Range32 (0x1bc9e, 0x1bc9e, 1),
            new Range32 (0x1e000, 0x1e006, 1),
            new Range32 (0x1e008, 0x1e018, 1),
            new Range32 (0x1e01b, 0x1e021, 1),
            new Range32 (0x1e023, 0x1e024, 1),
            new Range32 (0x1e026, 0x1e02a, 1),
            new Range32 (0x1e08f, 0x1e08f, 1),
            new Range32 (0x1e947, 0x1e947, 1),
            new Range32 (0x1f130, 0x1f149, 1),
            new Range32 (0x1f150, 0x1f169, 1),
            new Range32 (0x1f170, 0x1f189, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Other_Default_Ignorable_Code_Point = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x034f, 0x034f, 1),
            new Range16 (0x115f, 0x1160, 1),
            new Range16 (0x17b4, 0x17b5, 1),
            new Range16 (0x2065, 0x2065, 1),
            new Range16 (0x3164, 0x3164, 1),
            new Range16 (0xffa0, 0xffa0, 1),
            new Range16 (0xfff0, 0xfff8, 1),
                },
                r32: new Range32[] {
            new Range32 (0xe0000, 0xe0000, 1),
            new Range32 (0xe0002, 0xe001f, 1),
            new Range32 (0xe0080, 0xe00ff, 1),
            new Range32 (0xe01f0, 0xe0fff, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Other_Grapheme_Extend = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x09be, 0x09be, 1),
            new Range16 (0x09d7, 0x09d7, 1),
            new Range16 (0x0b3e, 0x0b3e, 1),
            new Range16 (0x0b57, 0x0b57, 1),
            new Range16 (0x0bbe, 0x0bbe, 1),
            new Range16 (0x0bd7, 0x0bd7, 1),
            new Range16 (0x0cc2, 0x0cc2, 1),
            new Range16 (0x0cd5, 0x0cd6, 1),
            new Range16 (0x0d3e, 0x0d3e, 1),
            new Range16 (0x0d57, 0x0d57, 1),
            new Range16 (0x0dcf, 0x0dcf, 1),
            new Range16 (0x0ddf, 0x0ddf, 1),
            new Range16 (0x1b35, 0x1b35, 1),
            new Range16 (0x200c, 0x200c, 1),
            new Range16 (0x302e, 0x302f, 1),
            new Range16 (0xff9e, 0xff9f, 1),
                },
                r32: new Range32[] {
            new Range32 (0x1133e, 0x1133e, 1),
            new Range32 (0x11357, 0x11357, 1),
            new Range32 (0x114b0, 0x114b0, 1),
            new Range32 (0x114bd, 0x114bd, 1),
            new Range32 (0x115af, 0x115af, 1),
            new Range32 (0x11930, 0x11930, 1),
            new Range32 (0x1d165, 0x1d165, 1),
            new Range32 (0x1d16e, 0x1d172, 1),
            new Range32 (0xe0020, 0xe007f, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Other_ID_Continue = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x00b7, 0x00b7, 1),
            new Range16 (0x0387, 0x0387, 1),
            new Range16 (0x1369, 0x1371, 1),
            new Range16 (0x19da, 0x19da, 1),
                },
                latinOffset: 1
            ); /* RangeTable */

            internal static RangeTable _Other_ID_Start = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x1885, 0x1886, 1),
            new Range16 (0x2118, 0x2118, 1),
            new Range16 (0x212e, 0x212e, 1),
            new Range16 (0x309b, 0x309c, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Other_Lowercase = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x00aa, 0x00aa, 1),
            new Range16 (0x00ba, 0x00ba, 1),
            new Range16 (0x02b0, 0x02b8, 1),
            new Range16 (0x02c0, 0x02c1, 1),
            new Range16 (0x02e0, 0x02e4, 1),
            new Range16 (0x0345, 0x0345, 1),
            new Range16 (0x037a, 0x037a, 1),
            new Range16 (0x10fc, 0x10fc, 1),
            new Range16 (0x1d2c, 0x1d6a, 1),
            new Range16 (0x1d78, 0x1d78, 1),
            new Range16 (0x1d9b, 0x1dbf, 1),
            new Range16 (0x2071, 0x2071, 1),
            new Range16 (0x207f, 0x207f, 1),
            new Range16 (0x2090, 0x209c, 1),
            new Range16 (0x2170, 0x217f, 1),
            new Range16 (0x24d0, 0x24e9, 1),
            new Range16 (0x2c7c, 0x2c7d, 1),
            new Range16 (0xa69c, 0xa69d, 1),
            new Range16 (0xa770, 0xa770, 1),
            new Range16 (0xa7f2, 0xa7f4, 1),
            new Range16 (0xa7f8, 0xa7f9, 1),
            new Range16 (0xab5c, 0xab5f, 1),
            new Range16 (0xab69, 0xab69, 1),
                },
                r32: new Range32[] {
            new Range32 (0x10780, 0x10780, 1),
            new Range32 (0x10783, 0x10785, 1),
            new Range32 (0x10787, 0x107b0, 1),
            new Range32 (0x107b2, 0x107ba, 1),
            new Range32 (0x1e030, 0x1e06d, 1),
                },
                latinOffset: 2
            ); /* RangeTable */

            internal static RangeTable _Other_Math = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x005e, 0x005e, 1),
            new Range16 (0x03d0, 0x03d2, 1),
            new Range16 (0x03d5, 0x03d5, 1),
            new Range16 (0x03f0, 0x03f1, 1),
            new Range16 (0x03f4, 0x03f5, 1),
            new Range16 (0x2016, 0x2016, 1),
            new Range16 (0x2032, 0x2034, 1),
            new Range16 (0x2040, 0x2040, 1),
            new Range16 (0x2061, 0x2064, 1),
            new Range16 (0x207d, 0x207e, 1),
            new Range16 (0x208d, 0x208e, 1),
            new Range16 (0x20d0, 0x20dc, 1),
            new Range16 (0x20e1, 0x20e1, 1),
            new Range16 (0x20e5, 0x20e6, 1),
            new Range16 (0x20eb, 0x20ef, 1),
            new Range16 (0x2102, 0x2102, 1),
            new Range16 (0x2107, 0x2107, 1),
            new Range16 (0x210a, 0x2113, 1),
            new Range16 (0x2115, 0x2115, 1),
            new Range16 (0x2119, 0x211d, 1),
            new Range16 (0x2124, 0x2124, 1),
            new Range16 (0x2128, 0x2129, 1),
            new Range16 (0x212c, 0x212d, 1),
            new Range16 (0x212f, 0x2131, 1),
            new Range16 (0x2133, 0x2138, 1),
            new Range16 (0x213c, 0x213f, 1),
            new Range16 (0x2145, 0x2149, 1),
            new Range16 (0x2195, 0x2199, 1),
            new Range16 (0x219c, 0x219f, 1),
            new Range16 (0x21a1, 0x21a2, 1),
            new Range16 (0x21a4, 0x21a5, 1),
            new Range16 (0x21a7, 0x21a7, 1),
            new Range16 (0x21a9, 0x21ad, 1),
            new Range16 (0x21b0, 0x21b1, 1),
            new Range16 (0x21b6, 0x21b7, 1),
            new Range16 (0x21bc, 0x21cd, 1),
            new Range16 (0x21d0, 0x21d1, 1),
            new Range16 (0x21d3, 0x21d3, 1),
            new Range16 (0x21d5, 0x21db, 1),
            new Range16 (0x21dd, 0x21dd, 1),
            new Range16 (0x21e4, 0x21e5, 1),
            new Range16 (0x2308, 0x230b, 1),
            new Range16 (0x23b4, 0x23b5, 1),
            new Range16 (0x23b7, 0x23b7, 1),
            new Range16 (0x23d0, 0x23d0, 1),
            new Range16 (0x23e2, 0x23e2, 1),
            new Range16 (0x25a0, 0x25a1, 1),
            new Range16 (0x25ae, 0x25b6, 1),
            new Range16 (0x25bc, 0x25c0, 1),
            new Range16 (0x25c6, 0x25c7, 1),
            new Range16 (0x25ca, 0x25cb, 1),
            new Range16 (0x25cf, 0x25d3, 1),
            new Range16 (0x25e2, 0x25e2, 1),
            new Range16 (0x25e4, 0x25e4, 1),
            new Range16 (0x25e7, 0x25ec, 1),
            new Range16 (0x2605, 0x2606, 1),
            new Range16 (0x2640, 0x2640, 1),
            new Range16 (0x2642, 0x2642, 1),
            new Range16 (0x2660, 0x2663, 1),
            new Range16 (0x266d, 0x266e, 1),
            new Range16 (0x27c5, 0x27c6, 1),
            new Range16 (0x27e6, 0x27ef, 1),
            new Range16 (0x2983, 0x2998, 1),
            new Range16 (0x29d8, 0x29db, 1),
            new Range16 (0x29fc, 0x29fd, 1),
            new Range16 (0xfe61, 0xfe61, 1),
            new Range16 (0xfe63, 0xfe63, 1),
            new Range16 (0xfe68, 0xfe68, 1),
            new Range16 (0xff3c, 0xff3c, 1),
            new Range16 (0xff3e, 0xff3e, 1),
                },
                r32: new Range32[] {
            new Range32 (0x1d400, 0x1d454, 1),
            new Range32 (0x1d456, 0x1d49c, 1),
            new Range32 (0x1d49e, 0x1d49f, 1),
            new Range32 (0x1d4a2, 0x1d4a2, 1),
            new Range32 (0x1d4a5, 0x1d4a6, 1),
            new Range32 (0x1d4a9, 0x1d4ac, 1),
            new Range32 (0x1d4ae, 0x1d4b9, 1),
            new Range32 (0x1d4bb, 0x1d4bb, 1),
            new Range32 (0x1d4bd, 0x1d4c3, 1),
            new Range32 (0x1d4c5, 0x1d505, 1),
            new Range32 (0x1d507, 0x1d50a, 1),
            new Range32 (0x1d50d, 0x1d514, 1),
            new Range32 (0x1d516, 0x1d51c, 1),
            new Range32 (0x1d51e, 0x1d539, 1),
            new Range32 (0x1d53b, 0x1d53e, 1),
            new Range32 (0x1d540, 0x1d544, 1),
            new Range32 (0x1d546, 0x1d546, 1),
            new Range32 (0x1d54a, 0x1d550, 1),
            new Range32 (0x1d552, 0x1d6a5, 1),
            new Range32 (0x1d6a8, 0x1d6c0, 1),
            new Range32 (0x1d6c2, 0x1d6da, 1),
            new Range32 (0x1d6dc, 0x1d6fa, 1),
            new Range32 (0x1d6fc, 0x1d714, 1),
            new Range32 (0x1d716, 0x1d734, 1),
            new Range32 (0x1d736, 0x1d74e, 1),
            new Range32 (0x1d750, 0x1d76e, 1),
            new Range32 (0x1d770, 0x1d788, 1),
            new Range32 (0x1d78a, 0x1d7a8, 1),
            new Range32 (0x1d7aa, 0x1d7c2, 1),
            new Range32 (0x1d7c4, 0x1d7cb, 1),
            new Range32 (0x1d7ce, 0x1d7ff, 1),
            new Range32 (0x1ee00, 0x1ee03, 1),
            new Range32 (0x1ee05, 0x1ee1f, 1),
            new Range32 (0x1ee21, 0x1ee22, 1),
            new Range32 (0x1ee24, 0x1ee24, 1),
            new Range32 (0x1ee27, 0x1ee27, 1),
            new Range32 (0x1ee29, 0x1ee32, 1),
            new Range32 (0x1ee34, 0x1ee37, 1),
            new Range32 (0x1ee39, 0x1ee39, 1),
            new Range32 (0x1ee3b, 0x1ee3b, 1),
            new Range32 (0x1ee42, 0x1ee42, 1),
            new Range32 (0x1ee47, 0x1ee47, 1),
            new Range32 (0x1ee49, 0x1ee49, 1),
            new Range32 (0x1ee4b, 0x1ee4b, 1),
            new Range32 (0x1ee4d, 0x1ee4f, 1),
            new Range32 (0x1ee51, 0x1ee52, 1),
            new Range32 (0x1ee54, 0x1ee54, 1),
            new Range32 (0x1ee57, 0x1ee57, 1),
            new Range32 (0x1ee59, 0x1ee59, 1),
            new Range32 (0x1ee5b, 0x1ee5b, 1),
            new Range32 (0x1ee5d, 0x1ee5d, 1),
            new Range32 (0x1ee5f, 0x1ee5f, 1),
            new Range32 (0x1ee61, 0x1ee62, 1),
            new Range32 (0x1ee64, 0x1ee64, 1),
            new Range32 (0x1ee67, 0x1ee6a, 1),
            new Range32 (0x1ee6c, 0x1ee72, 1),
            new Range32 (0x1ee74, 0x1ee77, 1),
            new Range32 (0x1ee79, 0x1ee7c, 1),
            new Range32 (0x1ee7e, 0x1ee7e, 1),
            new Range32 (0x1ee80, 0x1ee89, 1),
            new Range32 (0x1ee8b, 0x1ee9b, 1),
            new Range32 (0x1eea1, 0x1eea3, 1),
            new Range32 (0x1eea5, 0x1eea9, 1),
            new Range32 (0x1eeab, 0x1eebb, 1),
                },
                latinOffset: 1
            ); /* RangeTable */

            internal static RangeTable _Other_Uppercase = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x2160, 0x216f, 1),
            new Range16 (0x24b6, 0x24cf, 1),
                },
                r32: new Range32[] {
            new Range32 (0x1f130, 0x1f149, 1),
            new Range32 (0x1f150, 0x1f169, 1),
            new Range32 (0x1f170, 0x1f189, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Pattern_Syntax = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0021, 0x002f, 1),
            new Range16 (0x003a, 0x0040, 1),
            new Range16 (0x005b, 0x005e, 1),
            new Range16 (0x0060, 0x0060, 1),
            new Range16 (0x007b, 0x007e, 1),
            new Range16 (0x00a1, 0x00a7, 1),
            new Range16 (0x00a9, 0x00a9, 1),
            new Range16 (0x00ab, 0x00ac, 1),
            new Range16 (0x00ae, 0x00ae, 1),
            new Range16 (0x00b0, 0x00b1, 1),
            new Range16 (0x00b6, 0x00b6, 1),
            new Range16 (0x00bb, 0x00bb, 1),
            new Range16 (0x00bf, 0x00bf, 1),
            new Range16 (0x00d7, 0x00d7, 1),
            new Range16 (0x00f7, 0x00f7, 1),
            new Range16 (0x2010, 0x2027, 1),
            new Range16 (0x2030, 0x203e, 1),
            new Range16 (0x2041, 0x2053, 1),
            new Range16 (0x2055, 0x205e, 1),
            new Range16 (0x2190, 0x245f, 1),
            new Range16 (0x2500, 0x2775, 1),
            new Range16 (0x2794, 0x2bff, 1),
            new Range16 (0x2e00, 0x2e7f, 1),
            new Range16 (0x3001, 0x3003, 1),
            new Range16 (0x3008, 0x3020, 1),
            new Range16 (0x3030, 0x3030, 1),
            new Range16 (0xfd3e, 0xfd3f, 1),
            new Range16 (0xfe45, 0xfe46, 1),
                },
                latinOffset: 15
            ); /* RangeTable */

            internal static RangeTable _Pattern_White_Space = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0009, 0x000d, 1),
            new Range16 (0x0020, 0x0020, 1),
            new Range16 (0x0085, 0x0085, 1),
            new Range16 (0x200e, 0x200f, 1),
            new Range16 (0x2028, 0x2029, 1),
                },
                latinOffset: 3
            ); /* RangeTable */

            internal static RangeTable _Prepended_Concatenation_Mark = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0600, 0x0605, 1),
            new Range16 (0x06dd, 0x06dd, 1),
            new Range16 (0x070f, 0x070f, 1),
            new Range16 (0x0890, 0x0891, 1),
            new Range16 (0x08e2, 0x08e2, 1),
                },
                r32: new Range32[] {
            new Range32 (0x110bd, 0x110bd, 1),
            new Range32 (0x110cd, 0x110cd, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Quotation_Mark = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0022, 0x0022, 1),
            new Range16 (0x0027, 0x0027, 1),
            new Range16 (0x00ab, 0x00ab, 1),
            new Range16 (0x00bb, 0x00bb, 1),
            new Range16 (0x2018, 0x201f, 1),
            new Range16 (0x2039, 0x203a, 1),
            new Range16 (0x2e42, 0x2e42, 1),
            new Range16 (0x300c, 0x300f, 1),
            new Range16 (0x301d, 0x301f, 1),
            new Range16 (0xfe41, 0xfe44, 1),
            new Range16 (0xff02, 0xff02, 1),
            new Range16 (0xff07, 0xff07, 1),
            new Range16 (0xff62, 0xff63, 1),
                },
                latinOffset: 4
            ); /* RangeTable */

            internal static RangeTable _Radical = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x2e80, 0x2e99, 1),
            new Range16 (0x2e9b, 0x2ef3, 1),
            new Range16 (0x2f00, 0x2fd5, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Regional_Indicator = new RangeTable(
                r16: new Range16[] {
                },
                r32: new Range32[] {
            new Range32 (0x1f1e6, 0x1f1ff, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Sentence_Terminal = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0021, 0x0021, 1),
            new Range16 (0x002e, 0x002e, 1),
            new Range16 (0x003f, 0x003f, 1),
            new Range16 (0x0589, 0x0589, 1),
            new Range16 (0x061d, 0x061f, 1),
            new Range16 (0x06d4, 0x06d4, 1),
            new Range16 (0x0700, 0x0702, 1),
            new Range16 (0x07f9, 0x07f9, 1),
            new Range16 (0x0837, 0x0837, 1),
            new Range16 (0x0839, 0x0839, 1),
            new Range16 (0x083d, 0x083e, 1),
            new Range16 (0x0964, 0x0965, 1),
            new Range16 (0x104a, 0x104b, 1),
            new Range16 (0x1362, 0x1362, 1),
            new Range16 (0x1367, 0x1368, 1),
            new Range16 (0x166e, 0x166e, 1),
            new Range16 (0x1735, 0x1736, 1),
            new Range16 (0x1803, 0x1803, 1),
            new Range16 (0x1809, 0x1809, 1),
            new Range16 (0x1944, 0x1945, 1),
            new Range16 (0x1aa8, 0x1aab, 1),
            new Range16 (0x1b5a, 0x1b5b, 1),
            new Range16 (0x1b5e, 0x1b5f, 1),
            new Range16 (0x1b7d, 0x1b7e, 1),
            new Range16 (0x1c3b, 0x1c3c, 1),
            new Range16 (0x1c7e, 0x1c7f, 1),
            new Range16 (0x203c, 0x203d, 1),
            new Range16 (0x2047, 0x2049, 1),
            new Range16 (0x2e2e, 0x2e2e, 1),
            new Range16 (0x2e3c, 0x2e3c, 1),
            new Range16 (0x2e53, 0x2e54, 1),
            new Range16 (0x3002, 0x3002, 1),
            new Range16 (0xa4ff, 0xa4ff, 1),
            new Range16 (0xa60e, 0xa60f, 1),
            new Range16 (0xa6f3, 0xa6f3, 1),
            new Range16 (0xa6f7, 0xa6f7, 1),
            new Range16 (0xa876, 0xa877, 1),
            new Range16 (0xa8ce, 0xa8cf, 1),
            new Range16 (0xa92f, 0xa92f, 1),
            new Range16 (0xa9c8, 0xa9c9, 1),
            new Range16 (0xaa5d, 0xaa5f, 1),
            new Range16 (0xaaf0, 0xaaf1, 1),
            new Range16 (0xabeb, 0xabeb, 1),
            new Range16 (0xfe52, 0xfe52, 1),
            new Range16 (0xfe56, 0xfe57, 1),
            new Range16 (0xff01, 0xff01, 1),
            new Range16 (0xff0e, 0xff0e, 1),
            new Range16 (0xff1f, 0xff1f, 1),
            new Range16 (0xff61, 0xff61, 1),
                },
                r32: new Range32[] {
            new Range32 (0x10a56, 0x10a57, 1),
            new Range32 (0x10f55, 0x10f59, 1),
            new Range32 (0x10f86, 0x10f89, 1),
            new Range32 (0x11047, 0x11048, 1),
            new Range32 (0x110be, 0x110c1, 1),
            new Range32 (0x11141, 0x11143, 1),
            new Range32 (0x111c5, 0x111c6, 1),
            new Range32 (0x111cd, 0x111cd, 1),
            new Range32 (0x111de, 0x111df, 1),
            new Range32 (0x11238, 0x11239, 1),
            new Range32 (0x1123b, 0x1123c, 1),
            new Range32 (0x112a9, 0x112a9, 1),
            new Range32 (0x1144b, 0x1144c, 1),
            new Range32 (0x115c2, 0x115c3, 1),
            new Range32 (0x115c9, 0x115d7, 1),
            new Range32 (0x11641, 0x11642, 1),
            new Range32 (0x1173c, 0x1173e, 1),
            new Range32 (0x11944, 0x11944, 1),
            new Range32 (0x11946, 0x11946, 1),
            new Range32 (0x11a42, 0x11a43, 1),
            new Range32 (0x11a9b, 0x11a9c, 1),
            new Range32 (0x11c41, 0x11c42, 1),
            new Range32 (0x11ef7, 0x11ef8, 1),
            new Range32 (0x11f43, 0x11f44, 1),
            new Range32 (0x16a6e, 0x16a6f, 1),
            new Range32 (0x16af5, 0x16af5, 1),
            new Range32 (0x16b37, 0x16b38, 1),
            new Range32 (0x16b44, 0x16b44, 1),
            new Range32 (0x16e98, 0x16e98, 1),
            new Range32 (0x1bc9f, 0x1bc9f, 1),
            new Range32 (0x1da88, 0x1da88, 1),
                },
                latinOffset: 3
            ); /* RangeTable */

            internal static RangeTable _Soft_Dotted = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0069, 0x006a, 1),
            new Range16 (0x012f, 0x012f, 1),
            new Range16 (0x0249, 0x0249, 1),
            new Range16 (0x0268, 0x0268, 1),
            new Range16 (0x029d, 0x029d, 1),
            new Range16 (0x02b2, 0x02b2, 1),
            new Range16 (0x03f3, 0x03f3, 1),
            new Range16 (0x0456, 0x0456, 1),
            new Range16 (0x0458, 0x0458, 1),
            new Range16 (0x1d62, 0x1d62, 1),
            new Range16 (0x1d96, 0x1d96, 1),
            new Range16 (0x1da4, 0x1da4, 1),
            new Range16 (0x1da8, 0x1da8, 1),
            new Range16 (0x1e2d, 0x1e2d, 1),
            new Range16 (0x1ecb, 0x1ecb, 1),
            new Range16 (0x2071, 0x2071, 1),
            new Range16 (0x2148, 0x2149, 1),
            new Range16 (0x2c7c, 0x2c7c, 1),
                },
                r32: new Range32[] {
            new Range32 (0x1d422, 0x1d423, 1),
            new Range32 (0x1d456, 0x1d457, 1),
            new Range32 (0x1d48a, 0x1d48b, 1),
            new Range32 (0x1d4be, 0x1d4bf, 1),
            new Range32 (0x1d4f2, 0x1d4f3, 1),
            new Range32 (0x1d526, 0x1d527, 1),
            new Range32 (0x1d55a, 0x1d55b, 1),
            new Range32 (0x1d58e, 0x1d58f, 1),
            new Range32 (0x1d5c2, 0x1d5c3, 1),
            new Range32 (0x1d5f6, 0x1d5f7, 1),
            new Range32 (0x1d62a, 0x1d62b, 1),
            new Range32 (0x1d65e, 0x1d65f, 1),
            new Range32 (0x1d692, 0x1d693, 1),
            new Range32 (0x1df1a, 0x1df1a, 1),
            new Range32 (0x1e04c, 0x1e04d, 1),
            new Range32 (0x1e068, 0x1e068, 1),
                },
                latinOffset: 1
            ); /* RangeTable */

            internal static RangeTable _Terminal_Punctuation = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0021, 0x0021, 1),
            new Range16 (0x002c, 0x002c, 1),
            new Range16 (0x002e, 0x002e, 1),
            new Range16 (0x003a, 0x003b, 1),
            new Range16 (0x003f, 0x003f, 1),
            new Range16 (0x037e, 0x037e, 1),
            new Range16 (0x0387, 0x0387, 1),
            new Range16 (0x0589, 0x0589, 1),
            new Range16 (0x05c3, 0x05c3, 1),
            new Range16 (0x060c, 0x060c, 1),
            new Range16 (0x061b, 0x061b, 1),
            new Range16 (0x061d, 0x061f, 1),
            new Range16 (0x06d4, 0x06d4, 1),
            new Range16 (0x0700, 0x070a, 1),
            new Range16 (0x070c, 0x070c, 1),
            new Range16 (0x07f8, 0x07f9, 1),
            new Range16 (0x0830, 0x083e, 1),
            new Range16 (0x085e, 0x085e, 1),
            new Range16 (0x0964, 0x0965, 1),
            new Range16 (0x0e5a, 0x0e5b, 1),
            new Range16 (0x0f08, 0x0f08, 1),
            new Range16 (0x0f0d, 0x0f12, 1),
            new Range16 (0x104a, 0x104b, 1),
            new Range16 (0x1361, 0x1368, 1),
            new Range16 (0x166e, 0x166e, 1),
            new Range16 (0x16eb, 0x16ed, 1),
            new Range16 (0x1735, 0x1736, 1),
            new Range16 (0x17d4, 0x17d6, 1),
            new Range16 (0x17da, 0x17da, 1),
            new Range16 (0x1802, 0x1805, 1),
            new Range16 (0x1808, 0x1809, 1),
            new Range16 (0x1944, 0x1945, 1),
            new Range16 (0x1aa8, 0x1aab, 1),
            new Range16 (0x1b5a, 0x1b5b, 1),
            new Range16 (0x1b5d, 0x1b5f, 1),
            new Range16 (0x1b7d, 0x1b7e, 1),
            new Range16 (0x1c3b, 0x1c3f, 1),
            new Range16 (0x1c7e, 0x1c7f, 1),
            new Range16 (0x203c, 0x203d, 1),
            new Range16 (0x2047, 0x2049, 1),
            new Range16 (0x2e2e, 0x2e2e, 1),
            new Range16 (0x2e3c, 0x2e3c, 1),
            new Range16 (0x2e41, 0x2e41, 1),
            new Range16 (0x2e4c, 0x2e4c, 1),
            new Range16 (0x2e4e, 0x2e4f, 1),
            new Range16 (0x2e53, 0x2e54, 1),
            new Range16 (0x3001, 0x3002, 1),
            new Range16 (0xa4fe, 0xa4ff, 1),
            new Range16 (0xa60d, 0xa60f, 1),
            new Range16 (0xa6f3, 0xa6f7, 1),
            new Range16 (0xa876, 0xa877, 1),
            new Range16 (0xa8ce, 0xa8cf, 1),
            new Range16 (0xa92f, 0xa92f, 1),
            new Range16 (0xa9c7, 0xa9c9, 1),
            new Range16 (0xaa5d, 0xaa5f, 1),
            new Range16 (0xaadf, 0xaadf, 1),
            new Range16 (0xaaf0, 0xaaf1, 1),
            new Range16 (0xabeb, 0xabeb, 1),
            new Range16 (0xfe50, 0xfe52, 1),
            new Range16 (0xfe54, 0xfe57, 1),
            new Range16 (0xff01, 0xff01, 1),
            new Range16 (0xff0c, 0xff0c, 1),
            new Range16 (0xff0e, 0xff0e, 1),
            new Range16 (0xff1a, 0xff1b, 1),
            new Range16 (0xff1f, 0xff1f, 1),
            new Range16 (0xff61, 0xff61, 1),
            new Range16 (0xff64, 0xff64, 1),
                },
                r32: new Range32[] {
            new Range32 (0x1039f, 0x1039f, 1),
            new Range32 (0x103d0, 0x103d0, 1),
            new Range32 (0x10857, 0x10857, 1),
            new Range32 (0x1091f, 0x1091f, 1),
            new Range32 (0x10a56, 0x10a57, 1),
            new Range32 (0x10af0, 0x10af5, 1),
            new Range32 (0x10b3a, 0x10b3f, 1),
            new Range32 (0x10b99, 0x10b9c, 1),
            new Range32 (0x10f55, 0x10f59, 1),
            new Range32 (0x10f86, 0x10f89, 1),
            new Range32 (0x11047, 0x1104d, 1),
            new Range32 (0x110be, 0x110c1, 1),
            new Range32 (0x11141, 0x11143, 1),
            new Range32 (0x111c5, 0x111c6, 1),
            new Range32 (0x111cd, 0x111cd, 1),
            new Range32 (0x111de, 0x111df, 1),
            new Range32 (0x11238, 0x1123c, 1),
            new Range32 (0x112a9, 0x112a9, 1),
            new Range32 (0x1144b, 0x1144d, 1),
            new Range32 (0x1145a, 0x1145b, 1),
            new Range32 (0x115c2, 0x115c5, 1),
            new Range32 (0x115c9, 0x115d7, 1),
            new Range32 (0x11641, 0x11642, 1),
            new Range32 (0x1173c, 0x1173e, 1),
            new Range32 (0x11944, 0x11944, 1),
            new Range32 (0x11946, 0x11946, 1),
            new Range32 (0x11a42, 0x11a43, 1),
            new Range32 (0x11a9b, 0x11a9c, 1),
            new Range32 (0x11aa1, 0x11aa2, 1),
            new Range32 (0x11c41, 0x11c43, 1),
            new Range32 (0x11c71, 0x11c71, 1),
            new Range32 (0x11ef7, 0x11ef8, 1),
            new Range32 (0x11f43, 0x11f44, 1),
            new Range32 (0x12470, 0x12474, 1),
            new Range32 (0x16a6e, 0x16a6f, 1),
            new Range32 (0x16af5, 0x16af5, 1),
            new Range32 (0x16b37, 0x16b39, 1),
            new Range32 (0x16b44, 0x16b44, 1),
            new Range32 (0x16e97, 0x16e98, 1),
            new Range32 (0x1bc9f, 0x1bc9f, 1),
            new Range32 (0x1da87, 0x1da8a, 1),
                },
                latinOffset: 5
            ); /* RangeTable */

            internal static RangeTable _Unified_Ideograph = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x3400, 0x4dbf, 1),
            new Range16 (0x4e00, 0x9fff, 1),
            new Range16 (0xfa0e, 0xfa0f, 1),
            new Range16 (0xfa11, 0xfa11, 1),
            new Range16 (0xfa13, 0xfa14, 1),
            new Range16 (0xfa1f, 0xfa1f, 1),
            new Range16 (0xfa21, 0xfa21, 1),
            new Range16 (0xfa23, 0xfa24, 1),
            new Range16 (0xfa27, 0xfa29, 1),
                },
                r32: new Range32[] {
            new Range32 (0x20000, 0x2a6df, 1),
            new Range32 (0x2a700, 0x2b739, 1),
            new Range32 (0x2b740, 0x2b81d, 1),
            new Range32 (0x2b820, 0x2cea1, 1),
            new Range32 (0x2ceb0, 0x2ebe0, 1),
            new Range32 (0x30000, 0x3134a, 1),
            new Range32 (0x31350, 0x323af, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _Variation_Selector = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x180b, 0x180d, 1),
            new Range16 (0x180f, 0x180f, 1),
            new Range16 (0xfe00, 0xfe0f, 1),
                },
                r32: new Range32[] {
            new Range32 (0xe0100, 0xe01ef, 1),
                }
            ); /* RangeTable */

            internal static RangeTable _White_Space = new RangeTable(
                r16: new Range16[] {
            new Range16 (0x0009, 0x000d, 1),
            new Range16 (0x0020, 0x0020, 1),
            new Range16 (0x0085, 0x0085, 1),
            new Range16 (0x00a0, 0x00a0, 1),
            new Range16 (0x1680, 0x1680, 1),
            new Range16 (0x2000, 0x200a, 1),
            new Range16 (0x2028, 0x2029, 1),
            new Range16 (0x202f, 0x202f, 1),
            new Range16 (0x205f, 0x205f, 1),
            new Range16 (0x3000, 0x3000, 1),
                },
                latinOffset: 4
            ); /* RangeTable */

            /// <summary>ASCII_Hex_Digit is the set of Unicode characters with property ASCII_Hex_Digit.</summary>
            public static RangeTable ASCII_Hex_Digit => _ASCII_Hex_Digit;
            /// <summary>Bidi_Control is the set of Unicode characters with property Bidi_Control.</summary>
            public static RangeTable Bidi_Control => _Bidi_Control;
            /// <summary>Dash is the set of Unicode characters with property Dash.</summary>
            public static RangeTable Dash => _Dash;
            /// <summary>Deprecated is the set of Unicode characters with property Deprecated.</summary>
            public static RangeTable Deprecated => _Deprecated;
            /// <summary>Diacritic is the set of Unicode characters with property Diacritic.</summary>
            public static RangeTable Diacritic => _Diacritic;
            /// <summary>Extender is the set of Unicode characters with property Extender.</summary>
            public static RangeTable Extender => _Extender;
            /// <summary>Hex_Digit is the set of Unicode characters with property Hex_Digit.</summary>
            public static RangeTable Hex_Digit => _Hex_Digit;
            /// <summary>Hyphen is the set of Unicode characters with property Hyphen.</summary>
            public static RangeTable Hyphen => _Hyphen;
            /// <summary>IDS_Binary_Operator is the set of Unicode characters with property IDS_Binary_Operator.</summary>
            public static RangeTable IDS_Binary_Operator => _IDS_Binary_Operator;
            /// <summary>IDS_Trinary_Operator is the set of Unicode characters with property IDS_Trinary_Operator.</summary>
            public static RangeTable IDS_Trinary_Operator => _IDS_Trinary_Operator;
            /// <summary>Ideographic is the set of Unicode characters with property Ideographic.</summary>
            public static RangeTable Ideographic => _Ideographic;
            /// <summary>Join_Control is the set of Unicode characters with property Join_Control.</summary>
            public static RangeTable Join_Control => _Join_Control;
            /// <summary>Logical_Order_Exception is the set of Unicode characters with property Logical_Order_Exception.</summary>
            public static RangeTable Logical_Order_Exception => _Logical_Order_Exception;
            /// <summary>Noncharacter_Code_Point is the set of Unicode characters with property Noncharacter_Code_Point.</summary>
            public static RangeTable Noncharacter_Code_Point => _Noncharacter_Code_Point;
            /// <summary>Other_Alphabetic is the set of Unicode characters with property Other_Alphabetic.</summary>
            public static RangeTable Other_Alphabetic => _Other_Alphabetic;
            /// <summary>Other_Default_Ignorable_Code_Point is the set of Unicode characters with property Other_Default_Ignorable_Code_Point.</summary>
            public static RangeTable Other_Default_Ignorable_Code_Point => _Other_Default_Ignorable_Code_Point;
            /// <summary>Other_Grapheme_Extend is the set of Unicode characters with property Other_Grapheme_Extend.</summary>
            public static RangeTable Other_Grapheme_Extend => _Other_Grapheme_Extend;
            /// <summary>Other_ID_Continue is the set of Unicode characters with property Other_ID_Continue.</summary>
            public static RangeTable Other_ID_Continue => _Other_ID_Continue;
            /// <summary>Other_ID_Start is the set of Unicode characters with property Other_ID_Start.</summary>
            public static RangeTable Other_ID_Start => _Other_ID_Start;
            /// <summary>Other_Lowercase is the set of Unicode characters with property Other_Lowercase.</summary>
            public static RangeTable Other_Lowercase => _Other_Lowercase;
            /// <summary>Other_Math is the set of Unicode characters with property Other_Math.</summary>
            public static RangeTable Other_Math => _Other_Math;
            /// <summary>Other_Uppercase is the set of Unicode characters with property Other_Uppercase.</summary>
            public static RangeTable Other_Uppercase => _Other_Uppercase;
            /// <summary>Pattern_Syntax is the set of Unicode characters with property Pattern_Syntax.</summary>
            public static RangeTable Pattern_Syntax => _Pattern_Syntax;
            /// <summary>Pattern_White_Space is the set of Unicode characters with property Pattern_White_Space.</summary>
            public static RangeTable Pattern_White_Space => _Pattern_White_Space;
            /// <summary>Prepended_Concatenation_Mark is the set of Unicode characters with property Prepended_Concatenation_Mark.</summary>
            public static RangeTable Prepended_Concatenation_Mark => _Prepended_Concatenation_Mark;
            /// <summary>Quotation_Mark is the set of Unicode characters with property Quotation_Mark.</summary>
            public static RangeTable Quotation_Mark => _Quotation_Mark;
            /// <summary>Radical is the set of Unicode characters with property Radical.</summary>
            public static RangeTable Radical => _Radical;
            /// <summary>Regional_Indicator is the set of Unicode characters with property Regional_Indicator.</summary>
            public static RangeTable Regional_Indicator => _Regional_Indicator;
            /// <summary>STerm is an alias for Sentence_Terminal.</summary>
            public static RangeTable STerm => _Sentence_Terminal;
            /// <summary>Sentence_Terminal is the set of Unicode characters with property Sentence_Terminal.</summary>
            public static RangeTable Sentence_Terminal => _Sentence_Terminal;
            /// <summary>Soft_Dotted is the set of Unicode characters with property Soft_Dotted.</summary>
            public static RangeTable Soft_Dotted => _Soft_Dotted;
            /// <summary>Terminal_Punctuation is the set of Unicode characters with property Terminal_Punctuation.</summary>
            public static RangeTable Terminal_Punctuation => _Terminal_Punctuation;
            /// <summary>Unified_Ideograph is the set of Unicode characters with property Unified_Ideograph.</summary>
            public static RangeTable Unified_Ideograph => _Unified_Ideograph;
            /// <summary>Variation_Selector is the set of Unicode characters with property Variation_Selector.</summary>
            public static RangeTable Variation_Selector => _Variation_Selector;
            /// <summary>White_Space is the set of Unicode characters with property White_Space.</summary>
            public static RangeTable White_Space => _White_Space;
        }

        // Generated by running
        //	maketables --data=https://www.unicode.org/Public/15.0.0/ucd/UnicodeData.txt --casefolding=https://www.unicode.org/Public/15.0.0/ucd/CaseFolding.txt
        // DO NOT EDIT

        // CaseRanges is the table describing case mappings for all letters with
        // non-self mappings.
        static CaseRange[] CaseRanges => _CaseRanges;
        static CaseRange[] _CaseRanges = new CaseRange[] {
        new CaseRange (0x0041, 0x005A, 0, 32, 0),
        new CaseRange (0x0061, 0x007A, -32, 0, -32),
        new CaseRange (0x00B5, 0x00B5, 743, 0, 743),
        new CaseRange (0x00C0, 0x00D6, 0, 32, 0),
        new CaseRange (0x00D8, 0x00DE, 0, 32, 0),
        new CaseRange (0x00E0, 0x00F6, -32, 0, -32),
        new CaseRange (0x00F8, 0x00FE, -32, 0, -32),
        new CaseRange (0x00FF, 0x00FF, 121, 0, 121),
        new CaseRange (0x0100, 0x012F, UpperLower, UpperLower, UpperLower),
        new CaseRange (0x0130, 0x0130, 0, -199, 0),
        new CaseRange (0x0131, 0x0131, -232, 0, -232),
        new CaseRange (0x0132, 0x0137, UpperLower, UpperLower, UpperLower),
        new CaseRange (0x0139, 0x0148, UpperLower, UpperLower, UpperLower),
        new CaseRange (0x014A, 0x0177, UpperLower, UpperLower, UpperLower),
        new CaseRange (0x0178, 0x0178, 0, -121, 0),
        new CaseRange (0x0179, 0x017E, UpperLower, UpperLower, UpperLower),
        new CaseRange (0x017F, 0x017F, -300, 0, -300),
        new CaseRange (0x0180, 0x0180, 195, 0, 195),
        new CaseRange (0x0181, 0x0181, 0, 210, 0),
        new CaseRange (0x0182, 0x0185, UpperLower, UpperLower, UpperLower),
        new CaseRange (0x0186, 0x0186, 0, 206, 0),
        new CaseRange (0x0187, 0x0188, UpperLower, UpperLower, UpperLower),
        new CaseRange (0x0189, 0x018A, 0, 205, 0),
        new CaseRange (0x018B, 0x018C, UpperLower, UpperLower, UpperLower),
        new CaseRange (0x018E, 0x018E, 0, 79, 0),
        new CaseRange (0x018F, 0x018F, 0, 202, 0),
        new CaseRange (0x0190, 0x0190, 0, 203, 0),
        new CaseRange (0x0191, 0x0192, UpperLower, UpperLower, UpperLower),
        new CaseRange (0x0193, 0x0193, 0, 205, 0),
        new CaseRange (0x0194, 0x0194, 0, 207, 0),
        new CaseRange (0x0195, 0x0195, 97, 0, 97),
        new CaseRange (0x0196, 0x0196, 0, 211, 0),
        new CaseRange (0x0197, 0x0197, 0, 209, 0),
        new CaseRange (0x0198, 0x0199, UpperLower, UpperLower, UpperLower),
        new CaseRange (0x019A, 0x019A, 163, 0, 163),
        new CaseRange (0x019C, 0x019C, 0, 211, 0),
        new CaseRange (0x019D, 0x019D, 0, 213, 0),
        new CaseRange (0x019E, 0x019E, 130, 0, 130),
        new CaseRange (0x019F, 0x019F, 0, 214, 0),
        new CaseRange (0x01A0, 0x01A5, UpperLower, UpperLower, UpperLower),
        new CaseRange (0x01A6, 0x01A6, 0, 218, 0),
        new CaseRange (0x01A7, 0x01A8, UpperLower, UpperLower, UpperLower),
        new CaseRange (0x01A9, 0x01A9, 0, 218, 0),
        new CaseRange (0x01AC, 0x01AD, UpperLower, UpperLower, UpperLower),
        new CaseRange (0x01AE, 0x01AE, 0, 218, 0),
        new CaseRange (0x01AF, 0x01B0, UpperLower, UpperLower, UpperLower),
        new CaseRange (0x01B1, 0x01B2, 0, 217, 0),
        new CaseRange (0x01B3, 0x01B6, UpperLower, UpperLower, UpperLower),
        new CaseRange (0x01B7, 0x01B7, 0, 219, 0),
        new CaseRange (0x01B8, 0x01B9, UpperLower, UpperLower, UpperLower),
        new CaseRange (0x01BC, 0x01BD, UpperLower, UpperLower, UpperLower),
        new CaseRange (0x01BF, 0x01BF, 56, 0, 56),
        new CaseRange (0x01C4, 0x01C4, 0, 2, 1),
        new CaseRange (0x01C5, 0x01C5, -1, 1, 0),
        new CaseRange (0x01C6, 0x01C6, -2, 0, -1),
        new CaseRange (0x01C7, 0x01C7, 0, 2, 1),
        new CaseRange (0x01C8, 0x01C8, -1, 1, 0),
        new CaseRange (0x01C9, 0x01C9, -2, 0, -1),
        new CaseRange (0x01CA, 0x01CA, 0, 2, 1),
        new CaseRange (0x01CB, 0x01CB, -1, 1, 0),
        new CaseRange (0x01CC, 0x01CC, -2, 0, -1),
        new CaseRange (0x01CD, 0x01DC, UpperLower, UpperLower, UpperLower),
        new CaseRange (0x01DD, 0x01DD, -79, 0, -79),
        new CaseRange (0x01DE, 0x01EF, UpperLower, UpperLower, UpperLower),
        new CaseRange (0x01F1, 0x01F1, 0, 2, 1),
        new CaseRange (0x01F2, 0x01F2, -1, 1, 0),
        new CaseRange (0x01F3, 0x01F3, -2, 0, -1),
        new CaseRange (0x01F4, 0x01F5, UpperLower, UpperLower, UpperLower),
        new CaseRange (0x01F6, 0x01F6, 0, -97, 0),
        new CaseRange (0x01F7, 0x01F7, 0, -56, 0),
        new CaseRange (0x01F8, 0x021F, UpperLower, UpperLower, UpperLower),
        new CaseRange (0x0220, 0x0220, 0, -130, 0),
        new CaseRange (0x0222, 0x0233, UpperLower, UpperLower, UpperLower),
        new CaseRange (0x023A, 0x023A, 0, 10795, 0),
        new CaseRange (0x023B, 0x023C, UpperLower, UpperLower, UpperLower),
        new CaseRange (0x023D, 0x023D, 0, -163, 0),
        new CaseRange (0x023E, 0x023E, 0, 10792, 0),
        new CaseRange (0x023F, 0x0240, 10815, 0, 10815),
        new CaseRange (0x0241, 0x0242, UpperLower, UpperLower, UpperLower),
        new CaseRange (0x0243, 0x0243, 0, -195, 0),
        new CaseRange (0x0244, 0x0244, 0, 69, 0),
        new CaseRange (0x0245, 0x0245, 0, 71, 0),
        new CaseRange (0x0246, 0x024F, UpperLower, UpperLower, UpperLower),
        new CaseRange (0x0250, 0x0250, 10783, 0, 10783),
        new CaseRange (0x0251, 0x0251, 10780, 0, 10780),
        new CaseRange (0x0252, 0x0252, 10782, 0, 10782),
        new CaseRange (0x0253, 0x0253, -210, 0, -210),
        new CaseRange (0x0254, 0x0254, -206, 0, -206),
        new CaseRange (0x0256, 0x0257, -205, 0, -205),
        new CaseRange (0x0259, 0x0259, -202, 0, -202),
        new CaseRange (0x025B, 0x025B, -203, 0, -203),
        new CaseRange (0x025C, 0x025C, 42319, 0, 42319),
        new CaseRange (0x0260, 0x0260, -205, 0, -205),
        new CaseRange (0x0261, 0x0261, 42315, 0, 42315),
        new CaseRange (0x0263, 0x0263, -207, 0, -207),
        new CaseRange (0x0265, 0x0265, 42280, 0, 42280),
        new CaseRange (0x0266, 0x0266, 42308, 0, 42308),
        new CaseRange (0x0268, 0x0268, -209, 0, -209),
        new CaseRange (0x0269, 0x0269, -211, 0, -211),
        new CaseRange (0x026A, 0x026A, 42308, 0, 42308),
        new CaseRange (0x026B, 0x026B, 10743, 0, 10743),
        new CaseRange (0x026C, 0x026C, 42305, 0, 42305),
        new CaseRange (0x026F, 0x026F, -211, 0, -211),
        new CaseRange (0x0271, 0x0271, 10749, 0, 10749),
        new CaseRange (0x0272, 0x0272, -213, 0, -213),
        new CaseRange (0x0275, 0x0275, -214, 0, -214),
        new CaseRange (0x027D, 0x027D, 10727, 0, 10727),
        new CaseRange (0x0280, 0x0280, -218, 0, -218),
        new CaseRange (0x0282, 0x0282, 42307, 0, 42307),
        new CaseRange (0x0283, 0x0283, -218, 0, -218),
        new CaseRange (0x0287, 0x0287, 42282, 0, 42282),
        new CaseRange (0x0288, 0x0288, -218, 0, -218),
        new CaseRange (0x0289, 0x0289, -69, 0, -69),
        new CaseRange (0x028A, 0x028B, -217, 0, -217),
        new CaseRange (0x028C, 0x028C, -71, 0, -71),
        new CaseRange (0x0292, 0x0292, -219, 0, -219),
        new CaseRange (0x029D, 0x029D, 42261, 0, 42261),
        new CaseRange (0x029E, 0x029E, 42258, 0, 42258),
        new CaseRange (0x0345, 0x0345, 84, 0, 84),
        new CaseRange (0x0370, 0x0373, UpperLower, UpperLower, UpperLower),
        new CaseRange (0x0376, 0x0377, UpperLower, UpperLower, UpperLower),
        new CaseRange (0x037B, 0x037D, 130, 0, 130),
        new CaseRange (0x037F, 0x037F, 0, 116, 0),
        new CaseRange (0x0386, 0x0386, 0, 38, 0),
        new CaseRange (0x0388, 0x038A, 0, 37, 0),
        new CaseRange (0x038C, 0x038C, 0, 64, 0),
        new CaseRange (0x038E, 0x038F, 0, 63, 0),
        new CaseRange (0x0391, 0x03A1, 0, 32, 0),
        new CaseRange (0x03A3, 0x03AB, 0, 32, 0),
        new CaseRange (0x03AC, 0x03AC, -38, 0, -38),
        new CaseRange (0x03AD, 0x03AF, -37, 0, -37),
        new CaseRange (0x03B1, 0x03C1, -32, 0, -32),
        new CaseRange (0x03C2, 0x03C2, -31, 0, -31),
        new CaseRange (0x03C3, 0x03CB, -32, 0, -32),
        new CaseRange (0x03CC, 0x03CC, -64, 0, -64),
        new CaseRange (0x03CD, 0x03CE, -63, 0, -63),
        new CaseRange (0x03CF, 0x03CF, 0, 8, 0),
        new CaseRange (0x03D0, 0x03D0, -62, 0, -62),
        new CaseRange (0x03D1, 0x03D1, -57, 0, -57),
        new CaseRange (0x03D5, 0x03D5, -47, 0, -47),
        new CaseRange (0x03D6, 0x03D6, -54, 0, -54),
        new CaseRange (0x03D7, 0x03D7, -8, 0, -8),
        new CaseRange (0x03D8, 0x03EF, UpperLower, UpperLower, UpperLower),
        new CaseRange (0x03F0, 0x03F0, -86, 0, -86),
        new CaseRange (0x03F1, 0x03F1, -80, 0, -80),
        new CaseRange (0x03F2, 0x03F2, 7, 0, 7),
        new CaseRange (0x03F3, 0x03F3, -116, 0, -116),
        new CaseRange (0x03F4, 0x03F4, 0, -60, 0),
        new CaseRange (0x03F5, 0x03F5, -96, 0, -96),
        new CaseRange (0x03F7, 0x03F8, UpperLower, UpperLower, UpperLower),
        new CaseRange (0x03F9, 0x03F9, 0, -7, 0),
        new CaseRange (0x03FA, 0x03FB, UpperLower, UpperLower, UpperLower),
        new CaseRange (0x03FD, 0x03FF, 0, -130, 0),
        new CaseRange (0x0400, 0x040F, 0, 80, 0),
        new CaseRange (0x0410, 0x042F, 0, 32, 0),
        new CaseRange (0x0430, 0x044F, -32, 0, -32),
        new CaseRange (0x0450, 0x045F, -80, 0, -80),
        new CaseRange (0x0460, 0x0481, UpperLower, UpperLower, UpperLower),
        new CaseRange (0x048A, 0x04BF, UpperLower, UpperLower, UpperLower),
        new CaseRange (0x04C0, 0x04C0, 0, 15, 0),
        new CaseRange (0x04C1, 0x04CE, UpperLower, UpperLower, UpperLower),
        new CaseRange (0x04CF, 0x04CF, -15, 0, -15),
        new CaseRange (0x04D0, 0x052F, UpperLower, UpperLower, UpperLower),
        new CaseRange (0x0531, 0x0556, 0, 48, 0),
        new CaseRange (0x0561, 0x0586, -48, 0, -48),
        new CaseRange (0x10A0, 0x10C5, 0, 7264, 0),
        new CaseRange (0x10C7, 0x10C7, 0, 7264, 0),
        new CaseRange (0x10CD, 0x10CD, 0, 7264, 0),
        new CaseRange (0x10D0, 0x10FA, 3008, 0, 0),
        new CaseRange (0x10FD, 0x10FF, 3008, 0, 0),
        new CaseRange (0x13A0, 0x13EF, 0, 38864, 0),
        new CaseRange (0x13F0, 0x13F5, 0, 8, 0),
        new CaseRange (0x13F8, 0x13FD, -8, 0, -8),
        new CaseRange (0x1C80, 0x1C80, -6254, 0, -6254),
        new CaseRange (0x1C81, 0x1C81, -6253, 0, -6253),
        new CaseRange (0x1C82, 0x1C82, -6244, 0, -6244),
        new CaseRange (0x1C83, 0x1C84, -6242, 0, -6242),
        new CaseRange (0x1C85, 0x1C85, -6243, 0, -6243),
        new CaseRange (0x1C86, 0x1C86, -6236, 0, -6236),
        new CaseRange (0x1C87, 0x1C87, -6181, 0, -6181),
        new CaseRange (0x1C88, 0x1C88, 35266, 0, 35266),
        new CaseRange (0x1C90, 0x1CBA, 0, -3008, 0),
        new CaseRange (0x1CBD, 0x1CBF, 0, -3008, 0),
        new CaseRange (0x1D79, 0x1D79, 35332, 0, 35332),
        new CaseRange (0x1D7D, 0x1D7D, 3814, 0, 3814),
        new CaseRange (0x1D8E, 0x1D8E, 35384, 0, 35384),
        new CaseRange (0x1E00, 0x1E95, UpperLower, UpperLower, UpperLower),
        new CaseRange (0x1E9B, 0x1E9B, -59, 0, -59),
        new CaseRange (0x1E9E, 0x1E9E, 0, -7615, 0),
        new CaseRange (0x1EA0, 0x1EFF, UpperLower, UpperLower, UpperLower),
        new CaseRange (0x1F00, 0x1F07, 8, 0, 8),
        new CaseRange (0x1F08, 0x1F0F, 0, -8, 0),
        new CaseRange (0x1F10, 0x1F15, 8, 0, 8),
        new CaseRange (0x1F18, 0x1F1D, 0, -8, 0),
        new CaseRange (0x1F20, 0x1F27, 8, 0, 8),
        new CaseRange (0x1F28, 0x1F2F, 0, -8, 0),
        new CaseRange (0x1F30, 0x1F37, 8, 0, 8),
        new CaseRange (0x1F38, 0x1F3F, 0, -8, 0),
        new CaseRange (0x1F40, 0x1F45, 8, 0, 8),
        new CaseRange (0x1F48, 0x1F4D, 0, -8, 0),
        new CaseRange (0x1F51, 0x1F51, 8, 0, 8),
        new CaseRange (0x1F53, 0x1F53, 8, 0, 8),
        new CaseRange (0x1F55, 0x1F55, 8, 0, 8),
        new CaseRange (0x1F57, 0x1F57, 8, 0, 8),
        new CaseRange (0x1F59, 0x1F59, 0, -8, 0),
        new CaseRange (0x1F5B, 0x1F5B, 0, -8, 0),
        new CaseRange (0x1F5D, 0x1F5D, 0, -8, 0),
        new CaseRange (0x1F5F, 0x1F5F, 0, -8, 0),
        new CaseRange (0x1F60, 0x1F67, 8, 0, 8),
        new CaseRange (0x1F68, 0x1F6F, 0, -8, 0),
        new CaseRange (0x1F70, 0x1F71, 74, 0, 74),
        new CaseRange (0x1F72, 0x1F75, 86, 0, 86),
        new CaseRange (0x1F76, 0x1F77, 100, 0, 100),
        new CaseRange (0x1F78, 0x1F79, 128, 0, 128),
        new CaseRange (0x1F7A, 0x1F7B, 112, 0, 112),
        new CaseRange (0x1F7C, 0x1F7D, 126, 0, 126),
        new CaseRange (0x1F80, 0x1F87, 8, 0, 8),
        new CaseRange (0x1F88, 0x1F8F, 0, -8, 0),
        new CaseRange (0x1F90, 0x1F97, 8, 0, 8),
        new CaseRange (0x1F98, 0x1F9F, 0, -8, 0),
        new CaseRange (0x1FA0, 0x1FA7, 8, 0, 8),
        new CaseRange (0x1FA8, 0x1FAF, 0, -8, 0),
        new CaseRange (0x1FB0, 0x1FB1, 8, 0, 8),
        new CaseRange (0x1FB3, 0x1FB3, 9, 0, 9),
        new CaseRange (0x1FB8, 0x1FB9, 0, -8, 0),
        new CaseRange (0x1FBA, 0x1FBB, 0, -74, 0),
        new CaseRange (0x1FBC, 0x1FBC, 0, -9, 0),
        new CaseRange (0x1FBE, 0x1FBE, -7205, 0, -7205),
        new CaseRange (0x1FC3, 0x1FC3, 9, 0, 9),
        new CaseRange (0x1FC8, 0x1FCB, 0, -86, 0),
        new CaseRange (0x1FCC, 0x1FCC, 0, -9, 0),
        new CaseRange (0x1FD0, 0x1FD1, 8, 0, 8),
        new CaseRange (0x1FD8, 0x1FD9, 0, -8, 0),
        new CaseRange (0x1FDA, 0x1FDB, 0, -100, 0),
        new CaseRange (0x1FE0, 0x1FE1, 8, 0, 8),
        new CaseRange (0x1FE5, 0x1FE5, 7, 0, 7),
        new CaseRange (0x1FE8, 0x1FE9, 0, -8, 0),
        new CaseRange (0x1FEA, 0x1FEB, 0, -112, 0),
        new CaseRange (0x1FEC, 0x1FEC, 0, -7, 0),
        new CaseRange (0x1FF3, 0x1FF3, 9, 0, 9),
        new CaseRange (0x1FF8, 0x1FF9, 0, -128, 0),
        new CaseRange (0x1FFA, 0x1FFB, 0, -126, 0),
        new CaseRange (0x1FFC, 0x1FFC, 0, -9, 0),
        new CaseRange (0x2126, 0x2126, 0, -7517, 0),
        new CaseRange (0x212A, 0x212A, 0, -8383, 0),
        new CaseRange (0x212B, 0x212B, 0, -8262, 0),
        new CaseRange (0x2132, 0x2132, 0, 28, 0),
        new CaseRange (0x214E, 0x214E, -28, 0, -28),
        new CaseRange (0x2160, 0x216F, 0, 16, 0),
        new CaseRange (0x2170, 0x217F, -16, 0, -16),
        new CaseRange (0x2183, 0x2184, UpperLower, UpperLower, UpperLower),
        new CaseRange (0x24B6, 0x24CF, 0, 26, 0),
        new CaseRange (0x24D0, 0x24E9, -26, 0, -26),
        new CaseRange (0x2C00, 0x2C2F, 0, 48, 0),
        new CaseRange (0x2C30, 0x2C5F, -48, 0, -48),
        new CaseRange (0x2C60, 0x2C61, UpperLower, UpperLower, UpperLower),
        new CaseRange (0x2C62, 0x2C62, 0, -10743, 0),
        new CaseRange (0x2C63, 0x2C63, 0, -3814, 0),
        new CaseRange (0x2C64, 0x2C64, 0, -10727, 0),
        new CaseRange (0x2C65, 0x2C65, -10795, 0, -10795),
        new CaseRange (0x2C66, 0x2C66, -10792, 0, -10792),
        new CaseRange (0x2C67, 0x2C6C, UpperLower, UpperLower, UpperLower),
        new CaseRange (0x2C6D, 0x2C6D, 0, -10780, 0),
        new CaseRange (0x2C6E, 0x2C6E, 0, -10749, 0),
        new CaseRange (0x2C6F, 0x2C6F, 0, -10783, 0),
        new CaseRange (0x2C70, 0x2C70, 0, -10782, 0),
        new CaseRange (0x2C72, 0x2C73, UpperLower, UpperLower, UpperLower),
        new CaseRange (0x2C75, 0x2C76, UpperLower, UpperLower, UpperLower),
        new CaseRange (0x2C7E, 0x2C7F, 0, -10815, 0),
        new CaseRange (0x2C80, 0x2CE3, UpperLower, UpperLower, UpperLower),
        new CaseRange (0x2CEB, 0x2CEE, UpperLower, UpperLower, UpperLower),
        new CaseRange (0x2CF2, 0x2CF3, UpperLower, UpperLower, UpperLower),
        new CaseRange (0x2D00, 0x2D25, -7264, 0, -7264),
        new CaseRange (0x2D27, 0x2D27, -7264, 0, -7264),
        new CaseRange (0x2D2D, 0x2D2D, -7264, 0, -7264),
        new CaseRange (0xA640, 0xA66D, UpperLower, UpperLower, UpperLower),
        new CaseRange (0xA680, 0xA69B, UpperLower, UpperLower, UpperLower),
        new CaseRange (0xA722, 0xA72F, UpperLower, UpperLower, UpperLower),
        new CaseRange (0xA732, 0xA76F, UpperLower, UpperLower, UpperLower),
        new CaseRange (0xA779, 0xA77C, UpperLower, UpperLower, UpperLower),
        new CaseRange (0xA77D, 0xA77D, 0, -35332, 0),
        new CaseRange (0xA77E, 0xA787, UpperLower, UpperLower, UpperLower),
        new CaseRange (0xA78B, 0xA78C, UpperLower, UpperLower, UpperLower),
        new CaseRange (0xA78D, 0xA78D, 0, -42280, 0),
        new CaseRange (0xA790, 0xA793, UpperLower, UpperLower, UpperLower),
        new CaseRange (0xA794, 0xA794, 48, 0, 48),
        new CaseRange (0xA796, 0xA7A9, UpperLower, UpperLower, UpperLower),
        new CaseRange (0xA7AA, 0xA7AA, 0, -42308, 0),
        new CaseRange (0xA7AB, 0xA7AB, 0, -42319, 0),
        new CaseRange (0xA7AC, 0xA7AC, 0, -42315, 0),
        new CaseRange (0xA7AD, 0xA7AD, 0, -42305, 0),
        new CaseRange (0xA7AE, 0xA7AE, 0, -42308, 0),
        new CaseRange (0xA7B0, 0xA7B0, 0, -42258, 0),
        new CaseRange (0xA7B1, 0xA7B1, 0, -42282, 0),
        new CaseRange (0xA7B2, 0xA7B2, 0, -42261, 0),
        new CaseRange (0xA7B3, 0xA7B3, 0, 928, 0),
        new CaseRange (0xA7B4, 0xA7C3, UpperLower, UpperLower, UpperLower),
        new CaseRange (0xA7C4, 0xA7C4, 0, -48, 0),
        new CaseRange (0xA7C5, 0xA7C5, 0, -42307, 0),
        new CaseRange (0xA7C6, 0xA7C6, 0, -35384, 0),
        new CaseRange (0xA7C7, 0xA7CA, UpperLower, UpperLower, UpperLower),
        new CaseRange (0xA7D0, 0xA7D1, UpperLower, UpperLower, UpperLower),
        new CaseRange (0xA7D6, 0xA7D9, UpperLower, UpperLower, UpperLower),
        new CaseRange (0xA7F5, 0xA7F6, UpperLower, UpperLower, UpperLower),
        new CaseRange (0xAB53, 0xAB53, -928, 0, -928),
        new CaseRange (0xAB70, 0xABBF, -38864, 0, -38864),
        new CaseRange (0xFF21, 0xFF3A, 0, 32, 0),
        new CaseRange (0xFF41, 0xFF5A, -32, 0, -32),
        new CaseRange (0x10400, 0x10427, 0, 40, 0),
        new CaseRange (0x10428, 0x1044F, -40, 0, -40),
        new CaseRange (0x104B0, 0x104D3, 0, 40, 0),
        new CaseRange (0x104D8, 0x104FB, -40, 0, -40),
        new CaseRange (0x10570, 0x1057A, 0, 39, 0),
        new CaseRange (0x1057C, 0x1058A, 0, 39, 0),
        new CaseRange (0x1058C, 0x10592, 0, 39, 0),
        new CaseRange (0x10594, 0x10595, 0, 39, 0),
        new CaseRange (0x10597, 0x105A1, -39, 0, -39),
        new CaseRange (0x105A3, 0x105B1, -39, 0, -39),
        new CaseRange (0x105B3, 0x105B9, -39, 0, -39),
        new CaseRange (0x105BB, 0x105BC, -39, 0, -39),
        new CaseRange (0x10C80, 0x10CB2, 0, 64, 0),
        new CaseRange (0x10CC0, 0x10CF2, -64, 0, -64),
        new CaseRange (0x118A0, 0x118BF, 0, 32, 0),
        new CaseRange (0x118C0, 0x118DF, -32, 0, -32),
        new CaseRange (0x16E40, 0x16E5F, 0, 32, 0),
        new CaseRange (0x16E60, 0x16E7F, -32, 0, -32),
        new CaseRange (0x1E900, 0x1E921, 0, 34, 0),
        new CaseRange (0x1E922, 0x1E943, -34, 0, -34),
    };
        static CharClass[] properties = new CharClass[256] {
		/*0x00 */ CharClass.pC, // '\x00'
		/*0x01 */ CharClass.pC, // '\x01'
		/*0x02 */ CharClass.pC, // '\x02'
		/*0x03 */ CharClass.pC, // '\x03'
		/*0x04 */ CharClass.pC, // '\x04'
		/*0x05 */ CharClass.pC, // '\x05'
		/*0x06 */ CharClass.pC, // '\x06'
		/*0x07 */ CharClass.pC, // '\a'
		/*0x08 */ CharClass.pC, // '\b'
		/*0x09 */ CharClass.pC, // '\t'
		/*0x0A */ CharClass.pC, // '\n'
		/*0x0B */ CharClass.pC, // '\v'
		/*0x0C */ CharClass.pC, // '\f'
		/*0x0D */ CharClass.pC, // '\r'
		/*0x0E */ CharClass.pC, // '\x0e'
		/*0x0F */ CharClass.pC, // '\x0f'
		/*0x10 */ CharClass.pC, // '\x10'
		/*0x11 */ CharClass.pC, // '\x11'
		/*0x12 */ CharClass.pC, // '\x12'
		/*0x13 */ CharClass.pC, // '\x13'
		/*0x14 */ CharClass.pC, // '\x14'
		/*0x15 */ CharClass.pC, // '\x15'
		/*0x16 */ CharClass.pC, // '\x16'
		/*0x17 */ CharClass.pC, // '\x17'
		/*0x18 */ CharClass.pC, // '\x18'
		/*0x19 */ CharClass.pC, // '\x19'
		/*0x1A */ CharClass.pC, // '\x1a'
		/*0x1B */ CharClass.pC, // '\x1b'
		/*0x1C */ CharClass.pC, // '\x1c'
		/*0x1D */ CharClass.pC, // '\x1d'
		/*0x1E */ CharClass.pC, // '\x1e'
		/*0x1F */ CharClass.pC, // '\x1f'
		/*0x20 */ CharClass.pZ | CharClass.pp, // ' '
		/*0x21 */ CharClass.pP | CharClass.pp, // '!'
		/*0x22 */ CharClass.pP | CharClass.pp, // '"'
		/*0x23 */ CharClass.pP | CharClass.pp, // '#'
		/*0x24 */ CharClass.pS | CharClass.pp, // '$'
		/*0x25 */ CharClass.pP | CharClass.pp, // '%'
		/*0x26 */ CharClass.pP | CharClass.pp, // '&'
		/*0x27 */ CharClass.pP | CharClass.pp, // '\''
		/*0x28 */ CharClass.pP | CharClass.pp, // '('
		/*0x29 */ CharClass.pP | CharClass.pp, // ')'
		/*0x2A */ CharClass.pP | CharClass.pp, // '*'
		/*0x2B */ CharClass.pS | CharClass.pp, // '+'
		/*0x2C */ CharClass.pP | CharClass.pp, // ','
		/*0x2D */ CharClass.pP | CharClass.pp, // '-'
		/*0x2E */ CharClass.pP | CharClass.pp, // '.'
		/*0x2F */ CharClass.pP | CharClass.pp, // '/'
		/*0x30 */ CharClass.pN | CharClass.pp, // '0'
		/*0x31 */ CharClass.pN | CharClass.pp, // '1'
		/*0x32 */ CharClass.pN | CharClass.pp, // '2'
		/*0x33 */ CharClass.pN | CharClass.pp, // '3'
		/*0x34 */ CharClass.pN | CharClass.pp, // '4'
		/*0x35 */ CharClass.pN | CharClass.pp, // '5'
		/*0x36 */ CharClass.pN | CharClass.pp, // '6'
		/*0x37 */ CharClass.pN | CharClass.pp, // '7'
		/*0x38 */ CharClass.pN | CharClass.pp, // '8'
		/*0x39 */ CharClass.pN | CharClass.pp, // '9'
		/*0x3A */ CharClass.pP | CharClass.pp, // ':'
		/*0x3B */ CharClass.pP | CharClass.pp, // ';'
		/*0x3C */ CharClass.pS | CharClass.pp, // '<'
		/*0x3D */ CharClass.pS | CharClass.pp, // '='
		/*0x3E */ CharClass.pS | CharClass.pp, // '>'
		/*0x3F */ CharClass.pP | CharClass.pp, // '?'
		/*0x40 */ CharClass.pP | CharClass.pp, // '@'
		/*0x41 */ CharClass.pLu | CharClass.pp, // 'A'
		/*0x42 */ CharClass.pLu | CharClass.pp, // 'B'
		/*0x43 */ CharClass.pLu | CharClass.pp, // 'C'
		/*0x44 */ CharClass.pLu | CharClass.pp, // 'D'
		/*0x45 */ CharClass.pLu | CharClass.pp, // 'E'
		/*0x46 */ CharClass.pLu | CharClass.pp, // 'F'
		/*0x47 */ CharClass.pLu | CharClass.pp, // 'G'
		/*0x48 */ CharClass.pLu | CharClass.pp, // 'H'
		/*0x49 */ CharClass.pLu | CharClass.pp, // 'I'
		/*0x4A */ CharClass.pLu | CharClass.pp, // 'J'
		/*0x4B */ CharClass.pLu | CharClass.pp, // 'K'
		/*0x4C */ CharClass.pLu | CharClass.pp, // 'L'
		/*0x4D */ CharClass.pLu | CharClass.pp, // 'M'
		/*0x4E */ CharClass.pLu | CharClass.pp, // 'N'
		/*0x4F */ CharClass.pLu | CharClass.pp, // 'O'
		/*0x50 */ CharClass.pLu | CharClass.pp, // 'P'
		/*0x51 */ CharClass.pLu | CharClass.pp, // 'Q'
		/*0x52 */ CharClass.pLu | CharClass.pp, // 'R'
		/*0x53 */ CharClass.pLu | CharClass.pp, // 'S'
		/*0x54 */ CharClass.pLu | CharClass.pp, // 'T'
		/*0x55 */ CharClass.pLu | CharClass.pp, // 'U'
		/*0x56 */ CharClass.pLu | CharClass.pp, // 'V'
		/*0x57 */ CharClass.pLu | CharClass.pp, // 'W'
		/*0x58 */ CharClass.pLu | CharClass.pp, // 'X'
		/*0x59 */ CharClass.pLu | CharClass.pp, // 'Y'
		/*0x5A */ CharClass.pLu | CharClass.pp, // 'Z'
		/*0x5B */ CharClass.pP | CharClass.pp, // '['
		/*0x5C */ CharClass.pP | CharClass.pp, // '\\'
		/*0x5D */ CharClass.pP | CharClass.pp, // ']'
		/*0x5E */ CharClass.pS | CharClass.pp, // '^'
		/*0x5F */ CharClass.pP | CharClass.pp, // '_'
		/*0x60 */ CharClass.pS | CharClass.pp, // '`'
		/*0x61 */ CharClass.pLl | CharClass.pp, // 'a'
		/*0x62 */ CharClass.pLl | CharClass.pp, // 'b'
		/*0x63 */ CharClass.pLl | CharClass.pp, // 'c'
		/*0x64 */ CharClass.pLl | CharClass.pp, // 'd'
		/*0x65 */ CharClass.pLl | CharClass.pp, // 'e'
		/*0x66 */ CharClass.pLl | CharClass.pp, // 'f'
		/*0x67 */ CharClass.pLl | CharClass.pp, // 'g'
		/*0x68 */ CharClass.pLl | CharClass.pp, // 'h'
		/*0x69 */ CharClass.pLl | CharClass.pp, // 'i'
		/*0x6A */ CharClass.pLl | CharClass.pp, // 'j'
		/*0x6B */ CharClass.pLl | CharClass.pp, // 'k'
		/*0x6C */ CharClass.pLl | CharClass.pp, // 'l'
		/*0x6D */ CharClass.pLl | CharClass.pp, // 'm'
		/*0x6E */ CharClass.pLl | CharClass.pp, // 'n'
		/*0x6F */ CharClass.pLl | CharClass.pp, // 'o'
		/*0x70 */ CharClass.pLl | CharClass.pp, // 'p'
		/*0x71 */ CharClass.pLl | CharClass.pp, // 'q'
		/*0x72 */ CharClass.pLl | CharClass.pp, // 'r'
		/*0x73 */ CharClass.pLl | CharClass.pp, // 's'
		/*0x74 */ CharClass.pLl | CharClass.pp, // 't'
		/*0x75 */ CharClass.pLl | CharClass.pp, // 'u'
		/*0x76 */ CharClass.pLl | CharClass.pp, // 'v'
		/*0x77 */ CharClass.pLl | CharClass.pp, // 'w'
		/*0x78 */ CharClass.pLl | CharClass.pp, // 'x'
		/*0x79 */ CharClass.pLl | CharClass.pp, // 'y'
		/*0x7A */ CharClass.pLl | CharClass.pp, // 'z'
		/*0x7B */ CharClass.pP | CharClass.pp, // '{'
		/*0x7C */ CharClass.pS | CharClass.pp, // '|'
		/*0x7D */ CharClass.pP | CharClass.pp, // '}'
		/*0x7E */ CharClass.pS | CharClass.pp, // '~'
		/*0x7F */ CharClass.pC, // '\u007f'
		/*0x80 */ CharClass.pC, // '\u0080'
		/*0x81 */ CharClass.pC, // '\u0081'
		/*0x82 */ CharClass.pC, // '\u0082'
		/*0x83 */ CharClass.pC, // '\u0083'
		/*0x84 */ CharClass.pC, // '\u0084'
		/*0x85 */ CharClass.pC, // '\u0085'
		/*0x86 */ CharClass.pC, // '\u0086'
		/*0x87 */ CharClass.pC, // '\u0087'
		/*0x88 */ CharClass.pC, // '\u0088'
		/*0x89 */ CharClass.pC, // '\u0089'
		/*0x8A */ CharClass.pC, // '\u008a'
		/*0x8B */ CharClass.pC, // '\u008b'
		/*0x8C */ CharClass.pC, // '\u008c'
		/*0x8D */ CharClass.pC, // '\u008d'
		/*0x8E */ CharClass.pC, // '\u008e'
		/*0x8F */ CharClass.pC, // '\u008f'
		/*0x90 */ CharClass.pC, // '\u0090'
		/*0x91 */ CharClass.pC, // '\u0091'
		/*0x92 */ CharClass.pC, // '\u0092'
		/*0x93 */ CharClass.pC, // '\u0093'
		/*0x94 */ CharClass.pC, // '\u0094'
		/*0x95 */ CharClass.pC, // '\u0095'
		/*0x96 */ CharClass.pC, // '\u0096'
		/*0x97 */ CharClass.pC, // '\u0097'
		/*0x98 */ CharClass.pC, // '\u0098'
		/*0x99 */ CharClass.pC, // '\u0099'
		/*0x9A */ CharClass.pC, // '\u009a'
		/*0x9B */ CharClass.pC, // '\u009b'
		/*0x9C */ CharClass.pC, // '\u009c'
		/*0x9D */ CharClass.pC, // '\u009d'
		/*0x9E */ CharClass.pC, // '\u009e'
		/*0x9F */ CharClass.pC, // '\u009f'
		/*0xA0 */ CharClass.pZ, // '\u00a0'
		/*0xA1 */ CharClass.pP | CharClass.pp, // '¡'
		/*0xA2 */ CharClass.pS | CharClass.pp, // '¢'
		/*0xA3 */ CharClass.pS | CharClass.pp, // '£'
		/*0xA4 */ CharClass.pS | CharClass.pp, // '¤'
		/*0xA5 */ CharClass.pS | CharClass.pp, // '¥'
		/*0xA6 */ CharClass.pS | CharClass.pp, // '¦'
		/*0xA7 */ CharClass.pP | CharClass.pp, // '§'
		/*0xA8 */ CharClass.pS | CharClass.pp, // '¨'
		/*0xA9 */ CharClass.pS | CharClass.pp, // '©'
		/*0xAA */ CharClass.pLo | CharClass.pp, // 'ª'
		/*0xAB */ CharClass.pP | CharClass.pp, // '«'
		/*0xAC */ CharClass.pS | CharClass.pp, // '¬'
		/*0xAD */ 0, // '\u00ad'
		/*0xAE */ CharClass.pS | CharClass.pp, // '®'
		/*0xAF */ CharClass.pS | CharClass.pp, // '¯'
		/*0xB0 */ CharClass.pS | CharClass.pp, // '°'
		/*0xB1 */ CharClass.pS | CharClass.pp, // '±'
		/*0xB2 */ CharClass.pN | CharClass.pp, // '²'
		/*0xB3 */ CharClass.pN | CharClass.pp, // '³'
		/*0xB4 */ CharClass.pS | CharClass.pp, // '´'
		/*0xB5 */ CharClass.pLl | CharClass.pp, // 'µ'
		/*0xB6 */ CharClass.pP | CharClass.pp, // '¶'
		/*0xB7 */ CharClass.pP | CharClass.pp, // '·'
		/*0xB8 */ CharClass.pS | CharClass.pp, // '¸'
		/*0xB9 */ CharClass.pN | CharClass.pp, // '¹'
		/*0xBA */ CharClass.pLo | CharClass.pp, // 'º'
		/*0xBB */ CharClass.pP | CharClass.pp, // '»'
		/*0xBC */ CharClass.pN | CharClass.pp, // '¼'
		/*0xBD */ CharClass.pN | CharClass.pp, // '½'
		/*0xBE */ CharClass.pN | CharClass.pp, // '¾'
		/*0xBF */ CharClass.pP | CharClass.pp, // '¿'
		/*0xC0 */ CharClass.pLu | CharClass.pp, // 'À'
		/*0xC1 */ CharClass.pLu | CharClass.pp, // 'Á'
		/*0xC2 */ CharClass.pLu | CharClass.pp, // 'Â'
		/*0xC3 */ CharClass.pLu | CharClass.pp, // 'Ã'
		/*0xC4 */ CharClass.pLu | CharClass.pp, // 'Ä'
		/*0xC5 */ CharClass.pLu | CharClass.pp, // 'Å'
		/*0xC6 */ CharClass.pLu | CharClass.pp, // 'Æ'
		/*0xC7 */ CharClass.pLu | CharClass.pp, // 'Ç'
		/*0xC8 */ CharClass.pLu | CharClass.pp, // 'È'
		/*0xC9 */ CharClass.pLu | CharClass.pp, // 'É'
		/*0xCA */ CharClass.pLu | CharClass.pp, // 'Ê'
		/*0xCB */ CharClass.pLu | CharClass.pp, // 'Ë'
		/*0xCC */ CharClass.pLu | CharClass.pp, // 'Ì'
		/*0xCD */ CharClass.pLu | CharClass.pp, // 'Í'
		/*0xCE */ CharClass.pLu | CharClass.pp, // 'Î'
		/*0xCF */ CharClass.pLu | CharClass.pp, // 'Ï'
		/*0xD0 */ CharClass.pLu | CharClass.pp, // 'Ð'
		/*0xD1 */ CharClass.pLu | CharClass.pp, // 'Ñ'
		/*0xD2 */ CharClass.pLu | CharClass.pp, // 'Ò'
		/*0xD3 */ CharClass.pLu | CharClass.pp, // 'Ó'
		/*0xD4 */ CharClass.pLu | CharClass.pp, // 'Ô'
		/*0xD5 */ CharClass.pLu | CharClass.pp, // 'Õ'
		/*0xD6 */ CharClass.pLu | CharClass.pp, // 'Ö'
		/*0xD7 */ CharClass.pS | CharClass.pp, // '×'
		/*0xD8 */ CharClass.pLu | CharClass.pp, // 'Ø'
		/*0xD9 */ CharClass.pLu | CharClass.pp, // 'Ù'
		/*0xDA */ CharClass.pLu | CharClass.pp, // 'Ú'
		/*0xDB */ CharClass.pLu | CharClass.pp, // 'Û'
		/*0xDC */ CharClass.pLu | CharClass.pp, // 'Ü'
		/*0xDD */ CharClass.pLu | CharClass.pp, // 'Ý'
		/*0xDE */ CharClass.pLu | CharClass.pp, // 'Þ'
		/*0xDF */ CharClass.pLl | CharClass.pp, // 'ß'
		/*0xE0 */ CharClass.pLl | CharClass.pp, // 'à'
		/*0xE1 */ CharClass.pLl | CharClass.pp, // 'á'
		/*0xE2 */ CharClass.pLl | CharClass.pp, // 'â'
		/*0xE3 */ CharClass.pLl | CharClass.pp, // 'ã'
		/*0xE4 */ CharClass.pLl | CharClass.pp, // 'ä'
		/*0xE5 */ CharClass.pLl | CharClass.pp, // 'å'
		/*0xE6 */ CharClass.pLl | CharClass.pp, // 'æ'
		/*0xE7 */ CharClass.pLl | CharClass.pp, // 'ç'
		/*0xE8 */ CharClass.pLl | CharClass.pp, // 'è'
		/*0xE9 */ CharClass.pLl | CharClass.pp, // 'é'
		/*0xEA */ CharClass.pLl | CharClass.pp, // 'ê'
		/*0xEB */ CharClass.pLl | CharClass.pp, // 'ë'
		/*0xEC */ CharClass.pLl | CharClass.pp, // 'ì'
		/*0xED */ CharClass.pLl | CharClass.pp, // 'í'
		/*0xEE */ CharClass.pLl | CharClass.pp, // 'î'
		/*0xEF */ CharClass.pLl | CharClass.pp, // 'ï'
		/*0xF0 */ CharClass.pLl | CharClass.pp, // 'ð'
		/*0xF1 */ CharClass.pLl | CharClass.pp, // 'ñ'
		/*0xF2 */ CharClass.pLl | CharClass.pp, // 'ò'
		/*0xF3 */ CharClass.pLl | CharClass.pp, // 'ó'
		/*0xF4 */ CharClass.pLl | CharClass.pp, // 'ô'
		/*0xF5 */ CharClass.pLl | CharClass.pp, // 'õ'
		/*0xF6 */ CharClass.pLl | CharClass.pp, // 'ö'
		/*0xF7 */ CharClass.pS | CharClass.pp, // '÷'
		/*0xF8 */ CharClass.pLl | CharClass.pp, // 'ø'
		/*0xF9 */ CharClass.pLl | CharClass.pp, // 'ù'
		/*0xFA */ CharClass.pLl | CharClass.pp, // 'ú'
		/*0xFB */ CharClass.pLl | CharClass.pp, // 'û'
		/*0xFC */ CharClass.pLl | CharClass.pp, // 'ü'
		/*0xFD */ CharClass.pLl | CharClass.pp, // 'ý'
		/*0xFE */ CharClass.pLl | CharClass.pp, // 'þ'
		/*0xFF */ CharClass.pLl | CharClass.pp, // 'ÿ'
	};

        static ushort[] asciiFold = new ushort[128]{
        0x0000,
        0x0001,
        0x0002,
        0x0003,
        0x0004,
        0x0005,
        0x0006,
        0x0007,
        0x0008,
        0x0009,
        0x000A,
        0x000B,
        0x000C,
        0x000D,
        0x000E,
        0x000F,
        0x0010,
        0x0011,
        0x0012,
        0x0013,
        0x0014,
        0x0015,
        0x0016,
        0x0017,
        0x0018,
        0x0019,
        0x001A,
        0x001B,
        0x001C,
        0x001D,
        0x001E,
        0x001F,
        0x0020,
        0x0021,
        0x0022,
        0x0023,
        0x0024,
        0x0025,
        0x0026,
        0x0027,
        0x0028,
        0x0029,
        0x002A,
        0x002B,
        0x002C,
        0x002D,
        0x002E,
        0x002F,
        0x0030,
        0x0031,
        0x0032,
        0x0033,
        0x0034,
        0x0035,
        0x0036,
        0x0037,
        0x0038,
        0x0039,
        0x003A,
        0x003B,
        0x003C,
        0x003D,
        0x003E,
        0x003F,
        0x0040,
        0x0061,
        0x0062,
        0x0063,
        0x0064,
        0x0065,
        0x0066,
        0x0067,
        0x0068,
        0x0069,
        0x006A,
        0x006B,
        0x006C,
        0x006D,
        0x006E,
        0x006F,
        0x0070,
        0x0071,
        0x0072,
        0x0073,
        0x0074,
        0x0075,
        0x0076,
        0x0077,
        0x0078,
        0x0079,
        0x007A,
        0x005B,
        0x005C,
        0x005D,
        0x005E,
        0x005F,
        0x0060,
        0x0041,
        0x0042,
        0x0043,
        0x0044,
        0x0045,
        0x0046,
        0x0047,
        0x0048,
        0x0049,
        0x004A,
        0x212A,
        0x004C,
        0x004D,
        0x004E,
        0x004F,
        0x0050,
        0x0051,
        0x0052,
        0x017F,
        0x0054,
        0x0055,
        0x0056,
        0x0057,
        0x0058,
        0x0059,
        0x005A,
        0x007B,
        0x007C,
        0x007D,
        0x007E,
        0x007F,
    };

        static FoldPair[] CaseOrbit = new FoldPair[] {
        new FoldPair (0x004B, 0x006B),
        new FoldPair (0x0053, 0x0073),
        new FoldPair (0x006B, 0x212A),
        new FoldPair (0x0073, 0x017F),
        new FoldPair (0x00B5, 0x039C),
        new FoldPair (0x00C5, 0x00E5),
        new FoldPair (0x00DF, 0x1E9E),
        new FoldPair (0x00E5, 0x212B),
        new FoldPair (0x0130, 0x0130),
        new FoldPair (0x0131, 0x0131),
        new FoldPair (0x017F, 0x0053),
        new FoldPair (0x01C4, 0x01C5),
        new FoldPair (0x01C5, 0x01C6),
        new FoldPair (0x01C6, 0x01C4),
        new FoldPair (0x01C7, 0x01C8),
        new FoldPair (0x01C8, 0x01C9),
        new FoldPair (0x01C9, 0x01C7),
        new FoldPair (0x01CA, 0x01CB),
        new FoldPair (0x01CB, 0x01CC),
        new FoldPair (0x01CC, 0x01CA),
        new FoldPair (0x01F1, 0x01F2),
        new FoldPair (0x01F2, 0x01F3),
        new FoldPair (0x01F3, 0x01F1),
        new FoldPair (0x0345, 0x0399),
        new FoldPair (0x0392, 0x03B2),
        new FoldPair (0x0395, 0x03B5),
        new FoldPair (0x0398, 0x03B8),
        new FoldPair (0x0399, 0x03B9),
        new FoldPair (0x039A, 0x03BA),
        new FoldPair (0x039C, 0x03BC),
        new FoldPair (0x03A0, 0x03C0),
        new FoldPair (0x03A1, 0x03C1),
        new FoldPair (0x03A3, 0x03C2),
        new FoldPair (0x03A6, 0x03C6),
        new FoldPair (0x03A9, 0x03C9),
        new FoldPair (0x03B2, 0x03D0),
        new FoldPair (0x03B5, 0x03F5),
        new FoldPair (0x03B8, 0x03D1),
        new FoldPair (0x03B9, 0x1FBE),
        new FoldPair (0x03BA, 0x03F0),
        new FoldPair (0x03BC, 0x00B5),
        new FoldPair (0x03C0, 0x03D6),
        new FoldPair (0x03C1, 0x03F1),
        new FoldPair (0x03C2, 0x03C3),
        new FoldPair (0x03C3, 0x03A3),
        new FoldPair (0x03C6, 0x03D5),
        new FoldPair (0x03C9, 0x2126),
        new FoldPair (0x03D0, 0x0392),
        new FoldPair (0x03D1, 0x03F4),
        new FoldPair (0x03D5, 0x03A6),
        new FoldPair (0x03D6, 0x03A0),
        new FoldPair (0x03F0, 0x039A),
        new FoldPair (0x03F1, 0x03A1),
        new FoldPair (0x03F4, 0x0398),
        new FoldPair (0x03F5, 0x0395),
        new FoldPair (0x0412, 0x0432),
        new FoldPair (0x0414, 0x0434),
        new FoldPair (0x041E, 0x043E),
        new FoldPair (0x0421, 0x0441),
        new FoldPair (0x0422, 0x0442),
        new FoldPair (0x042A, 0x044A),
        new FoldPair (0x0432, 0x1C80),
        new FoldPair (0x0434, 0x1C81),
        new FoldPair (0x043E, 0x1C82),
        new FoldPair (0x0441, 0x1C83),
        new FoldPair (0x0442, 0x1C84),
        new FoldPair (0x044A, 0x1C86),
        new FoldPair (0x0462, 0x0463),
        new FoldPair (0x0463, 0x1C87),
        new FoldPair (0x1C80, 0x0412),
        new FoldPair (0x1C81, 0x0414),
        new FoldPair (0x1C82, 0x041E),
        new FoldPair (0x1C83, 0x0421),
        new FoldPair (0x1C84, 0x1C85),
        new FoldPair (0x1C85, 0x0422),
        new FoldPair (0x1C86, 0x042A),
        new FoldPair (0x1C87, 0x0462),
        new FoldPair (0x1C88, 0xA64A),
        new FoldPair (0x1E60, 0x1E61),
        new FoldPair (0x1E61, 0x1E9B),
        new FoldPair (0x1E9B, 0x1E60),
        new FoldPair (0x1E9E, 0x00DF),
        new FoldPair (0x1FBE, 0x0345),
        new FoldPair (0x2126, 0x03A9),
        new FoldPair (0x212A, 0x004B),
        new FoldPair (0x212B, 0x00C5),
        new FoldPair (0xA64A, 0xA64B),
        new FoldPair (0xA64B, 0x1C88),
    }; /* CaseOrbit */

        // FoldCategory maps a category name to a table of
        // code points outside the category that are equivalent under
        // simple case folding to code points inside the category.
        // If there is no entry for a category name, there are no such points.
        static Dictionary<string, RangeTable> FoldCategory = new Dictionary<string, RangeTable>() {
         { "Common", foldCommon },
         { "Greek", foldGreek },
         { "Inherited", foldInherited },
         { "L", foldL },
         { "Ll", foldLl },
         { "Lt", foldLt },
         { "Lu", foldLu },
         { "M", foldM },
         { "Mn", foldMn },
    };

        internal static RangeTable foldCommon = new RangeTable(
            r16: new Range16[] {
            new Range16 (0x039c, 0x03bc, 32),
            }
        );

        internal static RangeTable foldGreek = new RangeTable(
            r16: new Range16[] {
            new Range16 (0x00b5, 0x0345, 656),
            }
        );

        internal static RangeTable foldInherited = new RangeTable(
            r16: new Range16[] {
            new Range16 (0x0399, 0x03b9, 32),
            new Range16 (0x1fbe, 0x1fbe, 1),
            }
        );

        internal static RangeTable foldL = new RangeTable(
            r16: new Range16[] {
            new Range16 (0x0345, 0x0345, 1),
            }
        );

        internal static RangeTable foldLl = new RangeTable(
            r16: new Range16[] {
            new Range16 (0x0041, 0x005a, 1),
            new Range16 (0x00c0, 0x00d6, 1),
            new Range16 (0x00d8, 0x00de, 1),
            new Range16 (0x0100, 0x012e, 2),
            new Range16 (0x0132, 0x0136, 2),
            new Range16 (0x0139, 0x0147, 2),
            new Range16 (0x014a, 0x0178, 2),
            new Range16 (0x0179, 0x017d, 2),
            new Range16 (0x0181, 0x0182, 1),
            new Range16 (0x0184, 0x0186, 2),
            new Range16 (0x0187, 0x0189, 2),
            new Range16 (0x018a, 0x018b, 1),
            new Range16 (0x018e, 0x0191, 1),
            new Range16 (0x0193, 0x0194, 1),
            new Range16 (0x0196, 0x0198, 1),
            new Range16 (0x019c, 0x019d, 1),
            new Range16 (0x019f, 0x01a0, 1),
            new Range16 (0x01a2, 0x01a6, 2),
            new Range16 (0x01a7, 0x01a9, 2),
            new Range16 (0x01ac, 0x01ae, 2),
            new Range16 (0x01af, 0x01b1, 2),
            new Range16 (0x01b2, 0x01b3, 1),
            new Range16 (0x01b5, 0x01b7, 2),
            new Range16 (0x01b8, 0x01bc, 4),
            new Range16 (0x01c4, 0x01c5, 1),
            new Range16 (0x01c7, 0x01c8, 1),
            new Range16 (0x01ca, 0x01cb, 1),
            new Range16 (0x01cd, 0x01db, 2),
            new Range16 (0x01de, 0x01ee, 2),
            new Range16 (0x01f1, 0x01f2, 1),
            new Range16 (0x01f4, 0x01f6, 2),
            new Range16 (0x01f7, 0x01f8, 1),
            new Range16 (0x01fa, 0x0232, 2),
            new Range16 (0x023a, 0x023b, 1),
            new Range16 (0x023d, 0x023e, 1),
            new Range16 (0x0241, 0x0243, 2),
            new Range16 (0x0244, 0x0246, 1),
            new Range16 (0x0248, 0x024e, 2),
            new Range16 (0x0345, 0x0370, 43),
            new Range16 (0x0372, 0x0376, 4),
            new Range16 (0x037f, 0x0386, 7),
            new Range16 (0x0388, 0x038a, 1),
            new Range16 (0x038c, 0x038e, 2),
            new Range16 (0x038f, 0x0391, 2),
            new Range16 (0x0392, 0x03a1, 1),
            new Range16 (0x03a3, 0x03ab, 1),
            new Range16 (0x03cf, 0x03d8, 9),
            new Range16 (0x03da, 0x03ee, 2),
            new Range16 (0x03f4, 0x03f7, 3),
            new Range16 (0x03f9, 0x03fa, 1),
            new Range16 (0x03fd, 0x042f, 1),
            new Range16 (0x0460, 0x0480, 2),
            new Range16 (0x048a, 0x04c0, 2),
            new Range16 (0x04c1, 0x04cd, 2),
            new Range16 (0x04d0, 0x052e, 2),
            new Range16 (0x0531, 0x0556, 1),
            new Range16 (0x10a0, 0x10c5, 1),
            new Range16 (0x10c7, 0x10cd, 6),
            new Range16 (0x13a0, 0x13f5, 1),
            new Range16 (0x1c90, 0x1cba, 1),
            new Range16 (0x1cbd, 0x1cbf, 1),
            new Range16 (0x1e00, 0x1e94, 2),
            new Range16 (0x1e9e, 0x1efe, 2),
            new Range16 (0x1f08, 0x1f0f, 1),
            new Range16 (0x1f18, 0x1f1d, 1),
            new Range16 (0x1f28, 0x1f2f, 1),
            new Range16 (0x1f38, 0x1f3f, 1),
            new Range16 (0x1f48, 0x1f4d, 1),
            new Range16 (0x1f59, 0x1f5f, 2),
            new Range16 (0x1f68, 0x1f6f, 1),
            new Range16 (0x1f88, 0x1f8f, 1),
            new Range16 (0x1f98, 0x1f9f, 1),
            new Range16 (0x1fa8, 0x1faf, 1),
            new Range16 (0x1fb8, 0x1fbc, 1),
            new Range16 (0x1fc8, 0x1fcc, 1),
            new Range16 (0x1fd8, 0x1fdb, 1),
            new Range16 (0x1fe8, 0x1fec, 1),
            new Range16 (0x1ff8, 0x1ffc, 1),
            new Range16 (0x2126, 0x212a, 4),
            new Range16 (0x212b, 0x2132, 7),
            new Range16 (0x2183, 0x2c00, 2685),
            new Range16 (0x2c01, 0x2c2f, 1),
            new Range16 (0x2c60, 0x2c62, 2),
            new Range16 (0x2c63, 0x2c64, 1),
            new Range16 (0x2c67, 0x2c6d, 2),
            new Range16 (0x2c6e, 0x2c70, 1),
            new Range16 (0x2c72, 0x2c75, 3),
            new Range16 (0x2c7e, 0x2c80, 1),
            new Range16 (0x2c82, 0x2ce2, 2),
            new Range16 (0x2ceb, 0x2ced, 2),
            new Range16 (0x2cf2, 0xa640, 31054),
            new Range16 (0xa642, 0xa66c, 2),
            new Range16 (0xa680, 0xa69a, 2),
            new Range16 (0xa722, 0xa72e, 2),
            new Range16 (0xa732, 0xa76e, 2),
            new Range16 (0xa779, 0xa77d, 2),
            new Range16 (0xa77e, 0xa786, 2),
            new Range16 (0xa78b, 0xa78d, 2),
            new Range16 (0xa790, 0xa792, 2),
            new Range16 (0xa796, 0xa7aa, 2),
            new Range16 (0xa7ab, 0xa7ae, 1),
            new Range16 (0xa7b0, 0xa7b4, 1),
            new Range16 (0xa7b6, 0xa7c4, 2),
            new Range16 (0xa7c5, 0xa7c7, 1),
            new Range16 (0xa7c9, 0xa7d0, 7),
            new Range16 (0xa7d6, 0xa7d8, 2),
            new Range16 (0xa7f5, 0xff21, 22316),
            new Range16 (0xff22, 0xff3a, 1),
            },
            r32: new Range32[] {
            new Range32 (0x10400, 0x10427, 1),
            new Range32 (0x104b0, 0x104d3, 1),
            new Range32 (0x10570, 0x1057a, 1),
            new Range32 (0x1057c, 0x1058a, 1),
            new Range32 (0x1058c, 0x10592, 1),
            new Range32 (0x10594, 0x10595, 1),
            new Range32 (0x10c80, 0x10cb2, 1),
            new Range32 (0x118a0, 0x118bf, 1),
            new Range32 (0x16e40, 0x16e5f, 1),
            new Range32 (0x1e900, 0x1e921, 1),
            },
            latinOffset: 3
        );

        internal static RangeTable foldLt = new RangeTable(
            r16: new Range16[] {
            new Range16 (0x01c4, 0x01c6, 2),
            new Range16 (0x01c7, 0x01c9, 2),
            new Range16 (0x01ca, 0x01cc, 2),
            new Range16 (0x01f1, 0x01f3, 2),
            new Range16 (0x1f80, 0x1f87, 1),
            new Range16 (0x1f90, 0x1f97, 1),
            new Range16 (0x1fa0, 0x1fa7, 1),
            new Range16 (0x1fb3, 0x1fc3, 16),
            new Range16 (0x1ff3, 0x1ff3, 1),
            }
        );

        internal static RangeTable foldLu = new RangeTable(
            r16: new Range16[] {
            new Range16 (0x0061, 0x007a, 1),
            new Range16 (0x00b5, 0x00df, 42),
            new Range16 (0x00e0, 0x00f6, 1),
            new Range16 (0x00f8, 0x00ff, 1),
            new Range16 (0x0101, 0x012f, 2),
            new Range16 (0x0133, 0x0137, 2),
            new Range16 (0x013a, 0x0148, 2),
            new Range16 (0x014b, 0x0177, 2),
            new Range16 (0x017a, 0x017e, 2),
            new Range16 (0x017f, 0x0180, 1),
            new Range16 (0x0183, 0x0185, 2),
            new Range16 (0x0188, 0x018c, 4),
            new Range16 (0x0192, 0x0195, 3),
            new Range16 (0x0199, 0x019a, 1),
            new Range16 (0x019e, 0x01a1, 3),
            new Range16 (0x01a3, 0x01a5, 2),
            new Range16 (0x01a8, 0x01ad, 5),
            new Range16 (0x01b0, 0x01b4, 4),
            new Range16 (0x01b6, 0x01b9, 3),
            new Range16 (0x01bd, 0x01bf, 2),
            new Range16 (0x01c5, 0x01c6, 1),
            new Range16 (0x01c8, 0x01c9, 1),
            new Range16 (0x01cb, 0x01cc, 1),
            new Range16 (0x01ce, 0x01dc, 2),
            new Range16 (0x01dd, 0x01ef, 2),
            new Range16 (0x01f2, 0x01f3, 1),
            new Range16 (0x01f5, 0x01f9, 4),
            new Range16 (0x01fb, 0x021f, 2),
            new Range16 (0x0223, 0x0233, 2),
            new Range16 (0x023c, 0x023f, 3),
            new Range16 (0x0240, 0x0242, 2),
            new Range16 (0x0247, 0x024f, 2),
            new Range16 (0x0250, 0x0254, 1),
            new Range16 (0x0256, 0x0257, 1),
            new Range16 (0x0259, 0x025b, 2),
            new Range16 (0x025c, 0x0260, 4),
            new Range16 (0x0261, 0x0265, 2),
            new Range16 (0x0266, 0x0268, 2),
            new Range16 (0x0269, 0x026c, 1),
            new Range16 (0x026f, 0x0271, 2),
            new Range16 (0x0272, 0x0275, 3),
            new Range16 (0x027d, 0x0280, 3),
            new Range16 (0x0282, 0x0283, 1),
            new Range16 (0x0287, 0x028c, 1),
            new Range16 (0x0292, 0x029d, 11),
            new Range16 (0x029e, 0x0345, 167),
            new Range16 (0x0371, 0x0373, 2),
            new Range16 (0x0377, 0x037b, 4),
            new Range16 (0x037c, 0x037d, 1),
            new Range16 (0x03ac, 0x03af, 1),
            new Range16 (0x03b1, 0x03ce, 1),
            new Range16 (0x03d0, 0x03d1, 1),
            new Range16 (0x03d5, 0x03d7, 1),
            new Range16 (0x03d9, 0x03ef, 2),
            new Range16 (0x03f0, 0x03f3, 1),
            new Range16 (0x03f5, 0x03fb, 3),
            new Range16 (0x0430, 0x045f, 1),
            new Range16 (0x0461, 0x0481, 2),
            new Range16 (0x048b, 0x04bf, 2),
            new Range16 (0x04c2, 0x04ce, 2),
            new Range16 (0x04cf, 0x052f, 2),
            new Range16 (0x0561, 0x0586, 1),
            new Range16 (0x10d0, 0x10fa, 1),
            new Range16 (0x10fd, 0x10ff, 1),
            new Range16 (0x13f8, 0x13fd, 1),
            new Range16 (0x1c80, 0x1c88, 1),
            new Range16 (0x1d79, 0x1d7d, 4),
            new Range16 (0x1d8e, 0x1e01, 115),
            new Range16 (0x1e03, 0x1e95, 2),
            new Range16 (0x1e9b, 0x1ea1, 6),
            new Range16 (0x1ea3, 0x1eff, 2),
            new Range16 (0x1f00, 0x1f07, 1),
            new Range16 (0x1f10, 0x1f15, 1),
            new Range16 (0x1f20, 0x1f27, 1),
            new Range16 (0x1f30, 0x1f37, 1),
            new Range16 (0x1f40, 0x1f45, 1),
            new Range16 (0x1f51, 0x1f57, 2),
            new Range16 (0x1f60, 0x1f67, 1),
            new Range16 (0x1f70, 0x1f7d, 1),
            new Range16 (0x1fb0, 0x1fb1, 1),
            new Range16 (0x1fbe, 0x1fd0, 18),
            new Range16 (0x1fd1, 0x1fe0, 15),
            new Range16 (0x1fe1, 0x1fe5, 4),
            new Range16 (0x214e, 0x2184, 54),
            new Range16 (0x2c30, 0x2c5f, 1),
            new Range16 (0x2c61, 0x2c65, 4),
            new Range16 (0x2c66, 0x2c6c, 2),
            new Range16 (0x2c73, 0x2c76, 3),
            new Range16 (0x2c81, 0x2ce3, 2),
            new Range16 (0x2cec, 0x2cee, 2),
            new Range16 (0x2cf3, 0x2d00, 13),
            new Range16 (0x2d01, 0x2d25, 1),
            new Range16 (0x2d27, 0x2d2d, 6),
            new Range16 (0xa641, 0xa66d, 2),
            new Range16 (0xa681, 0xa69b, 2),
            new Range16 (0xa723, 0xa72f, 2),
            new Range16 (0xa733, 0xa76f, 2),
            new Range16 (0xa77a, 0xa77c, 2),
            new Range16 (0xa77f, 0xa787, 2),
            new Range16 (0xa78c, 0xa791, 5),
            new Range16 (0xa793, 0xa794, 1),
            new Range16 (0xa797, 0xa7a9, 2),
            new Range16 (0xa7b5, 0xa7c3, 2),
            new Range16 (0xa7c8, 0xa7ca, 2),
            new Range16 (0xa7d1, 0xa7d7, 6),
            new Range16 (0xa7d9, 0xa7f6, 29),
            new Range16 (0xab53, 0xab70, 29),
            new Range16 (0xab71, 0xabbf, 1),
            new Range16 (0xff41, 0xff5a, 1),
            },
            r32: new Range32[] {
            new Range32 (0x10428, 0x1044f, 1),
            new Range32 (0x104d8, 0x104fb, 1),
            new Range32 (0x10597, 0x105a1, 1),
            new Range32 (0x105a3, 0x105b1, 1),
            new Range32 (0x105b3, 0x105b9, 1),
            new Range32 (0x105bb, 0x105bc, 1),
            new Range32 (0x10cc0, 0x10cf2, 1),
            new Range32 (0x118c0, 0x118df, 1),
            new Range32 (0x16e60, 0x16e7f, 1),
            new Range32 (0x1e922, 0x1e943, 1),
            },
            latinOffset: 4
        );

        internal static RangeTable foldM = new RangeTable(
            r16: new Range16[] {
            new Range16 (0x0399, 0x03b9, 32),
            new Range16 (0x1fbe, 0x1fbe, 1),
            }
        );

        internal static RangeTable foldMn = new RangeTable(
            r16: new Range16[] {
            new Range16 (0x0399, 0x03b9, 32),
            new Range16 (0x1fbe, 0x1fbe, 1),
            }
        );

        // FoldScript maps a script name to a table of
        // code points outside the script that are equivalent under
        // simple case folding to code points inside the script.
        // If there is no entry for a script name, there are no such points.
        static Dictionary<string, RangeTable> FoldScript = new Dictionary<string, RangeTable>()
        {
        };

        // Range entries: 3675 16-bit, 2081 32-bit, 5756 total.
        // Range bytes: 22050 16-bit, 24972 32-bit, 47022 total.

        // Fold orbit bytes: 88 pairs, 352 bytes
    } /* partial class Unicode */
    //=======================================================================
    // ustring.cs: UTF8 String representation
    //
    // Based on the Go strings code
    // 
    // C# ification by Miguel de Icaza
    //
    // TODO:
    //   Fields
    // 
    // TODO from .NET API:
    // String.Split members (array of strings, StringSplitOptions)
    // Replace, should it allow nulls?
    // Generally: what to do with null parameters, that we can avoid exceptions and produce a good result.

    /// <summary>
    /// ustrings are used to manipulate utf8 strings, either from byte arrays or blocks of memory.
    /// </summary>
    /// <remarks>
    /// <para>
    ///   The ustring provides a series of string-like operations over an array of bytes.   The buffer
    ///   is expected to contain an UTF8 encoded string, but if the buffer contains an invalid utf8
    ///   sequence many of the operations will continue to work.
    /// </para>
    /// <para>
    ///   The strings can be created either from byte arrays, a range within a byte array, or from a 
    ///   block of unmanaged memory.  The ustrings are created using one of the Make or MakeCopy methods 
    ///   in the class, not by invoking the new operator on the class.
    /// </para>
    /// <para>
    /// <list type="table">
    ///   <listheader>
    ///     <term>Method</term>
    ///     <term>Description</term>
    ///   </listheader>
    ///   <item>
    ///     <term><see cref="M:NStack.ustring.Make(string)"/></term>
    ///     <description>Creates a ustring from a C# string.</description>
    ///   </item>
    ///   <item>
    ///     <term><see cref="M:NStack.ustring.Make(byte[])"/></term>
    ///     <description>Creates a ustring from a byte array.</description>
    ///   </item>
    ///   <item>
    ///     <term><see cref="M:NStack.ustring.Make(byte[],int,int)"/></term>
    ///     <description>Creates a ustring from a range in a byte array.</description>
    ///   </item>
    ///   <item>
    ///     <term><see cref="M:NStack.ustring.Make(uint[])"/></term>
    ///     <description>Creates a ustring from a single rune.</description>
    ///   </item>
    ///   <item>
    ///     <term><see cref="M:NStack.ustring.Make(char[])"/></term>
    ///     <description>Creates a ustring from a character array.</description>
    ///   </item>
    ///   <item>
    ///     <term><see cref="M:NStack.ustring.Make(System.IntPtr,int,System.Action{System.IntPtr})"/></term>
    ///     <description>Creates a ustring from an unmanaged memory block, with an optional method to invoke to release the block when the ustring is garbage collected.</description>
    ///   </item>
    ///   <item>
    ///     <term><see cref="M:NStack.ustring.Make(System.IntPtr,System.Action{System.IntPtr})"/></term>
    ///     <description>
    ///       Creates a ustring from an unmanaged memory block that is null-terminated, suitable for interoperability with C APIs.   
    ///       It takes an optional method to invoke to release the block when the ustring is garbage collected.
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <term><see cref="M:NStack.ustring.MakeCopy(System.IntPtr,int)"/></term>
    ///     <description>Creates a ustring by making a copy of the provided memory block.</description>
    ///   </item>
    ///   <item>
    ///     <term><see cref="M:NStack.ustring.MakeCopy(System.IntPtr)"/></term>
    ///     <description>
    ///       Creates a ustring by making a copy of the null-terminated memory block.   Suitable for interoperability with C APIs.   
    ///     </description>
    ///   </item>
    /// </list>
    /// </para>
    /// <para>
    ///   The Length property describes the length in bytes of the underlying array, while the RuneCount 
    ///   property describes the number of code points (or runes) that are represented by the underlying 
    ///   utf8 encoded buffer.
    /// </para>
    /// <para>
    ///   The ustring supports slicing by calling the indexer with two arguments, the argument represent
    ///   indexes into the underlying byte buffer.  The starting index is inclusive, while the ending index
    ///   is exclusive.   Negative values can be used to index the string from the end.  See the documentation
    ///   for the indexer for more details.
    /// </para>
    /// 
    /// </remarks>
    public abstract class ustring : IComparable<ustring>, IComparable, IConvertible, IEnumerable<uint>, IEquatable<ustring>
#if NETSTANDARD2_0
	, ICloneable
#endif
    {

        // The ustring subclass that supports creating strings for an IntPtr+Size pair.
        class IntPtrUString : ustring, IDisposable
        {
            internal IntPtr block;
            readonly int size;
            bool copy;
            Action<IntPtr> release;

            class IntPtrSubUString : IntPtrUString
            {
                IntPtrUString retain;

                public IntPtrSubUString(IntPtrUString retain, IntPtr block, int size) : base(block, size, copy: false, releaseFunc: null)
                {
                    this.retain = retain;
                }

                protected override void Dispose(bool disposing)
                {
                    base.Dispose(disposing);
                    retain = null;
                }

            }

            unsafe static int MeasureString(IntPtr block)
            {
                byte* p = (byte*)block;
                while (*p != 0)
                    p++;
                return (int)(p - ((byte*)block));
            }

            public IntPtrUString(IntPtr block, bool copy, Action<IntPtr> releaseFunc = null) : this(block, MeasureString(block), copy, releaseFunc)
            {
            }

            public IntPtrUString(IntPtr block, int size, bool copy, Action<IntPtr> releaseFunc = null)
            {
                if (block == IntPtr.Zero)
                    throw new ArgumentException("Null pointer passed", nameof(block));
                if (size < 0)
                    throw new ArgumentException("Invalid size passed", nameof(size));
                this.size = size;

                this.copy = copy;
                if (copy)
                {
                    this.release = null;
                    if (size == 0)
                        size = 1;
                    this.block = Marshal.AllocHGlobal(size);
                    unsafe
                    {
                        Buffer.MemoryCopy((void*)block, (void*)this.block, size, size);
                    }
                }
                else
                {
                    this.block = block;
                    this.release = releaseFunc;
                }
            }

            public override int Length => size;
            public override byte this[int index]
            {
                get
                {
                    if (index < 0 || index > size)
                        throw new ArgumentException(nameof(index));
                    return Marshal.ReadByte(block, index);
                }
            }

            public override void CopyTo(int fromOffset, byte[] target, int targetOffset, int count)
            {
                if (fromOffset < 0 || fromOffset >= size)
                    throw new ArgumentException(nameof(fromOffset));
                if (count < 0)
                    throw new ArgumentException(nameof(count));
                if (fromOffset + count > size)
                    throw new ArgumentException(nameof(count));
                unsafe
                {
                    byte* p = ((byte*)block) + fromOffset;

                    for (int i = 0; i < count; i++, p++)
                        target[i] = *p;
                }
            }

            public override string ToString()
            {
                unsafe
                {
                    return Encoding.UTF8.GetString((byte*)block, size);
                }
            }

            protected internal override ustring GetRange(int start, int end)
            {
                unsafe
                {
                    return new IntPtrSubUString(this, (IntPtr)((byte*)block + start), size: end - start);
                }
            }

            internal override int RealIndexByte(byte b, int offset)
            {
                var t = size - offset;
                unsafe
                {
                    byte* p = (byte*)block + offset;
                    for (int i = 0; i < t; i++)
                    {
                        if (p[i] == b)
                            return i + offset;
                    }
                }
                return -1;
            }

            public override byte[] ToByteArray()
            {
                var copy = new byte[size];
                Marshal.Copy(block, copy, 0, size);
                return copy;
            }

            #region IDisposable Support
            protected virtual void Dispose(bool disposing)
            {
                if (block != IntPtr.Zero)
                {
                    if (copy)
                    {
                        Marshal.FreeHGlobal(block);
                    }
                    else if (release != null)
                        release(block);
                    release = null;
                    block = IntPtr.Zero;
                }
            }

            ~IntPtrUString()
            {
                Dispose(false);
            }

            // This code added to correctly implement the disposable pattern.
            void IDisposable.Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            #endregion
        }

        // The ustring implementation that is implemented on top of a byte buffer.
        class ByteBufferUString : ustring
        {
            internal readonly byte[] buffer;

            public ByteBufferUString(byte[] buffer)
            {
                if (buffer == null)
                    throw new ArgumentNullException(nameof(buffer));
                this.buffer = buffer;
            }

            public ByteBufferUString(uint rune)
            {
                var len = Utf8.RuneLen(rune);
                buffer = new byte[len];
                Utf8.EncodeRune(rune, buffer, 0);
            }

            public ByteBufferUString(string str)
            {
                if (str == null)
                    throw new ArgumentNullException(nameof(str));
                buffer = Encoding.UTF8.GetBytes(str);
            }

            public ByteBufferUString(params char[] chars)
            {
                if (chars == null)
                    throw new ArgumentNullException(nameof(chars));
                buffer = Encoding.UTF8.GetBytes(chars);
            }

            public override int Length => buffer.Length;
            public override byte this[int index]
            {
                get
                {
                    return buffer[index];
                }
            }

            public override void CopyTo(int fromOffset, byte[] target, int targetOffset, int count)
            {
                Array.Copy(buffer, fromOffset, target, targetOffset, count);
            }

            public override string ToString()
            {
                return Encoding.UTF8.GetString(buffer);
            }

            internal override int RealIndexByte(byte b, int offset)
            {
                var t = Length - offset;
                unsafe
                {
                    fixed (byte* p = &buffer[offset])
                    {
                        for (int i = 0; i < t; i++)
                            if (p[i] == b)
                                return i + offset;
                    }
                }
                return -1;
            }

            protected internal override ustring GetRange(int start, int end)
            {
                return new ByteRangeUString(buffer, start, end - start);
            }

            public override byte[] ToByteArray()
            {
                return buffer;
            }
        }

        // The ustring implementation that presents a view on an existing byte buffer.
        class ByteRangeUString : ustring
        {
            readonly byte[] buffer;
            readonly int start, count;

            public ByteRangeUString(byte[] buffer, int start, int count)
            {
                if (buffer == null)
                    throw new ArgumentNullException(nameof(buffer));
                if (start < 0)
                    throw new ArgumentException(nameof(start));
                if (count < 0)
                    throw new ArgumentException(nameof(count));
                if (start >= buffer.Length)
                    throw new ArgumentException(nameof(start));
                if (buffer.Length - count < start)
                    throw new ArgumentException(nameof(count));
                this.start = start;
                this.count = count;
                this.buffer = buffer;
            }

            public override int Length => count;
            public override byte this[int index]
            {
                get
                {
                    if (index < 0 || index >= count)
                        throw new ArgumentException(nameof(index));
                    return buffer[start + index];
                }
            }

            public override void CopyTo(int fromOffset, byte[] target, int targetOffset, int count)
            {
                if (fromOffset < 0)
                    throw new ArgumentException(nameof(fromOffset));

                Array.Copy(buffer, fromOffset + start, target, targetOffset, count);
            }

            public override string ToString()
            {
                return Encoding.UTF8.GetString(buffer, start, count);
            }

            internal override int RealIndexByte(byte b, int offset)
            {
                var t = count - offset;
                unsafe
                {
                    fixed (byte* p = &buffer[start + offset])
                    {
                        for (int i = 0; i < t; i++)
                            if (p[i] == b)
                                return i + offset;
                    }
                }
                return -1;
            }

            protected internal override ustring GetRange(int start, int end)
            {
                return new ByteRangeUString(buffer, start + this.start, end - start);
            }

            public override byte[] ToByteArray()
            {
                var copy = new byte[count];
                Array.Copy(buffer, sourceIndex: start, destinationArray: copy, destinationIndex: 0, length: count);
                return copy;
            }
        }

        /// <summary>
        /// The empty ustring.
        /// </summary>
        public static ustring Empty = new ByteBufferUString(Array.Empty<byte>());

        /// <summary>
        /// Initializes a new instance of the <see cref="T:NStack.ustring"/> class using the provided byte array for its storage.
        /// </summary>
        /// <param name="buffer">Buffer containing the utf8 encoded string.</param>
        /// <remarks>
        /// <para>
        ///   No validation is performed on the contents of the byte buffer, so it
        ///   might contains invalid UTF-8 sequences.
        /// </para>
        /// <para>
        ///   No copy is made of the incoming byte buffer, so changes to it will be visible on the ustring.
        /// </para>
        /// </remarks>
        public static ustring Make(params byte[] buffer)
        {
            return new ByteBufferUString(buffer);
        }

        /// <summary>
        /// Initializes a new instance using the provided rune as the sole character in the string.
        /// </summary>
        /// <param name="rune">Rune (short name for Unicode code point).</param>
        public static ustring Make(Rune rune)
        {
            return new ByteBufferUString((uint)rune);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:NStack.ustring"/> class from a string.
        /// </summary>
        /// <param name="str">C# String.</param>
        public static ustring Make(string str)
        {
            return new ByteBufferUString(str);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:NStack.ustring"/> class from an array of C# characters.
        /// </summary>
        /// <param name="chars">Characters.</param>
        public static ustring Make(params char[] chars)
        {
            return new ByteBufferUString(chars);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:NStack.ustring"/> class from an array of Runes.
        /// </summary>
        /// <returns>The make.</returns>
        /// <param name="runes">Runes.</param>
        public static ustring Make(IList<Rune> runes)
        {
            if (runes == null)
                throw new ArgumentNullException(nameof(runes));
            int size = 0;
            foreach (var rune in runes)
            {
                size += Utf8.RuneLen(rune);
            }
            var encoded = new byte[size];
            int offset = 0;
            foreach (var rune in runes)
            {
                offset += Utf8.EncodeRune(rune, encoded, offset);
            }
            return Make(encoded);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:NStack.ustring"/> class from an IEnumerable of runes
        /// </summary>
        /// <returns>The make.</returns>
        /// <param name="runes">Runes.</param>
        public static ustring Make(IEnumerable<Rune> runes)
        {
            return Make(runes.ToList());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:NStack.ustring"/> class from an array of uints, which contain CodePoints.
        /// </summary>
        /// <returns>The make.</returns>
        /// <param name="runes">Runes.</param>
        public static ustring Make(uint[] runes)
        {
            if (runes == null)
                throw new ArgumentNullException(nameof(runes));
            int size = 0;
            foreach (var rune in runes)
            {
                size += Utf8.RuneLen(rune);
            }
            var encoded = new byte[size];
            int offset = 0;
            foreach (var rune in runes)
            {
                offset += Utf8.EncodeRune(rune, encoded, offset);
            }
            return Make(encoded);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:NStack.ustring"/> class from a block of memory and a size.
        /// </summary>
        /// <param name="block">Pointer to a block of memory.</param>
        /// <param name="size">Number of bytes in the block to treat as a string.</param>
        /// <param name="releaseFunc">Optional method to invoke to release when this string is finalized to clear the associated resources, you can use this for example to release the unamanged resource to which the block belongs.</param>
        /// <remarks>
        /// <para>
        ///    This will return a ustring that represents the block of memory provided.
        /// </para>
        /// <para>
        ///   The returned object will be a subclass of ustring that implements IDisposable, which you can use
        ///   to trigger the synchronous execution of the <paramref name="releaseFunc"/>.   If you do not call
        ///   Dispose manually, the provided release function will be invoked from the finalizer thread.
        /// </para>
        /// <para>
        ///   Alternatively, if the block of data is something that you do not own, and you would like
        ///   to make a copy of it, you might want to consider using the <see cref="T:NStack.ustring.MakeCopy(System.IntPtr,int)"/> method.
        /// </para>
        /// </remarks>
        public static ustring Make(IntPtr block, int size, Action<IntPtr> releaseFunc = null)
        {
            return new IntPtrUString(block, size, copy: false, releaseFunc: releaseFunc);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:NStack.ustring"/> class from a null terminated block of memory.
        /// </summary>
        /// <param name="block">Pointer to a block of memory, it is expected to be terminated by a 0 byte.</param>
        /// <param name="releaseFunc">Optional method to invoke to release when this string is finalized to clear the associated resources, you can use this for example to release the unamanged resource to which the block belongs.</param>
        /// <remarks>
        /// <para>
        ///    This will return a ustring that represents the block of memory provided.
        /// </para>
        /// <para>
        ///   The returned object will be a subclass of ustring that implements IDisposable, which you can use
        ///   to trigger the synchronous execution of the <paramref name="releaseFunc"/>.   If you do not call
        ///   Dispose manually, the provided release function will be invoked from the finalizer thread.
        /// </para>
        /// <para>
        ///   Alternatively, if the block of data is something that you do not own, and you would like
        ///   to make a copy of it, you might want to consider using the <see cref="T:NStack.ustring.MakeCopy(System.IntPtr)"/> method.
        /// </para>
        /// </remarks>
        public static ustring Make(IntPtr block, Action<IntPtr> releaseFunc = null)
        {
            return new IntPtrUString(block, copy: false, releaseFunc: releaseFunc);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:NStack.ustring"/> by making a copy of the specified block.
        /// </summary>
        /// <param name="block">Pointer to a block of memory which will be copied into the string.</param>
        /// <param name="size">Number of bytes in the block to treat as a string.</param>
        /// <remarks>
        /// <para>
        ///    This will return a ustring that contains a copy of the buffer pointed to by block.
        /// </para>
        /// <para>
        ///    This is useful when you do not control the lifecycle of the buffer pointed to and
        ///    desire the convenience of a method that makes a copy of the data for you.
        /// </para>
        /// </remarks>
        public static ustring MakeCopy(IntPtr block, int size)
        {
            return new IntPtrUString(block, size, copy: true, releaseFunc: null);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:NStack.ustring"/> by making a copy of the null-terminated block of memory.
        /// </summary>
        /// <param name="block">Pointer to a block of memory, it is expected to be terminated by a 0 byte.</param>
        /// <remarks>
        /// <para>
        ///    This will return a ustring that contains a copy of the zero-terminated buffer pointed to by block.
        /// </para>
        /// <para>
        ///   This is useful to create a string returned from C on a region of memory whose lifecycle
        ///   you do not control, so this will make a private copy of the buffer.
        /// </para>
        /// </remarks>
        public static ustring MakeCopy(IntPtr block)
        {
            return new IntPtrUString(block, copy: true, releaseFunc: null);
        }

        // The low-level version
        unsafe static bool EqualsHelper(byte* a, byte* b, int length)
        {
            // unroll the loop
            // the mono jit will inline the 64-bit check and eliminate the irrelevant path
            if (sizeof(IntPtr) == 8)
            {
                // for AMD64 bit platform we unroll by 12 and
                // check 3 qword at a time. This is less code
                // than the 32 bit case and is shorter
                // pathlength

                while (length >= 12)
                {
                    if (*(long*)a != *(long*)b) return false;
                    if (*(long*)(a + 4) != *(long*)(b + 4)) return false;
                    if (*(long*)(a + 8) != *(long*)(b + 8)) return false;
                    a += 12; b += 12; length -= 12;
                }
            }
            else
            {
                while (length >= 10)
                {
                    if (*(int*)a != *(int*)b) return false;
                    if (*(int*)(a + 2) != *(int*)(b + 2)) return false;
                    if (*(int*)(a + 4) != *(int*)(b + 4)) return false;
                    if (*(int*)(a + 6) != *(int*)(b + 6)) return false;
                    if (*(int*)(a + 8) != *(int*)(b + 8)) return false;
                    a += 10; b += 10; length -= 10;
                }
            }

            while (length > 0)
            {
                if (*a != *b)
                    return false;
                a++;
                b++;
                length--;
            }
            return true;
        }

        // The high-level version parameters have been validated
        static bool EqualsHelper(ustring a, ustring b)
        {
            // If both string are identical, return true.
            if (a.SequenceEqual(b))
            {
                return true;
            }

            var alen = a.Length;
            var blen = b.Length;
            if (alen != blen)
                return false;
            if (alen == 0)
                return true;
            var abs = a as ByteBufferUString;
            var bbs = b as ByteBufferUString;
            if ((object)abs != null && (object)bbs != null)
            {
                unsafe
                {
                    fixed (byte* ap = &abs.buffer[0]) fixed (byte* bp = &bbs.buffer[0])
                    {
                        return EqualsHelper(ap, bp, alen);
                    }
                }
            }
            var aip = a as IntPtrUString;
            var bip = b as IntPtrUString;
            if ((object)aip != null && (object)bip != null)
            {
                unsafe
                {
                    return EqualsHelper((byte*)aip.block, (byte*)bip.block, alen);
                }
            }

            for (int i = 0; i < alen; i++)
                if (a[i] != b[i])
                    return false;
            return true;
        }

        /// <summary>
        /// Determines whether a specified instance of <see cref="NStack.ustring"/> is equal to another specified <see cref="NStack.ustring"/>, this means that the contents of the string are identical
        /// </summary>
        /// <param name="a">The first <see cref="NStack.ustring"/> to compare.</param>
        /// <param name="b">The second <see cref="NStack.ustring"/> to compare.</param>
        /// <returns><c>true</c> if <c>a</c> and <c>b</c> are equal; otherwise, <c>false</c>.</returns>
        public static bool operator ==(ustring a, ustring b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            return EqualsHelper(a, b);
        }

        /// <summary>
        /// Determines whether a specified instance of <see cref="NStack.ustring"/> is not equal to another specified <see cref="NStack.ustring"/>.
        /// </summary>
        /// <param name="a">The first <see cref="NStack.ustring"/> to compare.</param>
        /// <param name="b">The second <see cref="NStack.ustring"/> to compare.</param>
        /// <returns><c>true</c> if <c>a</c> and <c>b</c> are not equal; otherwise, <c>false</c>.</returns>
        public static bool operator !=(ustring a, ustring b)
        {
            // If both are null, or both are same instance, return false
            if (System.Object.ReferenceEquals(a, b))
            {
                return false;
            }

            // If one is null, but not both, return true.
            if (((object)a == null) || ((object)b == null))
            {
                return true;
            }

            return !EqualsHelper(a, b);
        }

        /// <summary>
        /// Implicit conversion from a C# string into a ustring.
        /// </summary>
        /// <returns>The ustring with the same contents as the string.</returns>
        /// <param name="str">The string to encode as a ustring.</param>
        /// <remarks>
        /// This will allocate a byte array and copy the contents of the 
        /// string encoded as UTF8 into it.
        /// </remarks>
        public static implicit operator ustring(string str)
        {
            if (str == null)
                return null;

            return new ByteBufferUString(str);
        }

        /// <summary>
        /// Implicit conversion from a byte array into a ustring.
        /// </summary>
        /// <returns>The ustring wrapping the existing byte array.</returns>
        /// <param name="buffer">The buffer containing the data.</param>
        /// <remarks>
        /// The returned string will keep a reference to the buffer, which 
        /// means that changes done to the buffer will be reflected into the
        /// ustring.
        /// </remarks>
        public static implicit operator ustring(byte[] buffer)
        {
            return new ByteBufferUString(buffer);
        }

        /// <summary>
        /// Serves as a hash function for a <see cref="T:NStack.ustring"/> object.
        /// </summary>
        /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
        public override int GetHashCode()
        {
            return (int)HashStr().hash;
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="T:NStack.ustring"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="T:NStack.ustring"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current <see cref="T:NStack.ustring"/>;
        /// otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            // If parameter is null return false.
            if (obj == null)
                return false;

            // If parameter cannot be cast to Point return false.
            ustring p = obj as ustring;
            if ((object)p == null)
                return false;

            return EqualsHelper(this, p);
        }


        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="T:NStack.ustring"/>.
        /// </summary>
        /// <param name="other">The other string to compare with the current <see cref="T:NStack.ustring"/>.</param>
        /// <returns><c>true</c> if the specified ustring is equal to the current ustring;
        /// otherwise, <c>false</c>.</returns>
        public bool Equals(ustring other)
        {
            // If parameter is null return false.
            if ((object)other == null)
                return false;

            return EqualsHelper(this, other);

        }

        /// <summary>
        /// Reports whether this string and the provided string, when interpreted as UTF-8 strings, are equal under Unicode case-folding
        /// </summary>
        /// <returns><c>true</c>, if fold was equaled, <c>false</c> otherwise.</returns>
        /// <param name="other">Other.</param>
        public bool EqualsFold(ustring other)
        {
            if ((object)other == null)
                return false;

            int slen = Length;
            int tlen = other.Length;

            int soffset = 0, toffset = 0;
            while (soffset < slen && toffset < tlen)
            {
                // Extract first rune of each string
                uint sr, tr;
                int size;

                var rune = this[soffset];
                if (rune < Utf8.RuneSelf)
                {
                    sr = rune;
                    soffset++;
                }
                else
                {
                    (sr, size) = Utf8.DecodeRune(this, soffset);
                    soffset += size;
                }
                rune = other[toffset];
                if (rune < Utf8.RuneSelf)
                {
                    tr = rune;
                    toffset++;
                }
                else
                {
                    (tr, size) = Utf8.DecodeRune(other, toffset);
                    toffset += size;
                }
                // If they match, keep going; if not, return false.
                // Easy case.
                if (tr == sr)
                    continue;
                // Make sr < tr to simplify what follows.
                if (tr < sr)
                {
                    var x = tr;
                    tr = sr;
                    sr = x;
                }
                // Fast check for ascii
                if (tr < Utf8.RuneSelf && 'A' <= sr && sr <= 'Z')
                {
                    // ASCII, and sr is upper case.  tr must be lower case.
                    if (tr == sr + 'a' - 'A')
                        continue;
                    return false;
                }
                // General case. SimpleFold(x) returns the next equivalent rune > x
                // or wraps around to smaller values.
                var r = Unicode.SimpleFold(sr);
                while (r != sr && r < tr)
                {
                    r = Unicode.SimpleFold(r);
                }
                if (r == tr)
                    continue;
                return false;
            }
            return (soffset == Length && toffset == other.Length);
        }

        /// <summary>
        /// The Copy method makes a copy of the underlying data, it can be used to release the resources associated with an
        /// unmanaged buffer, or a ranged string.
        /// </summary>
        /// <returns>A copy of the underlying data.</returns>
        public ustring Copy()
        {
            return new ByteBufferUString(this.ToByteArray());
        }

#if NETSTANDARD2_0
		object ICloneable.Clone ()
		{
			return Copy ();
		}
#endif
        /// <summary>
        /// Initializes a new instance of the <see cref="T:NStack.ustring"/> class from a byte array.
        /// </summary>
        /// <param name="buffer">Buffer containing the utf8 encoded string.</param>
        /// <param name="start">Starting offset into the buffer.</param>
        /// <param name="count">Number of bytes to consume from the buffer.</param>
        /// <remarks>
        /// <para>
        /// No validation is performed on the contents of the byte buffer, so it
        /// might contains invalid UTF-8 sequences.
        /// </para>
        /// <para>
        /// This will make a copy of the buffer range.
        /// </para>
        /// </remarks>
        public static ustring Make(byte[] buffer, int start, int count)
        {
            return new ByteRangeUString(buffer, start, count);
        }

        /// <summary>
        /// Gets the length in bytes of the byte buffer.
        /// </summary>
        /// <value>The length in bytes of the encoded UTF8 string, does not represent the number of runes.</value>
        /// <remarks>To obtain the number of runes in the string, use the <see cref="P:System.ustring.RuneCount"/> property.</remarks>
        public abstract int Length { get; }

        /// <summary>
        /// Returns the byte at the specified position.
        /// </summary>
        /// <value>The byte encoded at the specified position.</value>
        /// <remarks>The index value should be between 0 and Length-1.</remarks>
        public abstract byte this[int index] { get; }

        /// <summary>
        /// For internal use, returns the range of bytes specified.
        /// </summary>
        /// <returns>The range.</returns>
        /// <param name="start">Start.</param>
        /// <param name="end">End.</param>
        protected internal abstract ustring GetRange(int start, int end);

        /// <summary>
        /// Returns a slice of the ustring delimited by the [start, end) range.  If the range is invalid, the return is the Empty string.
        /// </summary>
        /// <param name="start">Start index, this value is inclusive.   If the value is negative, the value is added to the length, allowing this parameter to count to count from the end of the string.</param>
        /// <param name="end">End index, this value is exclusive.   If the value is negative, the value is added to the length, plus one, allowing this parameter to count from the end of the string.</param>
        /// <remarks>
        /// <para>
        /// Some examples given the string "1234567890":
        /// </para>
        /// <para>The range [0, 4] produces "1234"</para>
        /// <para>The range [8, 10] produces "90"</para>
        /// <para>The range [8, null] produces "90"</para>
        /// <para>The range [-2, null] produces "90"</para>
        /// <para>The range [8, 9] produces "9"</para>
        /// <para>The range [-4, -1] produces "789"</para>
        /// <para>The range [-4, null] produces "7890"</para>
        /// <para>The range [-4, null] produces "7890"</para>
        /// <para>The range [-9, -3] produces "234567"</para>
        /// <para>The range [0, 0] produces the empty string</para>
        /// <para>
        ///   This indexer does not raise exceptions for invalid indexes, instead the value 
        ///   returned is the ustring.Empty value:
        /// </para>
        /// <para>
        ///   The range [100, 200] produces the ustring.Empty
        /// </para>
        /// <para>
        ///   The range [-100, 0] produces ustring.Empty
        /// </para>
        /// <para>
        ///   To simulate the optional end boundary, use the indexer that takes the
        ///   object parameter and pass a null to it.   For example, to fetch all
        ///   elements from the position five until the end, use [5, null]
        /// </para>
        /// </remarks>
        public ustring this[int start, int end]
        {
            get
            {
                int size = Length;
                if (end < 0)
                    end = size + end;

                if (start < 0)
                    start = size + start;

                if (start < 0 || start >= size || start >= end)
                    return Empty;
                if (end < 0 || end > size)
                    return Empty;
                return GetRange(start, end);
            }
        }

        /// <summary>
        /// Returns a slice of the ustring delimited by the [start, last-element-of-the-string range.  If the range is invalid, the return is the Empty string.
        /// </summary>
        /// <param name="start">Byte start index, this value is inclusive.   If the value is negative, the value is added to the length, allowing this parameter to count to count from the end of the string.</param>
        /// <param name="end">Byte end index.  This value is expected to be null to indicate that it should be the last element of the string.</param>
        /// <remarks>
        /// <para>
        /// This is a companion indexer to the indexer that takes two integers, it only exists
        /// to provide the optional end argument to mean "until the end", and to make the code
        /// that uses indexer look familiar, without having to resort to another API.
        /// 
        /// Some examples given the string "1234567890":
        /// </para>
        /// <para>
        ///   The indexes are byte indexes, they are not rune indexes.
        /// </para>
        /// <para>The range [8, null] produces "90"</para>
        /// <para>The range [-2, null] produces "90"</para>
        /// <para>The range [8, 9] produces "9"</para>
        /// <para>The range [-4, -1] produces "789"</para>
        /// <para>The range [-4, null] produces "7890"</para>
        /// <para>The range [-4, null] produces "7890"</para>
        /// <para>The range [-9, -3] produces "234567"</para>
        /// <para>
        ///   This indexer does not raise exceptions for invalid indexes, instead the value 
        ///   returned is the ustring.Empty value:
        /// </para>
        /// <para>
        ///   The range [100, 200] produces the ustring.Empty
        /// </para>
        /// <para>
        ///   The range [-100, 0] produces ustring.Empty
        /// </para>
        /// <para>
        ///   To simulate the optional end boundary, use the indexer that takes the
        ///   object parameter and pass a null to it.   For example, to fetch all
        ///   elements from the position five until the end, use [5, null]
        /// </para>
        /// </remarks>
        public ustring this[int start, object end]
        {
            get
            {
                int size = Length;
                int iend = size;
                if (start < 0)
                    start = size + start;

                if (start < 0 || start >= size || start >= iend)
                    return Empty;
                if (iend < 0 || iend > size)
                    return Empty;
                return GetRange(start, iend);
            }
        }

        /// <summary>
        /// Returns the substring starting at the given position in bytes from the origin of the Utf8 string.   
        /// Use RuneSubstring to extract substrings based on the rune index, rather than the byte index inside the
        /// Utf8 encoded string.
        /// </summary>
        /// <returns>The substring starting at the specified offset.</returns>
        /// <param name="byteStart">Starting point, default value is 0.</param>
        /// <param name="length">The substring length.</param>
        public ustring Substring(int byteStart, int length = 0)
        {
            if (length <= 0)
                length = Length - byteStart;
            if (byteStart < 0)
                byteStart = 0;
            return GetRange(byteStart, byteStart + length);
        }

        /// <summary>
        /// Returns the substring starting at the given position in rune index from the origin of the Utf8 string.
        /// </summary>
        /// <returns>The substring starting at the specified offset.</returns>
        /// <param name="runeStart">Starting point, default value is 0.</param>
        /// <param name="length">The substring length.</param>
        public ustring RuneSubstring(int runeStart, int length = 0)
        {
            if (length <= 0)
                length = Length - runeStart;
            if (runeStart < 0)
                runeStart = 0;

            var runes = this.ToRunes();
            ustring usRange = "";
            for (int i = runeStart; i < runeStart + length; i++)
            {
                usRange += ustring.Make(runes[i]);
            }
            return usRange;
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:NStack.ustring"/> is empty.
        /// </summary>
        /// <value><c>true</c> if is empty (Length is zero); otherwise, <c>false</c>.</value>
        public bool IsEmpty => Length == 0;

        /// <summary>
        /// Gets the rune count of the string.
        /// </summary>
        /// <value>The rune count.</value>
        public int RuneCount => Utf8.RuneCount(this);

        /// <summary>
        /// Returns the number of columns used by the unicode string on console applications. 
        /// This is done by calling the Rune.ColumnWidth or zero, if it's negative, on each rune.
        /// </summary>
        public int ConsoleWidth
        {
            get
            {
                int total = 0;
                int blen = Length;
                for (int i = 0; i < blen;)
                {
                    (var rune, var size) = Utf8.DecodeRune(this, i, i - blen);
                    i += size;
                    total += Math.Max(Rune.ColumnWidth(rune), 0);
                }
                return total;
            }
        }

        /// <summary>
        /// Copies the specified number of bytes from the underlying ustring representation to the target array at the specified offset.
        /// </summary>
        /// <param name="fromOffset">Offset in the underlying ustring buffer to copy from.</param>
        /// <param name="target">Target array where the buffer contents will be copied to.</param>
        /// <param name="targetOffset">Offset into the target array where this will be copied to.</param>
        /// <param name="count">Number of bytes to copy.</param>
        public abstract void CopyTo(int fromOffset, byte[] target, int targetOffset, int count);

        /// <summary>
        /// Returns a version of the ustring as a byte array, it might allocate or return the internal byte buffer, depending on the backing implementation.
        /// </summary>
        /// <returns>A byte array containing the contents of the ustring.</returns>
        /// <remarks>
        /// The byte array contains either a copy of the underlying data, in the cases where the ustring was created
        /// from an unmanaged pointer or when the ustring was created by either slicing or from a range withing a byte
        /// array.   Otherwise the returned array that is used by the ustring itself.
        /// </remarks>
        public abstract byte[] ToByteArray();

        /// <summary>
        /// Concatenates the provided ustrings into a new ustring.
        /// </summary>
        /// <returns>A new ustring that contains the concatenation of all the ustrings.</returns>
        /// <param name="args">One or more ustrings.</param>
        public static ustring Concat(params ustring[] args)
        {
            if (args == null)
                throw new ArgumentNullException(nameof(args));
            var t = 0;
            foreach (var x in args)
                t += x.Length;
            var copy = new byte[t];
            int p = 0;

            foreach (var x in args)
            {
                var n = x.Length;
                x.CopyTo(fromOffset: 0, target: copy, targetOffset: p, count: n);
                p += n;
            }
            return new ByteBufferUString(copy);
        }

        /// <summary>
        /// Explode splits the string into a slice of UTF-8 strings
        /// </summary>
        /// <returns>, one string per unicode character, 
        /// up to the specified limit.</returns>
        /// <param name="limit">Maximum number of entries to return, or -1 for no limits.</param>
        public ustring[] Explode(int limit = -1)
        {
            var n = Utf8.RuneCount(this);
            if (limit < 0 || n > limit)
                limit = n;
            var result = new ustring[limit];
            int offset = 0;
            for (int i = 0; i < limit - 1; i++)
            {
                (var rune, var size) = Utf8.DecodeRune(this, offset);
                if (rune == Utf8.RuneError)
                    result[i] = new ByteBufferUString(Utf8.RuneError);
                else
                {
                    var substr = new byte[size];

                    CopyTo(fromOffset: offset, target: substr, targetOffset: 0, count: size);
                    result[i] = new ByteBufferUString(substr);
                }
                offset += size;
            }
            if (n > 0)
            {
                var r = new byte[Length - offset];

                CopyTo(fromOffset: offset, target: r, targetOffset: 0, count: Length - offset);
                result[n - 1] = new ByteBufferUString(r);
            }
            return result;
        }

        /// <summary>
        /// Converts a ustring into a rune array.
        /// </summary>
        /// <returns>An array containing the runes for the string up to the specified limit.</returns>
        /// <param name="limit">Maximum number of entries to return, or -1 for no limits.</param>
        public uint[] ToRunes(int limit = -1)
        {
            var n = Utf8.RuneCount(this);
            if (limit < 0 || n > limit)
                limit = n;
            var result = new uint[limit];
            int offset = 0;
            for (int i = 0; i < limit; i++)
            {
                (var rune, var size) = Utf8.DecodeRune(this, offset);
                result[i] = rune;
                offset += size;
            }
            return result;
        }

        /// <summary>
        /// Converts a ustring into a List of runes.
        /// </summary>
        /// <returns>A list containing the runes for the string, it is not bound by any limits.</returns>
        public List<Rune> ToRuneList()
        {
            var result = new List<Rune>();
            for (int offset = 0; offset < Length;)
            {
                (var rune, var size) = Utf8.DecodeRune(this, offset);
                result.Add(rune);
                offset += size;
            }
            return result;
        }

        /// <summary>
        /// Converts a ustring into a rune array.
        /// </summary>
        /// <returns>An array containing the runes for the string up to the specified limit.</returns>
        /// <param name="limit">Maximum number of entries to return, or -1 for no limits.</param>
        public List<Rune> ToRuneList(int limit)
        {
            var n = Utf8.RuneCount(this);
            if (limit < 0 || n > limit)
                limit = n;
            var result = new List<Rune>();
            int offset = 0;
            for (int i = 0; i < limit; i++)
            {
                (var rune, var size) = Utf8.DecodeRune(this, offset);
                result[i] = rune;
                offset += size;
            }
            return result;
        }

        // primeRK is the prime base used in Rabin-Karp algorithm.
        const uint primeRK = 16777619;

        // hashStr returns the hash and the appropriate multiplicative
        // factor for use in Rabin-Karp algorithm.
        internal (uint hash, uint multFactor) HashStr()
        {
            uint hash = 0;
            int count = Length;
            for (int i = 0; i < count; i++)
                hash = hash * primeRK + (uint)(this[i]);

            uint pow = 1, sq = primeRK;
            for (int i = count; i > 0; i >>= 1)
            {
                if ((i & 1) != 0)
                    pow *= sq;
                sq *= sq;
            }
            return (hash, pow);
        }

        // hashStrRev returns the hash of the reverse of sep and the
        // appropriate multiplicative factor for use in Rabin-Karp algorithm.
        internal (uint hash, uint multFactor) RevHashStr()
        {
            uint hash = 0;

            int count = Length;
            for (int i = count - 1; i >= 0; i--)
            {
                hash = hash * primeRK + (uint)(this[i]);
            }

            uint pow = 1, sq = primeRK;

            for (int i = count; i > 0; i >>= 1)
            {
                if ((i & 1) != 0)
                {
                    pow *= sq;
                }
                sq *= sq;
            }
            return (hash, pow);
        }

        /// <summary>
        /// Count the number of non-overlapping instances of substr in the string.
        /// </summary>
        /// <returns>If substr is an empty string, Count returns 1 + the number of Unicode code points in the string, otherwise the count of non-overlapping instances in string.</returns>
        /// <param name="substr">Substr.</param>
        public int Count(ustring substr)
        {
            if ((object)substr == null)
                throw new ArgumentNullException(nameof(substr));
            int n = 0;
            if (substr.Length == 0)
                return Utf8.RuneCount(this) + 1;
            int offset = 0;
            int len = Length;
            int slen = substr.Length;
            while (offset < len)
            {
                var i = IndexOf(substr, offset);
                if (i == -1)
                    break;
                n++;
                offset = i + slen;
            }
            return n;
        }

        /// <summary>
        /// Returns a value indicating whether a specified substring occurs within this string.
        /// </summary>
        /// <returns>true if the <paramref name="substr" /> parameter occurs within this string, or if <paramref name="substr" /> is the empty string (""); otherwise, false.</returns>
        /// <param name="substr">The string to seek.</param>
        public bool Contains(ustring substr)
        {
            if ((object)substr == null)
                throw new ArgumentNullException(nameof(substr));
            return IndexOf(substr) >= 0;
        }

        /// <summary>
        /// Returns a value indicating whether a specified rune occurs within this string.
        /// </summary>
        /// <returns>true if the <paramref name="rune" /> parameter occurs within this string; otherwise, false.</returns>
        /// <param name="rune">The rune to seek.</param>
        public bool Contains(uint rune)
        {
            return IndexOf(rune) >= 0;
        }

        /// <summary>
        /// Returns a value indicating whether any of the characters in the provided string occurs within this string.
        /// </summary>
        /// <returns>true if any of the characters in <paramref name="chars" /> parameter occurs within this string; otherwise, false.</returns>
        /// <param name="chars">string containing one or more characters.</param>
        public bool ContainsAny(ustring chars)
        {
            if ((object)chars == null)
                throw new ArgumentNullException(nameof(chars));
            return IndexOfAny(chars) >= 0;
        }

        /// <summary>
        /// Returns a value indicating whether any of the runes occurs within this string.
        /// </summary>
        /// <returns>true if any of the runes in <paramref name="runes" /> parameter occurs within this string; otherwise, false.</returns>
        /// <param name="runes">one or more runes.</param>
        public bool ContainsAny(params uint[] runes)
        {
            return IndexOfAny(runes) >= 0;
        }

        static bool CompareArrayRange(byte[] first, int firstStart, int firstCount, byte[] second)
        {
            for (int i = 0; i < firstCount; i++)
                if (first[i + firstStart] != second[i])
                    return false;
            return true;
        }

        static bool CompareStringRange(ustring first, int firstStart, int firstCount, ustring second)
        {
            for (int i = 0; i < firstCount; i++)
                if (first[i + firstStart] != second[i])
                    return false;
            return true;
        }

        /// <summary>
        /// Reports the zero-based index of the first occurrence of a specified Unicode character or string within this instance. 
        /// </summary>
        /// <returns>The zero-based index position of value if that character is found, or -1 if it is not.   The index position returned is relative to the start of the substring, not to the offset.</returns>
        /// <param name="substr">The string to seek.</param>
        /// <param name="offset">The search starting position.</param>
        public int IndexOf(ustring substr, int offset = 0)
        {
            if ((object)substr == null)
                throw new ArgumentNullException(nameof(substr));

            var n = substr.Length;
            if (n == 0)
                return offset;
            var blen = Length;
            if (offset < 0 || offset > blen)
                throw new ArgumentException(nameof(offset));
            if (n > blen)
                return -1;
            if (n == 1)
                return RealIndexByte(substr[0], offset);
            if (blen == n)
            {
                if (this == substr)
                    return 0;
                return -1;
            }
            blen -= offset;
            if (n == blen)
            {
                // If the offset is zero, we can compare identity
                if (offset == 0)
                {
                    if (((object)substr == (object)this))
                        return offset;
                }

                if (CompareStringRange(this, offset, n, substr))
                    return offset;
                return -1;
            }
            if (n > blen)
                return -1;
            // Rabin-Karp search
            (var hashss, var pow) = substr.HashStr();
            uint h = 0;

            for (int i = 0; i < n; i++)
                h = h * primeRK + (uint)(this[i + offset]);

            if (h == hashss && CompareStringRange(this, offset, n, substr))
                return offset;

            for (int i = n; i < blen;)
            {
                var reali = offset + i;
                h *= primeRK;

                h += (uint)this[reali];

                h -= pow * (uint)(this[reali - n]);
                i++;
                reali++;
                if (h == hashss && CompareStringRange(this, reali - n, n, substr))
                    return reali - n;
            }
            return -1;
        }

        /// <summary>
        /// Reports the zero-based index position of the last occurrence of a specified substring within this instance
        /// </summary>
        /// <returns>The zero-based index position of <paramref name="substr" /> if that character is found, or -1 if it is not.</returns>
        /// <param name="substr">The ustring to seek.</param>
        public int LastIndexOf(ustring substr)
        {
            if ((object)substr == null)
                throw new ArgumentNullException(nameof(substr));
            var n = substr.Length;
            if (n == 0)
                return Length;
            if (n == 1)
                return LastIndexByte(substr[0]);
            if (n == Length)
            {
                if (((object)substr == (object)this))
                    return 0;

                if (CompareStringRange(substr, 0, n, this))
                    return 0;
                return -1;
            }
            if (n > Length)
                return -1;

            // Rabin-Karp search from the end of the string
            (var hashss, var pow) = substr.RevHashStr();
            var last = Length - n;
            uint h = 0;
            for (int i = Length - 1; i >= last; i--)
                h = h * primeRK + (uint)this[i];

            if (h == hashss && CompareStringRange(this, last, Length - last, substr))
                return last;

            for (int i = last - 1; i >= 0; i--)
            {
                h *= primeRK;
                h += (uint)(this[i]);
                h -= pow * (uint)(this[i + n]);
                if (h == hashss && CompareStringRange(this, i, n, substr))
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Reports the zero-based index position of the last occurrence of a specified byte on the underlying byte buffer.
        /// </summary>
        /// <returns>The zero-based index position of <paramref name="b" /> if that byte is found, or -1 if it is not.  </returns>
        /// <param name="b">The byte to seek.</param>
        public int LastIndexByte(byte b)
        {
            for (int i = Length - 1; i >= 0; i--)
                if (this[i] == b)
                    return i;
            return -1;
        }

        /// <summary>
        /// Reports the zero-based index of the first occurrence of the specified Unicode rune in this string
        /// </summary>
        /// <returns>The zero-based index position of <paramref name="rune" /> if that character is found, or -1 if it is not.  If the rune is Utf8.RuneError, it returns the first instance of any invalid UTF-8 byte sequence.</returns>
        /// <param name="rune">Rune.</param>
        /// <param name="offset">Starting offset to start the search from.</param>
        public int IndexOf(uint rune, int offset = 0)
        {
            if (rune < Utf8.RuneSelf)
                return IndexByte((byte)rune, offset);
            if (rune == Utf8.RuneError)
                return Utf8.InvalidIndex(this);
            if (!Utf8.ValidRune(rune))
                return -1;
            return IndexOf(new ByteBufferUString(rune));
        }

        /// <summary>
        /// Reports the zero-based index of the first occurrence of the specified byte in the underlying byte buffer.
        /// </summary>
        /// <returns>The zero-based index position of <paramref name="b" /> if that byte is found, or -1 if it is not.  </returns>
        /// <param name="b">The byte to seek.</param>
        /// <param name="offset">Starting location.</param>
        public int IndexByte(byte b, int offset)
        {
            if (offset < 0 || offset > Length)
                throw new ArgumentException(nameof(offset));
            if (Length == 0)
                return -1;
            return RealIndexByte(b, offset);
        }

        internal abstract int RealIndexByte(byte b, int offset);

        /// <summary>
        /// Reports the zero-based index of the first occurrence in this instance of any character in the provided string
        /// </summary>
        /// <returns>The zero-based index position of the first occurrence in this instance where any character in <paramref name="chars" /> was found; -1 if no character in <paramref name="chars" /> was found.</returns>
        /// <param name="chars">ustring containing characters to seek.</param>
        public int IndexOfAny(ustring chars)
        {
            if ((object)chars == null)
                throw new ArgumentNullException(nameof(chars));
            if (chars.Length == 0)
                return -1;
            var blen = Length;
            if (blen > 8)
            {
                AsciiSet aset;
                if (AsciiSet.MakeAsciiSet(ref aset, chars))
                {
                    for (int i = 0; i < blen; i++)
                        if (AsciiSet.Contains(ref aset, this[i]))
                            return i;
                    return -1;
                }
            }
            var clen = chars.Length;

            for (int i = 0; i < blen;)
            {
                (var rune, var size) = Utf8.DecodeRune(this, i, i - blen);
                for (int j = 0; j < clen;)
                {
                    (var crune, var csize) = Utf8.DecodeRune(chars, j, j - clen);
                    if (crune == rune)
                        return i;
                    j += csize;
                }
                i += size;
            }
            return -1;
        }

        /// <summary>
        /// Reports the zero-based index of the first occurrence in this instance of any runes in the provided string
        /// </summary>
        /// <returns>The zero-based index position of the first occurrence in this instance where any character in <paramref name="runes" /> was found; -1 if no character in <paramref name="runes" /> was found.</returns>
        /// <param name="runes">ustring containing runes.</param>
        public int IndexOfAny(params uint[] runes)
        {
            if (runes == null)
                throw new ArgumentNullException(nameof(runes));
            if (runes.Length == 0)
                return -1;
            var blen = Length;
            if (blen > 8)
            {
                AsciiSet aset;
                if (AsciiSet.MakeAsciiSet(ref aset, runes))
                {
                    for (int i = 0; i < blen; i++)
                        if (AsciiSet.Contains(ref aset, this[i]))
                            return i;
                    return -1;
                }
            }
            var clen = runes.Length;
            for (int i = 0; i < blen;)
            {
                (var rune, var size) = Utf8.DecodeRune(this, i, i - blen);
                for (int j = 0; j < clen; j++)
                {
                    if (rune == runes[j])
                        return i;
                }
                i += size;
            }

            return -1;
        }

        /// <summary>
        /// Reports the zero-based index position of the last occurrence in this instance of one or more characters specified in the ustring.
        /// </summary>
        /// <returns>The index position of the last occurrence in this instance where any character in <paramref name="chars" /> was found; -1 if no character in <paramref name="chars" /> was found.</returns>
        /// <param name="chars">The string containing characters to seek.</param>
        public int LastIndexOfAny(ustring chars)
        {
            if ((object)chars == null)
                throw new ArgumentNullException(nameof(chars));
            if (chars.Length == 0)
                return -1;
            var blen = Length;
            if (blen > 8)
            {
                AsciiSet aset;
                if (AsciiSet.MakeAsciiSet(ref aset, chars))
                {
                    for (int i = blen - 1; i >= 0; i--)
                        if (AsciiSet.Contains(ref aset, this[i]))
                            return i;
                    return -1;
                }
            }
            var clen = chars.Length;
            for (int i = blen; i > 0;)
            {
                (var rune, var size) = Utf8.DecodeLastRune(this, i);
                i -= size;

                for (int j = 0; j < clen;)
                {
                    (var crune, var csize) = Utf8.DecodeRune(chars, j, j - clen);
                    if (crune == rune)
                        return i;
                    j += csize;
                }
            }
            return -1;
        }

        /// <summary>
        /// Implements the IComparable<paramtype name="ustring"/>.CompareTo method
        /// </summary>
        /// <returns>Less than zero if this instance is less than value, zero if they are the same, and higher than zero if the instance is greater.</returns>
        /// <param name="other">Value.</param>
        public int CompareTo(ustring other)
        {
            if ((object)other == null)
                return 1;
            var blen = Length;
            var olen = other.Length;
            if (blen == 0)
            {
                if (olen == 0)
                    return 0;
                return -1;
            }
            else if (olen == 0)
                return 1;

            // Most common case, first character is different
            var e = this[0] - other[0];
            if (e != 0)
                return e;
            for (int i = 1; i < blen; i++)
            {
                if (i >= olen)
                    return 1;
                e = this[i] - other[i];
                if (e == 0)
                    continue;
                return e;
            }
            if (olen > blen)
                return -1;
            return 0;
        }

        int IComparable.CompareTo(object value)
        {
            if (value == null)
                return 1;
            var other = value as ustring;
            if ((object)other == null)
                throw new ArgumentException("Argument must be a ustring");
            return CompareTo(other);
        }

        // Generic split: splits after each instance of sep,
        // including sepSave bytes of sep in the subarrays.
        ustring[] GenSplit(ustring sep, int sepSave, int n = -1)
        {
            if (n == 0)
                return Array.Empty<ustring>();
            if (sep == "")
                return Explode(n);
            if (n < 0 || n == Int32.MaxValue)
                n = Count(sep) + 1;
            var result = new ustring[n];
            n--;
            int offset = 0, i = 0;
            var sepLen = sep.Length;
            while (i < n)
            {
                var m = IndexOf(sep, offset);
                if (m < 0)
                    break;
                result[i] = this[offset, m + sepSave];
                offset = m + sepLen;
                i++;
            }
            result[i] = this[offset, null];
            return result;
        }

        /// <summary>
        /// Split the string using at every instance of a string separator
        /// </summary>
        /// <returns>An array containing the individual strings, excluding the separator string.</returns>
        /// <param name="separator">Separator string.</param>
        /// <param name="n">Optional maximum number of results to return, or -1 for an unlimited result</param>
        public ustring[] Split(ustring separator, int n = -1)
        {
            if ((object)separator == null)
                throw new ArgumentNullException(nameof(separator));

            return GenSplit(separator, 0, n);
        }

        /// <summary>
        /// Determines whether the beginning of this string instance matches the specified string.
        /// </summary>
        /// <returns><c>true</c> if <paramref name="prefix" /> matches the beginning of this string; otherwise, <c>false</c>.</returns>
        /// <param name="prefix">Prefix.</param>
        public bool StartsWith(ustring prefix)
        {
            if ((object)prefix == null)
                throw new ArgumentNullException(nameof(prefix));
            if (Length < prefix.Length)
                return false;
            return CompareStringRange(this, 0, prefix.Length, prefix);
        }

        /// <summary>
        /// Determines whether the end of this string instance matches the specified string.
        /// </summary>
        /// <returns>true if <paramref name="suffix" /> matches the end of this instance; otherwise, false.</returns>
        /// <param name="suffix">The string to compare to the substring at the end of this instance.</param>
        public bool EndsWith(ustring suffix)
        {
            if ((object)suffix == null)
                throw new ArgumentNullException(nameof(suffix));
            if (Length < suffix.Length)
                return false;
            return CompareStringRange(this, Length - suffix.Length, suffix.Length, suffix);
        }

        /// <summary>
        /// Concatenates all the elements of a ustring array, using the specified separator between each element.
        /// </summary>
        /// <returns>A string that consists of the elements in <paramref name="values" /> delimited by the <paramref name="separator" /> string. If <paramref name="values" /> is an empty array, the method returns <see cref="F:System.ustring.Empty" />.</returns>
        /// <param name="separator">Separator.</param>
        /// <param name="values">Values.</param>
        public static ustring Join(ustring separator, params ustring[] values)
        {
            if ((object)separator == null)
                separator = Empty;
            if (values == null)
                throw new ArgumentNullException(nameof(values));
            if (values.Length == 0)
                return Empty;
            int size = 0, items = 0;
            foreach (var t in values)
            {
                if ((object)t == null)
                    continue;
                size += t.Length;
                items++;
            }
            if (items == 1)
            {
                foreach (var t in values)
                    if (t != null)
                        return t;
            }
            var slen = separator.Length;
            size += (items - 1) * slen;
            var result = new byte[size];
            int offset = 0;
            foreach (var t in values)
            {
                if ((object)t == null)
                    continue;
                var tlen = t.Length;
                t.CopyTo(fromOffset: 0, target: result, targetOffset: offset, count: tlen);
                offset += tlen;
                separator.CopyTo(fromOffset: 0, target: result, targetOffset: offset, count: slen);
                offset += slen;
            }
            return new ByteBufferUString(result);
        }

        // asciiSet is a 32-byte value, where each bit represents the presence of a
        // given ASCII character in the set. The 128-bits of the lower 16 bytes,
        // starting with the least-significant bit of the lowest word to the
        // most-significant bit of the highest word, map to the full range of all
        // 128 ASCII characters. The 128-bits of the upper 16 bytes will be zeroed,
        // ensuring that any non-ASCII character will be reported as not in the set.
        struct AsciiSet
        {
            unsafe internal fixed uint ascii[8];

            public static bool MakeAsciiSet(ref AsciiSet aset, ustring chars)
            {
                var n = chars.Length;
                unsafe
                {
                    fixed (uint* ascii = aset.ascii)
                    {
                        for (int i = 0; i < n; i++)
                        {
                            var c = chars[i];
                            if (c >= Utf8.RuneSelf)
                                return false;

                            var t = (uint)(1 << (c & 31));
                            ascii[c >> 5] |= t;
                        }
                    }
                }
                return true;
            }

            public static bool MakeAsciiSet(ref AsciiSet aset, uint[] runes)
            {
                var n = runes.Length;
                unsafe
                {
                    fixed (uint* ascii = aset.ascii)
                    {
                        for (int i = 0; i < n; i++)
                        {
                            var r = runes[i];
                            if (r >= Utf8.RuneSelf)
                                return false;
                            byte c = (byte)r;
                            var t = (uint)(1 << (c & 31));
                            ascii[c >> 5] |= t;
                        }
                    }
                }
                return true;
            }
            public static bool Contains(ref AsciiSet aset, byte b)
            {
                unsafe
                {
                    fixed (uint* ascii = aset.ascii)
                    {
                        return (ascii[b >> 5] & (1 << (b & 31))) != 0;
                    }
                }
            }
        }

        /// <summary>
        /// Concatenates the contents of two <see cref="NStack.ustring"/> instances.
        /// </summary>
        /// <param name="u1">The first <see cref="NStack.ustring"/> to add, can be null.</param>
        /// <param name="u2">The second <see cref="NStack.ustring"/> to add, can be null.</param>
        /// <returns>The <see cref="T:NStack.ustring"/> that is the concatenation of the strings of <c>u1</c> and <c>u2</c>.</returns>
        public static ustring operator +(ustring u1, ustring u2)
        {
            var u1l = (object)u1 == null ? 0 : u1.Length;
            var u2l = (object)u2 == null ? 0 : u2.Length;
            var copy = new byte[u1l + u2l];
            if (u1 != null)
                u1.CopyTo(fromOffset: 0, target: copy, targetOffset: 0, count: u1l);
            if (u2 != null)
                u2.CopyTo(fromOffset: 0, target: copy, targetOffset: u1l, count: u2l);
            return new ByteBufferUString(copy);
        }

        /// <summary>
        /// Convert from <see cref="string"/> to <see cref="ustring"/>
        /// or to null if the <paramref name="v"/> is null.
        /// </summary>
        /// <param name="v">The ustring.</param>
        public static explicit operator string(ustring v)
        {
            if (v == null)
                return null;

            return v.ToString();
        }

        /// <summary>
        /// An enumerator that returns the index within the string, and the rune found at that location
        /// </summary>
        /// <returns>Enumerable object that can be used to iterate and get the index of the values at the same time.</returns>
        /// <remarks>
        /// This is useful to iterate over the string and obtain both the index of the rune and the rune
        /// in the same call.  This version does allocate an object for the enumerator, if you want to avoid
        /// the object allocation, you can use the following code to iterate over the contents of the string
        /// <example>
        /// <code lang="c#">
        ///   ustring mystr = "hello";
        ///   int byteLen = mystr.Length;
        ///   for (int i = 0; i &lt; byteLen;) {
        ///       (var rune, var size) = Utf8.DecodeRune(mystr, i, i - byteLen);
        ///       Console.WriteLine ("Rune is: " + rune);
        ///       i += size;
        ///   }
        /// </code>
        /// </example>
        /// </remarks>
        public IEnumerable<(int index, uint rune)> Range()
        {
            int blen = Length;
            for (int i = 0; i < blen;)
            {
                (var rune, var size) = Utf8.DecodeRune(this, i, i - blen);
                yield return (i, rune);
                i += size;
            }
            yield break;
        }

        /// <summary>
        /// Returns the Rune encoded at the specified byte <paramref name="index"/>.   
        /// </summary>
        /// <returns>The <see cref="T:System.Rune"/> which might be Rune.Error if the value at the specified index is not UTF8 compliant, for example because it is not a valid UTF8 encoding, or the buffer is too short.</returns>
        /// <param name="index">Index.</param>
        public Rune RuneAt(int index)
        {
            return Utf8.DecodeRune(this, index).Rune;
        }

        // Map returns a copy of the string s with all its characters modified
        // according to the mapping function. If mapping returns a negative value, the character is
        // dropped from the string with no replacement.
        static ustring Map(Func<uint, uint> mapping, ustring s, Action scanReset = null)
        {
            // In the worst case, the string can grow when mapped, making
            // things unpleasant. But it's so rare we barge in assuming it's
            // fine. It could also shrink but that falls out naturally.

            // nbytes is the number of bytes needed to encode the string
            int nbytes = 0;

            bool modified = false;
            int blen = s.Length;
            for (int offset = 0; offset < blen;)
            {
                (var rune, var size) = Utf8.DecodeRune(s, offset);
                var mapped = mapping(rune);
                if (mapped == rune)
                {
                    nbytes++;
                    offset += size;
                    continue;
                }
                modified = true;
                var mappedLen = Utf8.RuneLen(mapped);
                if (mappedLen == -1)
                    mappedLen = 3; // Errors are encoded with 3 bytes
                nbytes += mappedLen;

                if (rune == Utf8.RuneError)
                {
                    // RuneError is the result of either decoding
                    // an invalid sequence or '\uFFFD'. Determine
                    // the correct number of bytes we need to advance.
                    (_, size) = Utf8.DecodeRune(s[offset, 0]);

                }
                offset += size;
            }


            if (!modified)
                return s;

            scanReset?.Invoke();

            var result = new byte[nbytes];
            int targetOffset = 0;
            for (int offset = 0; offset < blen;)
            {
                (var rune, var size) = Utf8.DecodeRune(s, offset);
                offset += size;

                var mapped = mapping(rune);

                // common case
                if (0 < mapped && mapped <= Utf8.RuneSelf)
                {
                    result[targetOffset] = (byte)mapped;
                    targetOffset++;
                    continue;
                }

                targetOffset += Utf8.EncodeRune(mapped, dest: result, offset: targetOffset);
            }
            return new ByteBufferUString(result);
        }

        /// <summary>
        /// Returns a copy of the string s with all Unicode letters mapped to their upper case.
        /// </summary>
        /// <returns>The string to uppercase.</returns>
        public ustring ToUpper()
        {
            return Map(Unicode.ToUpper, this);
        }

        /// <summary>
        /// Returns a copy of the string s with all Unicode letters mapped to their upper case giving priority to the special casing rules.
        /// </summary>
        /// <returns>The string to uppercase.</returns>
        public ustring ToUpper(Unicode.SpecialCase specialCase)
        {
            return Map((rune) => specialCase.ToUpper(rune), this);
        }

        /// <summary>
        /// Returns a copy of the string s with all Unicode letters mapped to their lower case.
        /// </summary>
        /// <returns>The lowercased string.</returns>
        public ustring ToLower()
        {
            return Map(Unicode.ToLower, this);
        }

        /// <summary>
        /// Returns a copy of the string s with all Unicode letters mapped to their lower case giving priority to the special casing rules.
        /// </summary>
        /// <returns>The string to uppercase.</returns>
        public ustring ToLower(Unicode.SpecialCase specialCase)
        {
            return Map((rune) => specialCase.ToLower(rune), this);
        }
        /// <summary>
        /// Returns a copy of the string s with all Unicode letters mapped to their title case.
        /// </summary>
        /// <returns>The title-cased string.</returns>
        public ustring ToTitle()
        {
            return Map(Unicode.ToTitle, this);
        }

        /// <summary>
        /// Returns a copy of the string s with all Unicode letters mapped to their title case giving priority to the special casing rules.
        /// </summary>
        /// <returns>The string to uppercase.</returns>
        public ustring ToTitle(Unicode.SpecialCase specialCase)
        {
            return Map((rune) => specialCase.ToTitle(rune), this);
        }

        /// <summary>
        /// IsSeparator reports whether the rune could mark a word boundary.
        /// </summary>
        /// <returns><c>true</c>, if the rune can be considered a word boundary, <c>false</c> otherwise.</returns>
        /// <param name="rune">The rune to test.</param>
        public static bool IsSeparator(uint rune)
        {
            if (rune <= 0x7f)
            {
                // ASCII alphanumerics and underscore are not separators
                if ('0' <= rune && rune <= '9')
                    return false;
                if ('a' <= rune && rune <= 'z')
                    return false;
                if ('A' <= rune && rune <= 'Z')
                    return false;
                if (rune == '_')
                    return false;
                return true;
            }
            // Letters and digits are not separators
            if (Unicode.IsLetter(rune) || Unicode.IsDigit(rune))
                return false;
            // Otherwise, all we can do for now is treat spaces as separators.
            return Unicode.IsSpace(rune);
        }

        /// <summary>
        /// Converts the string to Title-case, that is every word (as determined by <see cref="M:NStack.ustring.IsSeparator"/> is Title cased.
        /// </summary>
        /// <returns>A title-cased string.</returns>
        public ustring Title()
        {
            uint prev = ' ';
            return Map((rune) =>
            {
                if (IsSeparator(prev))
                {
                    prev = rune;
                    return Unicode.ToTitle(rune);
                }
                prev = rune;
                return rune;
            }, this, () => { prev = ' '; });
        }

        // IndexFunc returns the index into s of the first Unicode
        // code point satisfying f(c), or -1 if none do.

        /// <summary>
        /// Rune predicate functions take a rune as input and return a boolean determining if the rune matches or not.
        /// </summary>
        public delegate bool RunePredicate(uint rune);

        /// <summary>
        /// IndexOf returns the index into s of the first Unicode rune satisfying matchFunc(rune), or -1 if none do.
        /// </summary>
        /// <returns>The index inside the string where the rune is found, or -1 on error.</returns>
        /// <param name="matchFunc">Match func, it receives a rune as a parameter and should return true if it matches, false otherwise.</param>
        public int IndexOf(RunePredicate matchFunc)
        {
            return FlexIndexOf(matchFunc, true);
        }

        /// <summary>
        /// LastIndexOf returns the index into s of the last Unicode rune satisfying matchFunc(rune), or -1 if none do.
        /// </summary>
        /// <returns>The last index inside the string where the rune is found, or -1 on error.</returns>
        /// <param name="matchFunc">Match func, it receives a rune as a parameter and should return true if it matches, false otherwise.</param>
        public int LastIndexOf(RunePredicate matchFunc)
        {
            return FlexLastIndexOf(matchFunc, true);
        }


        /// <summary>
        /// Returns a slice of the string with all leading runes matching the predicate removed.
        /// </summary>
        /// <returns>The current string if the predicate does not match anything, or a slice of the string starting in the first rune after the predicate matched.</returns>
        /// <param name="predicate">Function that determines whether this character must be trimmed.</param>
        public ustring TrimStart(RunePredicate predicate)
        {
            var i = FlexIndexOf(predicate, false);
            if (i == -1)
                return this;
            return this[i, null];
        }

        RunePredicate MakeCutSet(ustring cutset)
        {
            if (cutset.Length == 1 && cutset[0] < Utf8.RuneSelf)
                return (x) => x == (uint)cutset[0];
            AsciiSet aset;
            if (AsciiSet.MakeAsciiSet(ref aset, cutset))
            {
                return (x) => x < Utf8.RuneSelf && AsciiSet.Contains(ref aset, (byte)x);
            }
            return (x) => cutset.IndexOf(x) >= 0;
        }

        /// <summary>
        /// TrimStarts returns a slice of the string with all leading characters in cutset removed.
        /// </summary>
        /// <returns>The slice of the string with all cutset characters removed.</returns>
        /// <param name="cutset">Characters to remove.</param>
        public ustring TrimStart(ustring cutset)
        {
            if (IsEmpty || (object)cutset == null || cutset.IsEmpty)
                return this;
            return TrimStart(MakeCutSet(cutset));
        }

        /// <summary>
        /// TrimEnd returns a slice of the string with all leading characters in cutset removed.
        /// </summary>
        /// <returns>The slice of the string with all cutset characters removed.</returns>
        /// <param name="cutset">Characters to remove.</param>
        public ustring TrimEnd(ustring cutset)
        {
            if (IsEmpty || (object)cutset == null || cutset.IsEmpty)
                return this;
            return TrimEnd(MakeCutSet(cutset));
        }

        /// <summary>
        /// Returns a slice of the string with all leading and trailing space characters removed (as determined by <see cref="M:NStack.Unicode.IsSpace()"/> 
        /// </summary>
        /// <returns>The space.</returns>
        public ustring TrimSpace()
        {
            return Trim(Unicode.IsSpace);
        }

        /// <summary>
        /// Returns a slice of the string with all trailing runes matching the predicate removed.
        /// </summary>
        /// <returns>The current string if the predicate does not match anything, or a slice of the string starting in the first rune after the predicate matched.</returns>
        /// <param name="predicate">Function that determines whether this character must be trimmed.</param>
        public ustring TrimEnd(RunePredicate predicate)
        {
            var i = FlexLastIndexOf(predicate, false);
            if (i >= 0 && this[i] >= Utf8.RuneSelf)
            {
                (var rune, var wid) = Utf8.DecodeRune(this[i, null]);
                i += wid;
            }
            else
                i++;
            return this[0, i];
        }

        /// <summary>
        /// Returns a slice of the string with all leading and trailing runes matching the predicate removed.
        /// </summary>
        /// <returns>The trim.</returns>
        /// <param name="predicate">Predicate.</param>
        public ustring Trim(RunePredicate predicate)
        {
            return TrimStart(predicate).TrimEnd(predicate);
        }

        // FlexIndexOf is a generalization of IndexOf that allows
        // the desired result of the predicate to be specified.
        int FlexIndexOf(RunePredicate matchFunc, bool expected)
        {
            int blen = Length;
            for (int i = 0; i < blen;)
            {
                (var rune, var size) = Utf8.DecodeRune(this, i, i - blen);
                if (matchFunc(rune) == expected)
                    return i;
                i += size;
            }
            return -1;
        }

        // FlexLastIndexOf is a generalization of IndexOf that allows
        // the desired result of the predicate to be specified.
        int FlexLastIndexOf(RunePredicate matchFunc, bool expected)
        {
            int blen = Length;
            for (int i = blen; i > 0;)
            {
                (var rune, var size) = Utf8.DecodeLastRune(this, i);
                i -= size;
                if (matchFunc(rune) == expected)
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Returns a new ustring with the non-overlapping instances of oldValue replaced with newValue.
        /// </summary>
        /// <returns>The replace.</returns>
        /// <param name="oldValue">Old value;  if it is empty, the string matches at the beginning of the string and after each UTF-8 sequence, yielding up to k+1 replacements for a k-rune string.</param>
        /// <param name="newValue">New value that will replace the oldValue.</param>
        /// <param name="maxReplacements">Optional, the maximum number of replacements.   Negative values indicate that there should be no limit to the replacements.</param>
        public ustring Replace(ustring oldValue, ustring newValue, int maxReplacements = -1)
        {
            if (oldValue == newValue || maxReplacements == 0)
                return this;

            // Compute number of replacements
            var m = Count(oldValue);
            if (m == 0)
                return this;
            if (maxReplacements < 0 || m < maxReplacements)
                maxReplacements = m;

            var oldLen = oldValue.Length;
            var newLen = newValue.Length;

            // Apply replacements to buffer
            var result = new byte[Length + maxReplacements * (newValue.Length - oldValue.Length)];
            int w = 0, start = 0;
            for (int i = 0; i < maxReplacements; i++)
            {
                var j = start;
                if (oldLen == 0)
                {
                    if (i > 0)
                    {
                        (_, var wid) = Utf8.DecodeRune(this, start);
                        j += wid;
                    }
                }
                else
                    j += IndexOf(oldValue, start) - start;
                var copyCount = j - start;
                if (copyCount > 0)
                {
                    CopyTo(fromOffset: start, target: result, targetOffset: w, count: copyCount);
                    w += copyCount;
                }

                newValue.CopyTo(fromOffset: 0, target: result, targetOffset: w, count: newLen);
                w += newLen;
                start = j + oldLen;
            }
            CopyTo(fromOffset: start, target: result, targetOffset: w, count: Length - start);
            return new ByteBufferUString(result);
        }

        /// <summary>
        /// Represent the null or empty value related to the ustring.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(ustring value)
        {
            if (value?.IsEmpty != false)
                return true;
            return false;
        }

        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            return Convert.ToBoolean(ToString(), provider);
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            return Convert.ToByte(ToString(), provider);
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            return Convert.ToChar(ToString(), provider);
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            return Convert.ToDateTime(ToString(), provider);
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            return Convert.ToDecimal(ToString(), provider);
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            return Convert.ToDouble(ToString(), provider);
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            return Convert.ToInt16(ToString(), provider);
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            return Convert.ToInt32(ToString(), provider);
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            return Convert.ToInt64(ToString(), provider);
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            return Convert.ToSByte(ToString(), provider);
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            return Convert.ToSingle(ToString(), provider);
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            return ToString();
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            if (conversionType == typeof(string))
                return ToString();
            return Convert.ChangeType(ToString(), conversionType);
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            return Convert.ToUInt16(ToString(), provider);
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            return Convert.ToUInt32(ToString(), provider);
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            return Convert.ToUInt64(ToString(), provider);
        }

        IEnumerator<uint> IEnumerable<uint>.GetEnumerator()
        {
            int blen = Length;
            for (int i = 0; i < blen;)
            {
                (var rune, var size) = Utf8.DecodeRune(this, i, i - blen);
                i += size;
                yield return rune;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            int blen = Length;
            for (int i = 0; i < blen;)
            {
                (var rune, var size) = Utf8.DecodeRune(this, i, i - blen);
                i += size;
                yield return rune;
            }
        }
    }
    //=======================================================================
    /// <summary>
    /// UTF8 Helper methods and routines.
    /// </summary>
    /// <remarks>
    /// The term "rune" is used to represent a Unicode code point merely because it is a shorter way of talking about it.
    /// </remarks>
    public static class Utf8
    {
        /// <summary>
        /// The "error" Rune or "Unicode replacement character"
        /// </summary>
        public static uint RuneError = 0xfffd;

        /// <summary>
        /// Characters below RuneSelf are represented as themselves in a single byte
        /// </summary>
        public const byte RuneSelf = 0x80;

        /// <summary>
        /// Maximum number of bytes required to encode every unicode code point.
        /// </summary>
        public const int Utf8Max = 4;

        /// <summary>
        /// Maximum valid Unicode code point.
        /// </summary>
        public const uint MaxRune = 0x10ffff;

        // Code points in the surrogate range are not valid for UTF-8.
        const uint surrogateMin = 0xd800;
        const uint surrogateMax = 0xdfff;

        const byte t1 = 0x00; // 0000 0000
        const byte tx = 0x80; // 1000 0000
        const byte t2 = 0xC0; // 1100 0000
        const byte t3 = 0xE0; // 1110 0000
        const byte t4 = 0xF0; // 1111 0000
        const byte t5 = 0xF8; // 1111 1000

        const byte maskx = 0x3F; // 0011 1111
        const byte mask2 = 0x1F; // 0001 1111
        const byte mask3 = 0x0F; // 0000 1111
        const byte mask4 = 0x07; // 0000 0111

        const uint rune1Max = (1 << 7) - 1;
        const uint rune2Max = (1 << 11) - 1;
        const uint rune3Max = (1 << 16) - 1;

        // The default lowest and highest continuation byte.
        const byte locb = 0x80; // 1000 0000
        const byte hicb = 0xBF; // 1011 1111

        // These names of these constants are chosen to give nice alignment in the
        // table below. The first nibble is an index into acceptRanges or F for
        // special one-byte ca1es. The second nibble is the Rune length or the
        // Status for the special one-byte ca1e.
        const byte xx = 0xF1; // invalid: size 1
        const byte a1 = 0xF0; // a1CII: size 1
        const byte s1 = 0x02; // accept 0, size 2
        const byte s2 = 0x13; // accept 1, size 3
        const byte s3 = 0x03; // accept 0, size 3
        const byte s4 = 0x23; // accept 2, size 3
        const byte s5 = 0x34; // accept 3, size 4
        const byte s6 = 0x04; // accept 0, size 4
        const byte s7 = 0x44; // accept 4, size 4

        static byte[] first = new byte[256]{
			//   1   2   3   4   5   6   7   8   9   A   B   C   D   E   F
			a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, // 0x00-0x0F
			a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1,a1, a1, a1, a1, a1, // 0x10-0x1F
			a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, // 0x20-0x2F
			a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, // 0x30-0x3F
			a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, // 0x40-0x4F
			a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, // 0x50-0x5F
			a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, // 0x60-0x6F
			a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, // 0x70-0x7F

			//   1   2   3   4   5   6   7   8   9   A   B   C   D   E   F
			xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, // 0x80-0x8F
			xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, // 0x90-0x9F
			xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, // 0xA0-0xAF
			xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, // 0xB0-0xBF
			xx, xx, s1, s1, s1, s1, s1, s1, s1, s1, s1, s1, s1, s1, s1, s1, // 0xC0-0xCF
			s1, s1, s1, s1, s1, s1, s1, s1, s1, s1, s1, s1, s1, s1, s1, s1, // 0xD0-0xDF
			s2, s3, s3, s3, s3, s3, s3, s3, s3, s3, s3, s3, s3, s4, s3, s3, // 0xE0-0xEF
			s5, s6, s6, s6, s7, xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, // 0xF0-0xFF
		};

        struct AcceptRange
        {
            public byte Lo, Hi;
            public AcceptRange(byte lo, byte hi)
            {
                Lo = lo;
                Hi = hi;
            }
        }

        static AcceptRange[] AcceptRanges = new AcceptRange[] {
            new AcceptRange (locb, hicb),
            new AcceptRange (0xa0, hicb),
            new AcceptRange (locb, 0x9f),
            new AcceptRange (0x90, hicb),
            new AcceptRange (locb, 0x8f),
        };

        /// <summary>
        /// FullRune reports whether the bytes in p begin with a full UTF-8 encoding of a rune.
        /// An invalid encoding is considered a full Rune since it will convert as a width-1 error rune.
        /// </summary>
        /// <returns><c>true</c>, if the bytes in p begin with a full UTF-8 encoding of a rune, <c>false</c> otherwise.</returns>
        /// <param name="p">byte array.</param>
        public static bool FullRune(byte[] p)
        {
            if (p == null)
                throw new ArgumentNullException(nameof(p));
            var n = p.Length;

            if (n == 0)
                return false;
            var x = first[p[0]];
            if (n >= (x & 7))
            {
                // ascii, invalid or valid
                return true;
            }
            // must be short or invalid
            if (n > 1)
            {
                var accept = AcceptRanges[x >> 4];
                var c = p[1];
                if (c < accept.Lo || accept.Hi < c)
                    return true;
                else if (n > 2 && (p[2] < locb || hicb < p[2]))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// FullRune reports whether the ustring begins with a full UTF-8 encoding of a rune.
        /// An invalid encoding is considered a full Rune since it will convert as a width-1 error rune.
        /// </summary>
        /// <returns><c>true</c>, if the bytes in p begin with a full UTF-8 encoding of a rune, <c>false</c> otherwise.</returns>
        /// <param name="str">The string to check.</param>
        public static bool FullRune(ustring str)
        {
            if ((object)str == null)
                throw new ArgumentNullException(nameof(str));
            var n = str.Length;

            if (n == 0)
                return false;
            var x = first[str[0]];
            if (n >= (x & 7))
            {
                // ascii, invalid or valid
                return true;
            }
            // must be short or invalid
            if (n > 1)
            {
                var accept = AcceptRanges[x >> 4];
                var c = str[1];
                if (c < accept.Lo || accept.Hi < c)
                    return true;
                else if (n > 2 && (str[2] < locb || hicb < str[2]))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// DecodeRune unpacks the first UTF-8 encoding in p and returns the rune and
        /// its width in bytes. 
        /// </summary>
        /// <returns>If p is empty it returns (RuneError, 0). Otherwise, if
        /// the encoding is invalid, it returns (RuneError, 1). Both are impossible
        /// results for correct, non-empty UTF-8.
        /// </returns>
        /// <param name="buffer">Byte buffer containing the utf8 string.</param>
        /// <param name="start">Starting offset to look into..</param>
        /// <param name="n">Number of bytes valid in the buffer, or -1 to make it the lenght of the buffer.</param>
        public static (uint Rune, int Size) DecodeRune(byte[] buffer, int start = 0, int n = -1)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));
            if (start < 0)
                throw new ArgumentException("invalid offset", nameof(start));
            if (n < 0)
                n = buffer.Length - start;
            if (start > buffer.Length - n)
                throw new ArgumentException("Out of bounds");

            if (n < 1)
                return (RuneError, 0);

            var p0 = buffer[start];
            var x = first[p0];
            if (x >= a1)
            {
                // The following code simulates an additional check for x == xx and
                // handling the ASCII and invalid cases accordingly. This mask-and-or
                // approach prevents an additional branch.
                uint mask = (uint)((((byte)x) << 31) >> 31); // Create 0x0000 or 0xFFFF.
                return (((buffer[start]) & ~mask | RuneError & mask), 1);
            }

            var sz = x & 7;
            var accept = AcceptRanges[x >> 4];
            if (n < (int)sz)
                return (RuneError, 1);

            var b1 = buffer[start + 1];
            if (b1 < accept.Lo || accept.Hi < b1)
                return (RuneError, 1);

            if (sz == 2)
                return ((uint)((p0 & mask2)) << 6 | (uint)((b1 & maskx)), 2);

            var b2 = buffer[start + 2];
            if (b2 < locb || hicb < b2)
                return (RuneError, 1);

            if (sz == 3)
                return (((uint)((p0 & mask3)) << 12 | (uint)((b1 & maskx)) << 6 | (uint)((b2 & maskx))), 3);

            var b3 = buffer[start + 3];
            if (b3 < locb || hicb < b3)
            {
                return (RuneError, 1);
            }
            return ((uint)(p0 & mask4) << 18 | (uint)(b1 & maskx) << 12 | (uint)(b2 & maskx) << 6 | (uint)(b3 & maskx), 4);
        }

        /// <summary>
        /// DecodeRune unpacks the first UTF-8 encoding in the ustring returns the rune and
        /// its width in bytes. 
        /// </summary>
        /// <returns>If p is empty it returns (RuneError, 0). Otherwise, if
        /// the encoding is invalid, it returns (RuneError, 1). Both are impossible
        /// results for correct, non-empty UTF-8.
        /// </returns>
        /// <param name="str">ustring to decode.</param>
        /// <param name="start">Starting offset to look into..</param>
        /// <param name="n">Number of bytes valid in the buffer, or -1 to make it the lenght of the buffer.</param>
        public static (uint Rune, int size) DecodeRune(ustring str, int start = 0, int n = -1)
        {
            if ((object)str == null)
                throw new ArgumentNullException(nameof(str));
            if (start < 0)
                throw new ArgumentException("invalid offset", nameof(start));
            if (n < 0)
                n = str.Length - start;
            if (start > str.Length - n)
                throw new ArgumentException("Out of bounds");

            if (n < 1)
                return (RuneError, 0);

            var p0 = str[start];
            var x = first[p0];
            if (x >= a1)
            {
                // The following code simulates an additional check for x == xx and
                // handling the ASCII and invalid cases accordingly. This mask-and-or
                // approach prevents an additional branch.
                uint mask = (uint)((((byte)x) << 31) >> 31); // Create 0x0000 or 0xFFFF.
                return (((str[start]) & ~mask | RuneError & mask), 1);
            }

            var sz = x & 7;
            var accept = AcceptRanges[x >> 4];
            if (n < (int)sz)
                return (RuneError, 1);

            var b1 = str[start + 1];
            if (b1 < accept.Lo || accept.Hi < b1)
                return (RuneError, 1);

            if (sz == 2)
                return ((uint)((p0 & mask2)) << 6 | (uint)((b1 & maskx)), 2);

            var b2 = str[start + 2];
            if (b2 < locb || hicb < b2)
                return (RuneError, 1);

            if (sz == 3)
                return (((uint)((p0 & mask3)) << 12 | (uint)((b1 & maskx)) << 6 | (uint)((b2 & maskx))), 3);

            var b3 = str[start + 3];
            if (b3 < locb || hicb < b3)
            {
                return (RuneError, 1);
            }
            return ((uint)(p0 & mask4) << 18 | (uint)(b1 & maskx) << 12 | (uint)(b2 & maskx) << 6 | (uint)(b3 & maskx), 4);
        }

        // RuneStart reports whether the byte could be the first byte of an encoded,
        // possibly invalid rune. Second and subsequent bytes always have the top two
        // bits set to 10.
        static bool RuneStart(byte b) => (b & 0xc0) != 0x80;

        /// <summary>
        /// DecodeLastRune unpacks the last UTF-8 encoding in buffer
        /// </summary>
        /// <returns>The last rune and its width in bytes.</returns>
        /// <param name="buffer">Buffer to decode rune from;   if it is empty,
        /// it returns (RuneError, 0). Otherwise, if
        /// the encoding is invalid, it returns (RuneError, 1). Both are impossible
        /// results for correct, non-empty UTF-8.</param>
        /// <param name="end">Scan up to that point, if the value is -1, it sets the value to the lenght of the buffer.</param>
        /// <remarks>
        /// An encoding is invalid if it is incorrect UTF-8, encodes a rune that is
        /// out of range, or is not the shortest possible UTF-8 encoding for the
        /// value. No other validation is performed.</remarks> 
        public static (uint Rune, int size) DecodeLastRune(byte[] buffer, int end = -1)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));
            if (buffer.Length == 0)
                return (RuneError, 0);
            if (end == -1)
                end = buffer.Length;
            else if (end > buffer.Length)
                throw new ArgumentException("The end goes beyond the size of the buffer");

            var start = end - 1;
            uint r = buffer[start];
            if (r < RuneSelf)
                return (r, 1);
            // guard against O(n^2) behavior when traversing
            // backwards through strings with long sequences of
            // invalid UTF-8.
            var lim = end - Utf8Max;

            if (lim < 0)
                lim = 0;

            for (start--; start >= lim; start--)
            {
                if (RuneStart(buffer[start]))
                {
                    break;
                }
            }
            if (start < 0)
                start = 0;
            int size;
            (r, size) = DecodeRune(buffer, start, end - start);
            if (start + size != end)
                return (RuneError, 1);
            return (r, size);
        }

        /// <summary>
        /// DecodeLastRune unpacks the last UTF-8 encoding in the ustring.
        /// </summary>
        /// <returns>The last rune and its width in bytes.</returns>
        /// <param name="str">String to decode rune from;   if it is empty,
        /// it returns (RuneError, 0). Otherwise, if
        /// the encoding is invalid, it returns (RuneError, 1). Both are impossible
        /// results for correct, non-empty UTF-8.</param>
        /// <param name="end">Scan up to that point, if the value is -1, it sets the value to the lenght of the buffer.</param>
        /// <remarks>
        /// An encoding is invalid if it is incorrect UTF-8, encodes a rune that is
        /// out of range, or is not the shortest possible UTF-8 encoding for the
        /// value. No other validation is performed.</remarks> 
        public static (uint Rune, int size) DecodeLastRune(ustring str, int end = -1)
        {
            if ((object)str == null)
                throw new ArgumentNullException(nameof(str));
            if (str.Length == 0)
                return (RuneError, 0);
            if (end == -1)
                end = str.Length;
            else if (end > str.Length)
                throw new ArgumentException("The end goes beyond the size of the buffer");

            var start = end - 1;
            uint r = str[start];
            if (r < RuneSelf)
                return (r, 1);
            // guard against O(n^2) behavior when traversing
            // backwards through strings with long sequences of
            // invalid UTF-8.
            var lim = end - Utf8Max;

            if (lim < 0)
                lim = 0;

            for (start--; start >= lim; start--)
            {
                if (RuneStart(str[start]))
                {
                    break;
                }
            }
            if (start < 0)
                start = 0;
            int size;
            (r, size) = DecodeRune(str, start, end - start);
            if (start + size != end)
                return (RuneError, 1);
            return (r, size);
        }

        /// <summary>
        /// number of bytes required to encode the rune.
        /// </summary>
        /// <returns>The length, or -1 if the rune is not a valid value to encode in UTF-8.</returns>
        /// <param name="rune">Rune to probe.</param>
        public static int RuneLen(uint rune)
        {
            if (rune <= rune1Max)
                return 1;
            if (rune <= rune2Max)
                return 2;
            if (rune > MaxRune || (surrogateMin <= rune && rune <= surrogateMax))
                // error
                return -1;
            if (rune <= rune3Max)
                return 3;
            if (rune <= MaxRune)
                return 4;
            return -1;
        }

        /// <summary>
        /// Writes into the destination buffer starting at offset the UTF8 encoded version of the rune
        /// </summary>
        /// <returns>The number of bytes written into the destination buffer.</returns>
        /// <param name="rune">Rune to encode.</param>
        /// <param name="dest">Destination buffer.</param>
        /// <param name="offset">Offset into the destination buffer.</param>
        public static int EncodeRune(uint rune, byte[] dest, int offset = 0)
        {
            if (dest == null)
                throw new ArgumentNullException(nameof(dest));
            if (rune <= rune1Max)
            {
                dest[offset] = (byte)rune;
                return 1;
            }
            if (rune <= rune2Max)
            {
                dest[offset++] = (byte)(t2 | (byte)(rune >> 6));
                dest[offset] = (byte)(tx | (byte)(rune & maskx));
                return 2;
            }
            if ((rune > MaxRune) || (surrogateMin <= rune && rune <= surrogateMax))
            {
                // error
                dest[offset++] = 0xef;
                dest[offset++] = 0x3f;
                dest[offset] = 0x3d;
                return 3;
            }
            if (rune <= rune3Max)
            {
                dest[offset++] = (byte)(t3 | (byte)(rune >> 12));
                dest[offset++] = (byte)(tx | (byte)(rune >> 6) & maskx);
                dest[offset] = (byte)(tx | (byte)(rune) & maskx);
                return 3;
            }
            dest[offset++] = (byte)(t4 | (byte)(rune >> 18));
            dest[offset++] = (byte)(tx | (byte)(rune >> 12) & maskx);
            dest[offset++] = (byte)(tx | (byte)(rune >> 6) & maskx);
            dest[offset++] = (byte)(tx | (byte)(rune) & maskx);
            return 4;
        }

        /// <summary>
        /// Returns the number of runes in a utf8 encoded buffer
        /// </summary>
        /// <returns>Numnber of runes.</returns>
        /// <param name="buffer">Byte buffer containing a utf8 string.</param>
        /// <param name="offset">Starting offset in the buffer.</param>
        /// <param name="count">Number of bytes to process in buffer, or -1 to process until the end of the buffer.</param>
        public static int RuneCount(byte[] buffer, int offset = 0, int count = -1)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));
            if (count == -1)
                count = buffer.Length;
            int n = 0;
            for (int i = offset; i < count;)
            {
                n++;
                var c = buffer[i];

                if (c < RuneSelf)
                {
                    // ASCII fast path
                    i++;
                    continue;
                }
                var x = first[c];
                if (x == xx)
                {
                    i++; // invalid.
                    continue;
                }

                var size = (int)(x & 7);

                if (i + size > count)
                {
                    i++; // Short or invalid.
                    continue;
                }
                var accept = AcceptRanges[x >> 4];
                c = buffer[i + 1];
                if (c < accept.Lo || accept.Hi < c)
                {
                    i++;
                    continue;
                }
                if (size == 2)
                {
                    i += 2;
                    continue;
                }
                c = buffer[i + 2];
                if (c < locb || hicb < c)
                {
                    i++;
                    continue;
                }
                if (size == 3)
                {
                    i += 3;
                    continue;
                }
                c = buffer[i + 3];
                if (c < locb || hicb < c)
                {
                    i++;
                    continue;
                }
                i += size;

            }
            return n;
        }

        /// <summary>
        /// Returns the number of runes in a ustring.
        /// </summary>
        /// <returns>Numnber of runes.</returns>
        /// <param name="str">utf8 string.</param>
        public static int RuneCount(ustring str)
        {
            if ((object)str == null)
                throw new ArgumentNullException(nameof(str));
            var count = str.Length;
            int n = 0;
            for (int i = 0; i < count;)
            {
                n++;
                var c = str[i];

                if (c < RuneSelf)
                {
                    // ASCII fast path
                    i++;
                    continue;
                }
                var x = first[c];
                if (x == xx)
                {
                    i++; // invalid.
                    continue;
                }

                var size = (int)(x & 7);

                //if (i <= str.Length - size )
                //{
                //	(Rune rune, _) = Utf8.DecodeRune(str, i, size);
                //	if (rune.IsSurrogatePair)
                //	{
                //		i += size;
                //		continue;
                //	}
                //}

                if (i + size > count)
                {
                    i++; // Short or invalid.
                    continue;
                }
                var accept = AcceptRanges[x >> 4];
                c = str[i + 1];
                if (c < accept.Lo || accept.Hi < c)
                {
                    i++;
                    continue;
                }
                if (size == 2)
                {
                    i += 2;
                    continue;
                }
                c = str[i + 2];
                if (c < locb || hicb < c)
                {
                    i++;
                    continue;
                }
                if (size == 3)
                {
                    i += 3;
                    continue;
                }
                c = str[i + 3];
                if (c < locb || hicb < c)
                {
                    i++;
                    continue;
                }
                i += size;
            }
            return n;
        }

        /// <summary>
        /// Reports whether p consists entirely of valid UTF-8-encoded runes.
        /// </summary>
        /// <param name="buffer">Byte buffer containing a utf8 string.</param>
        public static bool Valid(byte[] buffer)
        {
            return InvalidIndex(buffer) == -1;
        }

        /// <summary>
        /// Reports whether the ustring consists entirely of valid UTF-8-encoded runes.
        /// </summary>
        /// <param name="str">String to validate.</param>
        public static bool Valid(ustring str)
        {
            return InvalidIndex(str) == -1;
        }

        /// <summary>
        /// Use to find the index of the first invalid utf8 byte sequence in a buffer
        /// </summary>
        /// <returns>The index of the first insvalid byte sequence or -1 if the entire buffer is valid.</returns>
        /// <param name="buffer">Buffer containing the utf8 buffer.</param>
        public static int InvalidIndex(byte[] buffer)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));
            var n = buffer.Length;

            for (int i = 0; i < n;)
            {
                var pi = buffer[i];

                if (pi < RuneSelf)
                {
                    i++;
                    continue;
                }
                var x = first[pi];
                if (x == xx)
                    return i; // Illegal starter byte.
                var size = (int)(x & 7);
                if (i + size > n)
                    return i; // Short or invalid.
                var accept = AcceptRanges[x >> 4];

                var c = buffer[i + 1];

                if (c < accept.Lo || accept.Hi < c)
                    return i;

                if (size == 2)
                {
                    i += 2;
                    continue;
                }
                c = buffer[i + 2];
                if (c < locb || hicb < c)
                    return i;
                if (size == 3)
                {
                    i += 3;
                    continue;
                }
                c = buffer[i + 3];
                if (c < locb || hicb < c)
                    return i;
                i += size;
            }
            return -1;
        }

        /// <summary>
        /// Use to find the index of the first invalid utf8 byte sequence in a buffer
        /// </summary>
        /// <returns>The index of the first insvalid byte sequence or -1 if the entire buffer is valid.</returns>
        /// <param name="str">String containing the utf8 buffer.</param>
        public static int InvalidIndex(ustring str)
        {
            if ((object)str == null)
                throw new ArgumentNullException(nameof(str));
            var n = str.Length;

            for (int i = 0; i < n;)
            {
                var pi = str[i];

                if (pi < RuneSelf)
                {
                    i++;
                    continue;
                }
                var x = first[pi];
                if (x == xx)
                    return i; // Illegal starter byte.
                var size = (int)(x & 7);
                if (i + size > n)
                    return i; // Short or invalid.
                var accept = AcceptRanges[x >> 4];

                var c = str[i + 1];

                if (c < accept.Lo || accept.Hi < c)
                    return i;

                if (size == 2)
                {
                    i += 2;
                    continue;
                }
                c = str[i + 2];
                if (c < locb || hicb < c)
                    return i;
                if (size == 3)
                {
                    i += 3;
                    continue;
                }
                c = str[i + 3];
                if (c < locb || hicb < c)
                    return i;
                i += size;
            }
            return -1;
        }

        /// <summary>
        ///  ValidRune reports whether a rune can be legally encoded as UTF-8.
        /// </summary>
        /// <returns><c>true</c>, if rune is valid, <c>false</c> otherwise.</returns>
        /// <param name="rune">The rune to test.</param>
        public static bool ValidRune(uint rune)
        {
            if (rune < surrogateMin)
                return true;
            if (surrogateMax < rune && rune <= MaxRune)
                return true;
            return false;
        }
    }
}
//=======================================================================
// Contains a port of this code:
// https://www.cl.cam.ac.uk/%7Emgk25/ucs/wcwidth.c

namespace System
{
    public partial struct Rune
    {
        static uint[,] combining = new uint[,] {
            { 0x0300, 0x036F }, { 0x0483, 0x0486 }, { 0x0488, 0x0489 },
            { 0x0591, 0x05BD }, { 0x05BF, 0x05BF }, { 0x05C1, 0x05C2 },
            { 0x05C4, 0x05C5 }, { 0x05C7, 0x05C7 }, { 0x0600, 0x0603 },
            { 0x0610, 0x0615 }, { 0x064B, 0x065E }, { 0x0670, 0x0670 },
            { 0x06D6, 0x06E4 }, { 0x06E7, 0x06E8 }, { 0x06EA, 0x06ED },
            { 0x070F, 0x070F }, { 0x0711, 0x0711 }, { 0x0730, 0x074A },
            { 0x07A6, 0x07B0 }, { 0x07EB, 0x07F3 }, { 0x0901, 0x0902 },
            { 0x093C, 0x093C }, { 0x0941, 0x0948 }, { 0x094D, 0x094D },
            { 0x0951, 0x0954 }, { 0x0962, 0x0963 }, { 0x0981, 0x0981 },
            { 0x09BC, 0x09BC }, { 0x09C1, 0x09C4 }, { 0x09CD, 0x09CD },
            { 0x09E2, 0x09E3 }, { 0x0A01, 0x0A02 }, { 0x0A3C, 0x0A3C },
            { 0x0A41, 0x0A42 }, { 0x0A47, 0x0A48 }, { 0x0A4B, 0x0A4D },
            { 0x0A70, 0x0A71 }, { 0x0A81, 0x0A82 }, { 0x0ABC, 0x0ABC },
            { 0x0AC1, 0x0AC5 }, { 0x0AC7, 0x0AC8 }, { 0x0ACD, 0x0ACD },
            { 0x0AE2, 0x0AE3 }, { 0x0B01, 0x0B01 }, { 0x0B3C, 0x0B3C },
            { 0x0B3F, 0x0B3F }, { 0x0B41, 0x0B43 }, { 0x0B4D, 0x0B4D },
            { 0x0B56, 0x0B56 }, { 0x0B82, 0x0B82 }, { 0x0BC0, 0x0BC0 },
            { 0x0BCD, 0x0BCD }, { 0x0C3E, 0x0C40 }, { 0x0C46, 0x0C48 },
            { 0x0C4A, 0x0C4D }, { 0x0C55, 0x0C56 }, { 0x0CBC, 0x0CBC },
            { 0x0CBF, 0x0CBF }, { 0x0CC6, 0x0CC6 }, { 0x0CCC, 0x0CCD },
            { 0x0CE2, 0x0CE3 }, { 0x0D41, 0x0D43 }, { 0x0D4D, 0x0D4D },
            { 0x0DCA, 0x0DCA }, { 0x0DD2, 0x0DD4 }, { 0x0DD6, 0x0DD6 },
            { 0x0E31, 0x0E31 }, { 0x0E34, 0x0E3A }, { 0x0E47, 0x0E4E },
            { 0x0EB1, 0x0EB1 }, { 0x0EB4, 0x0EB9 }, { 0x0EBB, 0x0EBC },
            { 0x0EC8, 0x0ECD }, { 0x0F18, 0x0F19 }, { 0x0F35, 0x0F35 },
            { 0x0F37, 0x0F37 }, { 0x0F39, 0x0F39 }, { 0x0F71, 0x0F7E },
            { 0x0F80, 0x0F84 }, { 0x0F86, 0x0F87 }, { 0x0F90, 0x0F97 },
            { 0x0F99, 0x0FBC }, { 0x0FC6, 0x0FC6 }, { 0x102D, 0x1030 },
            { 0x1032, 0x1032 }, { 0x1036, 0x1037 }, { 0x1039, 0x1039 },
            { 0x1058, 0x1059 }, { 0x1160, 0x11FF }, { 0x135F, 0x135F },
            { 0x1712, 0x1714 }, { 0x1732, 0x1734 }, { 0x1752, 0x1753 },
            { 0x1772, 0x1773 }, { 0x17B4, 0x17B5 }, { 0x17B7, 0x17BD },
            { 0x17C6, 0x17C6 }, { 0x17C9, 0x17D3 }, { 0x17DD, 0x17DD },
            { 0x180B, 0x180D }, { 0x18A9, 0x18A9 }, { 0x1920, 0x1922 },
            { 0x1927, 0x1928 }, { 0x1932, 0x1932 }, { 0x1939, 0x193B },
            { 0x1A17, 0x1A18 }, { 0x1B00, 0x1B03 }, { 0x1B34, 0x1B34 },
            { 0x1B36, 0x1B3A }, { 0x1B3C, 0x1B3C }, { 0x1B42, 0x1B42 },
            { 0x1B6B, 0x1B73 }, { 0x1DC0, 0x1DCA }, { 0x1DFE, 0x1DFF },
            { 0x200B, 0x200F }, { 0x202A, 0x202E }, { 0x2060, 0x2063 },
            { 0x206A, 0x206F }, { 0x20D0, 0x20EF }, { 0x2E9A, 0x2E9A },
            { 0x2EF4, 0x2EFF }, { 0x2FD6, 0x2FEF }, { 0x2FFC, 0x2FFF },
            { 0x31E4, 0x31EF }, { 0x321F, 0x321F }, { 0xA48D, 0xA48F },
            { 0xA806, 0xA806 }, { 0xA80B, 0xA80B }, { 0xA825, 0xA826 },
            { 0xFB1E, 0xFB1E }, { 0xFE00, 0xFE0F }, { 0xFE1A, 0xFE1F },
            { 0xFE20, 0xFE23 }, { 0xFE53, 0xFE53 }, { 0xFE67, 0xFE67 },
            { 0xFEFF, 0xFEFF }, { 0xFFF9, 0xFFFB },
        };

        static uint[,] combiningWideChars = new uint[,] {
			/* Hangul Jamo init. consonants - 0x1100, 0x11ff */
			/* Miscellaneous Technical - 0x2300, 0x23ff */
			/* Hangul Syllables - 0x11a8, 0x11c2 */
			/* CJK Compatibility Ideographs - f900, fad9 */
			/* Vertical forms - fe10, fe19 */
			/* CJK Compatibility Forms - fe30, fe4f */
			/* Fullwidth Forms - ff01, ffee */
			/* Alphabetic Presentation Forms - 0xFB00, 0xFb4f */
			/* Chess Symbols - 0x1FA00, 0x1FA0f */

			{ 0x1100, 0x115f }, { 0x231a, 0x231b }, { 0x2329, 0x232a },
            { 0x23e9, 0x23ec }, { 0x23f0, 0x23f0 }, { 0x23f3, 0x23f3 },
            { 0x25fd, 0x25fe }, { 0x2614, 0x2615 }, { 0x2648, 0x2653 },
            { 0x267f, 0x267f }, { 0x2693, 0x2693 }, { 0x26a1, 0x26a1 },
            { 0x26aa, 0x26ab }, { 0x26bd, 0x26be }, { 0x26c4, 0x26c5 },
            { 0x26ce, 0x26ce }, { 0x26d4, 0x26d4 }, { 0x26ea, 0x26ea },
            { 0x26f2, 0x26f3 }, { 0x26f5, 0x26f5 }, { 0x26fa, 0x26fa },
            { 0x26fd, 0x26fd }, { 0x2705, 0x2705 }, { 0x270a, 0x270b },
            { 0x2728, 0x2728 }, { 0x274c, 0x274c }, { 0x274e, 0x274e },
            { 0x2753, 0x2755 }, { 0x2757, 0x2757 }, { 0x2795, 0x2797 },
            { 0x27b0, 0x27b0 }, { 0x27bf, 0x27bf }, { 0x2b1b, 0x2b1c },
            { 0x2b50, 0x2b50 }, { 0x2b55, 0x2b55 }, { 0x2e80, 0x303e },
            { 0x3041, 0x3096 }, { 0x3099, 0x30ff }, { 0x3105, 0x312f },
            { 0x3131, 0x318e }, { 0x3190, 0x3247 }, { 0x3250, 0x4dbf },
            { 0x4e00, 0xa4c6 }, { 0xa960, 0xa97c }, { 0xac00 ,0xd7a3 },
            { 0xf900, 0xfaff }, { 0xfe10, 0xfe1f }, { 0xfe30 ,0xfe6b },
            { 0xff01, 0xff60 }, { 0xffe0, 0xffe6 }, { 0x10000, 0x10ffff }
        };

        static int bisearch(uint rune, uint[,] table, int max)
        {
            int min = 0;
            int mid;

            if (rune < table[0, 0] || rune > table[max, 1])
                return 0;
            while (max >= min)
            {
                mid = (min + max) / 2;
                if (rune > table[mid, 1])
                    min = mid + 1;
                else if (rune < table[mid, 0])
                    max = mid - 1;
                else
                    return 1;
            }

            return 0;
        }

        //static bool bisearch(uint rune, uint[,] table, out int width)
        //{
        //	width = 0;
        //	var length = table.GetLength(0);
        //	if (length == 0 || rune < table[0, 0] || rune > table[length - 1, 1])
        //		return false;

        //	for (int i = 0; i < length; i++)
        //	{
        //		if (rune >= table[i, 0] && rune <= table[i, 1])
        //		{
        //			width = (int)table[i, 2];
        //			return true;
        //		}
        //	}

        //	return false;
        //}

        /// <summary>
        /// Check if the rune is a non-spacing character.
        /// </summary>
        /// <param name="rune">The rune.</param>
        /// <returns>True if is a non-spacing character, false otherwise.</returns>
        public static bool IsNonSpacingChar(uint rune)
        {
            return bisearch(rune, combining, combining.GetLength(0) - 1) != 0;
        }

        /// <summary>
        /// Check if the rune is a wide character.
        /// </summary>
        /// <param name="rune">The rune.</param>
        /// <returns>True if is a wide character, false otherwise.</returns>
        public static bool IsWideChar(uint rune)
        {
            return bisearch(rune, combiningWideChars, combiningWideChars.GetLength(0) - 1) != 0;
        }

        /// <summary>
        /// Number of column positions of a wide-character code.   This is used to measure runes as displayed by text-based terminals.
        /// </summary>
        /// <returns>The width in columns, 0 if the argument is the null character, -1 if the value is not printable, otherwise the number of columns that the rune occupies.</returns>
        /// <param name="rune">The rune.</param>
        public static int ColumnWidth(Rune rune)
        {
            uint irune = (uint)rune;
            if (irune < 0x20 || (irune >= 0x7f && irune < 0xa0))
                return -1;
            if (irune < 0x7f)
                return 1;
            /* binary search in table of non-spacing characters */
            if (bisearch(irune, combining, combining.GetLength(0) - 1) != 0)
                return 0;
            /* if we arrive here, ucs is not a combining or C0/C1 control character */
            return 1 +
                (bisearch(irune, combiningWideChars, combiningWideChars.GetLength(0) - 1) != 0 ? 1 : 0);
        }
    }
    //=======================================================================
    /// <summary>
    /// A Rune represents a Unicode CodePoint storing the contents in a 32-bit value
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    public partial struct Rune
    {
        // Stores the rune
        uint value;

        /// <summary>
        /// Gets the rune unsigned integer value.
        /// </summary>
        public uint Value => value;

        /// <summary>
        /// The "error" Rune or "Unicode replacement character"
        /// </summary>
        public static Rune Error = new Rune(0xfffd);

        /// <summary>
        /// Maximum valid Unicode code point.
        /// </summary>
        public static Rune MaxRune = new Rune(0x10ffff);

        /// <summary>
        /// Characters below RuneSelf are represented as themselves in a single byte
        /// </summary>
        public const byte RuneSelf = 0x80;

        /// <summary>
        /// Represents invalid code points.
        /// </summary>
        public static Rune ReplacementChar = new Rune(0xfffd);

        /// <summary>
        /// Maximum number of bytes required to encode every unicode code point.
        /// </summary>
        public const int Utf8Max = 4;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Rune"/> from a unsigned integer.
        /// </summary>
        /// <param name="rune">Unsigned integer.</param>
        /// <remarks>
        /// The value does not have to be a valid Unicode code point, this API
        /// will create an instance of Rune regardless of the whether it is in 
        /// range or not.
        /// </remarks>
        public Rune(uint rune)
        {
            if (rune > maxRune)
            {
                throw new ArgumentOutOfRangeException("Value is beyond the supplementary range!");
            }
            this.value = rune;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Rune"/> from a character value.
        /// </summary>
        /// <param name="ch">C# characters.</param>
        public Rune(char ch)
        {
            this.value = (uint)ch;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Rune"/> from a surrogate pair value.
        /// </summary>
        /// <param name="highSurrogate">The high surrogate code point.</param>
        /// <param name="lowSurrogate">The low surrogate code point.</param>
        public Rune(uint highSurrogate, uint lowSurrogate)
        {
            if (EncodeSurrogatePair(highSurrogate, lowSurrogate, out Rune rune))
            {
                this.value = rune;
            }
            else if (highSurrogate < highSurrogateMin || lowSurrogate > lowSurrogateMax)
            {
                throw new ArgumentOutOfRangeException($"Must be between {highSurrogateMin:x} and {lowSurrogateMax:x} inclusive!");
            }
            else
            {
                throw new ArgumentOutOfRangeException($"Resulted rune must be less or equal to {(uint)MaxRune:x}!");
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:System.Rune"/> can be encoded as UTF-8
        /// </summary>
        /// <value><c>true</c> if is valid; otherwise, <c>false</c>.</value>
        public bool IsValid => ValidRune(value);

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:System.Rune"/> is a surrogate code point.
        /// </summary>
        /// <returns><c>true</c>If is a surrogate code point, <c>false</c>otherwise.</returns>
        public bool IsSurrogate => IsSurrogateRune(value);

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:System.Rune"/> is a valid surrogate pair.
        /// </summary>
        /// <returns><c>true</c>If is a valid surrogate pair, <c>false</c>otherwise.</returns>
        public bool IsSurrogatePair => DecodeSurrogatePair(value, out _);

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:System.Rune"/> is a high surrogate.
        /// </summary>
        public bool IsHighSurrogate => value >= highSurrogateMin && value <= highSurrogateMax;

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:System.Rune"/> is a low surrogate.
        /// </summary>
        public bool IsLowSurrogate => value >= lowSurrogateMin && value <= lowSurrogateMax;

        /// <summary>
        /// Check if the rune is a non-spacing character.
        /// </summary>
        /// <returns>True if is a non-spacing character, false otherwise.</returns>
        public bool IsNonSpacing => IsNonSpacingChar(value);

        // Code points in the surrogate range are not valid for UTF-8.
        const uint highSurrogateMin = 0xd800;
        const uint highSurrogateMax = 0xdbff;
        const uint lowSurrogateMin = 0xdc00;
        const uint lowSurrogateMax = 0xdfff;

        const uint maxRune = 0x10ffff;

        const byte t1 = 0x00; // 0000 0000
        const byte tx = 0x80; // 1000 0000
        const byte t2 = 0xC0; // 1100 0000
        const byte t3 = 0xE0; // 1110 0000
        const byte t4 = 0xF0; // 1111 0000
        const byte t5 = 0xF8; // 1111 1000

        const byte maskx = 0x3F; // 0011 1111
        const byte mask2 = 0x1F; // 0001 1111
        const byte mask3 = 0x0F; // 0000 1111
        const byte mask4 = 0x07; // 0000 0111

        const uint rune1Max = (1 << 7) - 1;
        const uint rune2Max = (1 << 11) - 1;
        const uint rune3Max = (1 << 16) - 1;

        // The default lowest and highest continuation byte.
        const byte locb = 0x80; // 1000 0000
        const byte hicb = 0xBF; // 1011 1111

        // These names of these constants are chosen to give nice alignment in the
        // table below. The first nibble is an index into acceptRanges or F for
        // special one-byte ca1es. The second nibble is the Rune length or the
        // Status for the special one-byte ca1e.
        const byte xx = 0xF1; // invalid: size 1
        const byte a1 = 0xF0; // a1CII: size 1
        const byte s1 = 0x02; // accept 0, size 2
        const byte s2 = 0x13; // accept 1, size 3
        const byte s3 = 0x03; // accept 0, size 3
        const byte s4 = 0x23; // accept 2, size 3
        const byte s5 = 0x34; // accept 3, size 4
        const byte s6 = 0x04; // accept 0, size 4
        const byte s7 = 0x44; // accept 4, size 4

        static byte[] first = new byte[256]{
			//   1   2   3   4   5   6   7   8   9   A   B   C   D   E   F
			a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, // 0x00-0x0F
			a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, // 0x10-0x1F
			a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, // 0x20-0x2F
			a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, // 0x30-0x3F
			a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, // 0x40-0x4F
			a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, // 0x50-0x5F
			a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, // 0x60-0x6F
			a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, a1, // 0x70-0x7F

			//   1   2   3   4   5   6   7   8   9   A   B   C   D   E   F
			xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, // 0x80-0x8F
			xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, // 0x90-0x9F
			xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, // 0xA0-0xAF
			xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, // 0xB0-0xBF
			xx, xx, s1, s1, s1, s1, s1, s1, s1, s1, s1, s1, s1, s1, s1, s1, // 0xC0-0xCF
			s1, s1, s1, s1, s1, s1, s1, s1, s1, s1, s1, s1, s1, s1, s1, s1, // 0xD0-0xDF
			s2, s3, s3, s3, s3, s3, s3, s3, s3, s3, s3, s3, s3, s4, s3, s3, // 0xE0-0xEF
			s5, s6, s6, s6, s7, xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, xx, // 0xF0-0xFF
		};

        struct AcceptRange
        {
            public byte Lo, Hi;
            public AcceptRange(byte lo, byte hi)
            {
                Lo = lo;
                Hi = hi;
            }
        }

        static AcceptRange[] AcceptRanges = new AcceptRange[] {
            new AcceptRange (locb, hicb),
            new AcceptRange (0xa0, hicb),
            new AcceptRange (locb, 0x9f),
            new AcceptRange (0x90, hicb),
            new AcceptRange (locb, 0x8f),
        };

        /// <summary>
        /// FullRune reports whether the bytes in p begin with a full UTF-8 encoding of a rune.
        /// An invalid encoding is considered a full Rune since it will convert as a width-1 error rune.
        /// </summary>
        /// <returns><c>true</c>, if the bytes in p begin with a full UTF-8 encoding of a rune, <c>false</c> otherwise.</returns>
        /// <param name="p">byte array.</param>
        public static bool FullRune(byte[] p)
        {
            if (p == null)
                throw new ArgumentNullException(nameof(p));
            var n = p.Length;

            if (n == 0)
                return false;
            var x = first[p[0]];
            if (n >= (x & 7))
            {
                // ascii, invalid or valid
                return true;
            }
            // must be short or invalid
            if (n > 1)
            {
                var accept = AcceptRanges[x >> 4];
                var c = p[1];
                if (c < accept.Lo || accept.Hi < c)
                    return true;
                else if (n > 2 && (p[2] < locb || hicb < p[2]))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// DecodeRune unpacks the first UTF-8 encoding in p and returns the rune and
        /// its width in bytes. 
        /// </summary>
        /// <returns>If p is empty it returns (RuneError, 0). Otherwise, if
        /// the encoding is invalid, it returns (RuneError, 1). Both are impossible
        /// results for correct, non-empty UTF-8.
        /// </returns>
        /// <param name="buffer">Byte buffer containing the utf8 string.</param>
        /// <param name="start">Starting offset to look into..</param>
        /// <param name="n">Number of bytes valid in the buffer, or -1 to make it the length of the buffer.</param>
        public static (Rune rune, int Size) DecodeRune(byte[] buffer, int start = 0, int n = -1)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));
            if (start < 0)
                throw new ArgumentException("invalid offset", nameof(start));
            if (n < 0)
                n = buffer.Length - start;
            if (start > buffer.Length - n)
                throw new ArgumentException("Out of bounds");

            if (n < 1)
                return (Error, 0);

            var p0 = buffer[start];
            var x = first[p0];
            if (x >= a1)
            {
                // The following code simulates an additional check for x == xx and
                // handling the ASCII and invalid cases accordingly. This mask-and-or
                // approach prevents an additional branch.
                uint mask = (uint)((((byte)x) << 31) >> 31); // Create 0x0000 or 0xFFFF.
                return (new Rune((buffer[start]) & ~mask | Error.value & mask), 1);
            }

            var sz = x & 7;
            var accept = AcceptRanges[x >> 4];
            if (n < (int)sz)
                return (Error, 1);

            var b1 = buffer[start + 1];
            if (b1 < accept.Lo || accept.Hi < b1)
                return (Error, 1);

            if (sz == 2)
                return (new Rune((uint)((p0 & mask2)) << 6 | (uint)((b1 & maskx))), 2);

            var b2 = buffer[start + 2];
            if (b2 < locb || hicb < b2)
                return (Error, 1);

            if (sz == 3)
                return (new Rune((uint)((p0 & mask3)) << 12 | (uint)((b1 & maskx)) << 6 | (uint)((b2 & maskx))), 3);

            var b3 = buffer[start + 3];
            if (b3 < locb || hicb < b3)
            {
                return (Error, 1);
            }
            return (new Rune((uint)(p0 & mask4) << 18 | (uint)(b1 & maskx) << 12 | (uint)(b2 & maskx) << 6 | (uint)(b3 & maskx)), 4);
        }


        // RuneStart reports whether the byte could be the first byte of an encoded,
        // possibly invalid rune. Second and subsequent bytes always have the top two
        // bits set to 10.
        static bool RuneStart(byte b) => (b & 0xc0) != 0x80;

        /// <summary>
        /// DecodeLastRune unpacks the last UTF-8 encoding in buffer
        /// </summary>
        /// <returns>The last rune and its width in bytes.</returns>
        /// <param name="buffer">Buffer to decode rune from;   if it is empty,
        /// it returns (RuneError, 0). Otherwise, if
        /// the encoding is invalid, it returns (RuneError, 1). Both are impossible
        /// results for correct, non-empty UTF-8.</param>
        /// <param name="end">Scan up to that point, if the value is -1, it sets the value to the length of the buffer.</param>
        /// <remarks>
        /// An encoding is invalid if it is incorrect UTF-8, encodes a rune that is
        /// out of range, or is not the shortest possible UTF-8 encoding for the
        /// value. No other validation is performed.</remarks> 
        public static (Rune rune, int size) DecodeLastRune(byte[] buffer, int end = -1)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));
            if (buffer.Length == 0)
                return (Error, 0);
            if (end == -1)
                end = buffer.Length;
            else if (end > buffer.Length)
                throw new ArgumentException("The end goes beyond the size of the buffer");

            var start = end - 1;
            uint r = buffer[start];
            if (r < RuneSelf)
                return (new Rune(r), 1);
            // guard against O(n^2) behavior when traversing
            // backwards through strings with long sequences of
            // invalid UTF-8.
            var lim = end - Utf8Max;

            if (lim < 0)
                lim = 0;

            for (start--; start >= lim; start--)
            {
                if (RuneStart(buffer[start]))
                {
                    break;
                }
            }
            if (start < 0)
                start = 0;
            int size;
            Rune r2;
            (r2, size) = DecodeRune(buffer, start, end - start);
            if (start + size != end)
                return (Error, 1);
            return (r2, size);
        }

        /// <summary>
        /// number of bytes required to encode the rune.
        /// </summary>
        /// <returns>The length, or -1 if the rune is not a valid value to encode in UTF-8.</returns>
        /// <param name="rune">Rune to probe.</param>
        public static int RuneLen(Rune rune)
        {
            var rvalue = rune.value;
            if (rvalue <= rune1Max)
                return 1;
            if (rvalue <= rune2Max)
                return 2;
            if (highSurrogateMin <= rvalue && rvalue <= lowSurrogateMax)
                return -1;
            if (rvalue <= rune3Max)
                return 3;
            if (rvalue <= MaxRune.value)
                return 4;
            return -1;
        }

        /// <summary>
        /// Writes into the destination buffer starting at offset the UTF8 encoded version of the rune
        /// </summary>
        /// <returns>The number of bytes written into the destination buffer.</returns>
        /// <param name="rune">Rune to encode.</param>
        /// <param name="dest">Destination buffer.</param>
        /// <param name="offset">Offset into the destination buffer.</param>
        public static int EncodeRune(Rune rune, byte[] dest, int offset = 0)
        {
            if (dest == null)
                throw new ArgumentNullException(nameof(dest));
            var runeValue = rune.value;
            if (runeValue <= rune1Max)
            {
                dest[offset] = (byte)runeValue;
                return 1;
            }
            if (runeValue <= rune2Max)
            {
                dest[offset++] = (byte)(t2 | (byte)(runeValue >> 6));
                dest[offset] = (byte)(tx | (byte)(runeValue & maskx));
                return 2;
            }
            if ((runeValue > MaxRune.value) || (highSurrogateMin <= runeValue && runeValue <= lowSurrogateMax))
            {
                // error
                dest[offset++] = 0xef;
                dest[offset++] = 0x3f;
                dest[offset] = 0x3d;
                return 3;
            }
            if (runeValue <= rune3Max)
            {
                dest[offset++] = (byte)(t3 | (byte)(runeValue >> 12));
                dest[offset++] = (byte)(tx | (byte)(runeValue >> 6) & maskx);
                dest[offset] = (byte)(tx | (byte)(runeValue) & maskx);
                return 3;
            }
            dest[offset++] = (byte)(t4 | (byte)(runeValue >> 18));
            dest[offset++] = (byte)(tx | (byte)(runeValue >> 12) & maskx);
            dest[offset++] = (byte)(tx | (byte)(runeValue >> 6) & maskx);
            dest[offset++] = (byte)(tx | (byte)(runeValue) & maskx);
            return 4;
        }

        /// <summary>
        /// Returns the number of runes in a utf8 encoded buffer
        /// </summary>
        /// <returns>Number of runes.</returns>
        /// <param name="buffer">Byte buffer containing a utf8 string.</param>
        /// <param name="offset">Starting offset in the buffer.</param>
        /// <param name="count">Number of bytes to process in buffer, or -1 to process until the end of the buffer.</param>
        public static int RuneCount(byte[] buffer, int offset = 0, int count = -1)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));
            if (count == -1)
                count = buffer.Length;
            int n = 0;
            for (int i = offset; i < count;)
            {
                n++;
                var c = buffer[i];

                if (c < RuneSelf)
                {
                    // ASCII fast path
                    i++;
                    continue;
                }
                var x = first[c];
                if (x == xx)
                {
                    i++; // invalid.
                    continue;
                }

                var size = (int)(x & 7);

                if (i + size > count)
                {
                    i++; // Short or invalid.
                    continue;
                }
                var accept = AcceptRanges[x >> 4];
                c = buffer[i + 1];
                if (c < accept.Lo || accept.Hi < c)
                {
                    i++;
                    continue;
                }
                if (size == 2)
                {
                    i += 2;
                    continue;
                }
                c = buffer[i + 2];
                if (c < locb || hicb < c)
                {
                    i++;
                    continue;
                }
                if (size == 3)
                {
                    i += 3;
                    continue;
                }
                c = buffer[i + 3];
                if (c < locb || hicb < c)
                {
                    i++;
                    continue;
                }
                i += size;

            }
            return n;
        }

        /// <summary>
        /// Reports whether p consists entirely of valid UTF-8-encoded runes.
        /// </summary>
        /// <param name="buffer">Byte buffer containing a utf8 string.</param>
        public static bool Valid(byte[] buffer)
        {
            return InvalidIndex(buffer) == -1;
        }

        /// <summary>
        /// Use to find the index of the first invalid utf8 byte sequence in a buffer
        /// </summary>
        /// <returns>The index of the first invalid byte sequence or -1 if the entire buffer is valid.</returns>
        /// <param name="buffer">Buffer containing the utf8 buffer.</param>
        public static int InvalidIndex(byte[] buffer)
        {
            if (buffer == null)
                throw new ArgumentNullException(nameof(buffer));
            var n = buffer.Length;

            for (int i = 0; i < n;)
            {
                var pi = buffer[i];

                if (pi < RuneSelf)
                {
                    i++;
                    continue;
                }
                var x = first[pi];
                if (x == xx)
                    return i; // Illegal starter byte.
                var size = (int)(x & 7);
                if (i + size > n)
                    return i; // Short or invalid.
                var accept = AcceptRanges[x >> 4];

                var c = buffer[i + 1];

                if (c < accept.Lo || accept.Hi < c)
                    return i;

                if (size == 2)
                {
                    i += 2;
                    continue;
                }
                c = buffer[i + 2];
                if (c < locb || hicb < c)
                    return i;
                if (size == 3)
                {
                    i += 3;
                    continue;
                }
                c = buffer[i + 3];
                if (c < locb || hicb < c)
                    return i;
                i += size;
            }
            return -1;
        }

        /// <summary>
        ///  ValidRune reports whether a rune can be legally encoded as UTF-8.
        /// </summary>
        /// <returns><c>true</c>, if rune was validated, <c>false</c> otherwise.</returns>
        /// <param name="rune">The rune to test.</param>
        public static bool ValidRune(Rune rune)
        {
            if ((0 <= (int)rune.value && rune.value < highSurrogateMin) ||
                (lowSurrogateMax < rune.value && rune.value <= MaxRune.value))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Reports whether a rune is a surrogate code point.
        /// </summary>
        /// <param name="rune">The rune.</param>
        /// <returns><c>true</c>If is a surrogate code point, <c>false</c>otherwise.</returns>
        public static bool IsSurrogateRune(uint rune)
        {
            return rune >= highSurrogateMin && rune <= lowSurrogateMax;
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="T:System.Rune"/> can be encoded as UTF-16 from a surrogate pair or zero otherwise.
        /// </summary>
        /// <param name="highsurrogate">The high surrogate code point.</param>
        /// <param name="lowSurrogate">The low surrogate code point.</param>
        /// <param name="rune">The returning rune.</param>
        /// <returns><c>True</c>if the returning rune is greater than 0 <c>False</c>otherwise.</returns>
        public static bool EncodeSurrogatePair(uint highsurrogate, uint lowSurrogate, out Rune rune)
        {
            rune = 0;
            if (highsurrogate >= highSurrogateMin && highsurrogate <= highSurrogateMax &&
                lowSurrogate >= lowSurrogateMin && lowSurrogate <= lowSurrogateMax)
            {
                //return 0x10000 + ((highsurrogate - highSurrogateMin) * 0x0400) + (lowSurrogate - lowSurrogateMin);
                return (rune = 0x10000 + ((highsurrogate - highSurrogateMin) << 10) + (lowSurrogate - lowSurrogateMin)) > 0;
            }
            return false;
        }

        /// <summary>
        /// Reports whether this <see cref="T:System.Rune"/> is a valid surrogate pair and can be decoded from UTF-16.
        /// </summary>
        /// <param name="rune">The rune</param>
        /// <param name="chars">The chars if is valid. Empty otherwise.</param>
        /// <returns><c>true</c>If is a valid surrogate pair, <c>false</c>otherwise.</returns>
        public static bool DecodeSurrogatePair(uint rune, out char[] chars)
        {
            uint s = rune - 0x10000;
            uint h = highSurrogateMin + (s >> 10);
            uint l = lowSurrogateMin + (s & 0x3FF);

            if (EncodeSurrogatePair(h, l, out Rune dsp) && dsp == rune)
            {
                chars = new char[] { (char)h, (char)l };
                return true;
            }
            chars = null;
            return false;
        }

        /// <summary>
        /// Reports whether this <see cref="T:System.Rune"/> is a valid surrogate pair and can be decoded from UTF-16.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="chars">The chars if is valid. Empty otherwise.</param>
        /// <returns><c>true</c>If is a valid surrogate pair, <c>false</c>otherwise.</returns>
        public static bool DecodeSurrogatePair(string str, out char[] chars)
        {
            if (str.Length == 2)
            {
                chars = str.ToCharArray();
                if (EncodeSurrogatePair(chars[0], chars[1], out _))
                {
                    return true;
                }
            }
            chars = null;
            return false;
        }

        /// <summary>
        /// Given one byte from a utf8 string, return the number of expected bytes that make up the sequence.
        /// </summary>
        /// <returns>The number of UTF8 bytes expected given the first prefix.</returns>
        /// <param name="firstByte">Is the first byte of a UTF8 sequence.</param>
        public static int ExpectedSizeFromFirstByte(byte firstByte)
        {
            var x = first[firstByte];

            // Invalid runes, just return 1 for byte, and let higher level pass to print
            if (x == xx)
                return -1;
            if (x == a1)
                return 1;
            return x & 0xf;
        }

        /// <summary>
        /// IsDigit reports whether the rune is a decimal digit.
        /// </summary>
        /// <returns><c>true</c>, if the rune is a mark, <c>false</c> otherwise.</returns>
        /// <param name="rune">The rune to test for.</param>
        public static bool IsDigit(Rune rune) => NStack.Unicode.IsDigit(rune.value);

        /// <summary>
        /// IsGraphic reports whether the rune is defined as a Graphic by Unicode.
        /// </summary>
        /// <returns><c>true</c>, if the rune is a lower case letter, <c>false</c> otherwise.</returns>
        /// <param name="rune">The rune to test for.</param>
        /// <remarks>
        /// Such characters include letters, marks, numbers, punctuation, symbols, and
        /// spaces, from categories L, M, N, P, S, Zs.
        /// </remarks>
        public static bool IsGraphic(Rune rune) => NStack.Unicode.IsGraphic(rune.value);

        /// <summary>
        /// IsPrint reports whether the rune is defined as printable.
        /// </summary>
        /// <returns><c>true</c>, if the rune is a lower case letter, <c>false</c> otherwise.</returns>
        /// <param name="rune">The rune to test for.</param>
        /// <remarks>
        /// Such characters include letters, marks, numbers, punctuation, symbols, and the
        /// ASCII space character, from categories L, M, N, P, S and the ASCII space
        /// character. This categorization is the same as IsGraphic except that the
        /// only spacing character is ASCII space, U+0020.
        /// </remarks>
        public static bool IsPrint(Rune rune) => NStack.Unicode.IsPrint(rune.value);


        /// <summary>
        /// IsControl reports whether the rune is a control character.
        /// </summary>
        /// <returns><c>true</c>, if the rune is a lower case letter, <c>false</c> otherwise.</returns>
        /// <param name="rune">The rune to test for.</param>
        /// <remarks>
        /// The C (Other) Unicode category includes more code points such as surrogates; use C.InRange (r) to test for them.
        /// </remarks>
        public static bool IsControl(Rune rune) => NStack.Unicode.IsControl(rune.value);

        /// <summary>
        /// IsLetter reports whether the rune is a letter (category L).
        /// </summary>
        /// <returns><c>true</c>, if the rune is a letter, <c>false</c> otherwise.</returns>
        /// <param name="rune">The rune to test for.</param>
        /// <remarks>
        /// </remarks>
        public static bool IsLetter(Rune rune) => NStack.Unicode.IsLetter(rune.value);

        /// <summary>
        /// IsLetterOrDigit reports whether the rune is a letter (category L) or a digit.
        /// </summary>
        /// <returns><c>true</c>, if the rune is a letter or digit, <c>false</c> otherwise.</returns>
        /// <param name="rune">The rune to test for.</param>
        /// <remarks>
        /// </remarks>
        public static bool IsLetterOrDigit(Rune rune) => NStack.Unicode.IsLetter(rune.value) || NStack.Unicode.IsDigit(rune.value);

        /// <summary>
        /// IsLetterOrDigit reports whether the rune is a letter (category L) or a number (category N).
        /// </summary>
        /// <returns><c>true</c>, if the rune is a letter or number, <c>false</c> otherwise.</returns>
        /// <param name="rune">The rune to test for.</param>
        /// <remarks>
        /// </remarks>
        public static bool IsLetterOrNumber(Rune rune) => NStack.Unicode.IsLetter(rune.value) || NStack.Unicode.IsNumber(rune.value);

        /// <summary>
        /// IsMark reports whether the rune is a letter (category M).
        /// </summary>
        /// <returns><c>true</c>, if the rune is a mark, <c>false</c> otherwise.</returns>
        /// <param name="rune">The rune to test for.</param>
        /// <remarks>
        /// Reports whether the rune is a mark character (category M).
        /// </remarks>
        public static bool IsMark(Rune rune) => NStack.Unicode.IsMark(rune.value);

        /// <summary>
        /// IsNumber reports whether the rune is a letter (category N).
        /// </summary>
        /// <returns><c>true</c>, if the rune is a mark, <c>false</c> otherwise.</returns>
        /// <param name="rune">The rune to test for.</param>
        /// <remarks>
        /// Reports whether the rune is a mark character (category N).
        /// </remarks>
        public static bool IsNumber(Rune rune) => NStack.Unicode.IsNumber(rune.value);

        /// <summary>
        /// IsPunct reports whether the rune is a letter (category P).
        /// </summary>
        /// <returns><c>true</c>, if the rune is a mark, <c>false</c> otherwise.</returns>
        /// <param name="rune">The rune to test for.</param>
        /// <remarks>
        /// Reports whether the rune is a mark character (category P).
        /// </remarks>
        public static bool IsPunctuation(Rune rune) => NStack.Unicode.IsPunct(rune.value);

        /// <summary>
        /// IsSpace reports whether the rune is a space character as defined by Unicode's White Space property.
        /// </summary>
        /// <returns><c>true</c>, if the rune is a mark, <c>false</c> otherwise.</returns>
        /// <param name="rune">The rune to test for.</param>
        /// <remarks>
        /// In the Latin-1 space, white space includes '\t', '\n', '\v', '\f', '\r', ' ', 
        /// U+0085 (NEL), U+00A0 (NBSP).
        /// Other definitions of spacing characters are set by category  Z and property Pattern_White_Space.
        /// </remarks>
        public static bool IsWhiteSpace(Rune rune) => NStack.Unicode.IsSpace(rune.value);

        /// <summary>
        /// IsSymbol reports whether the rune is a symbolic character.
        /// </summary>
        /// <returns><c>true</c>, if the rune is a mark, <c>false</c> otherwise.</returns>
        /// <param name="rune">The rune to test for.</param>
        public static bool IsSymbol(Rune rune) => NStack.Unicode.IsSymbol(rune.value);

        /// <summary>
        /// Reports whether the rune is an upper case letter.
        /// </summary>
        /// <returns><c>true</c>, if the rune is an upper case letter, <c>false</c> otherwise.</returns>
        /// <param name="rune">The rune to test for.</param>
        public static bool IsUpper(Rune rune) => NStack.Unicode.IsUpper(rune.value);

        /// <summary>
        /// Reports whether the rune is a lower case letter.
        /// </summary>
        /// <returns><c>true</c>, if the rune is a lower case letter, <c>false</c> otherwise.</returns>
        /// <param name="rune">The rune to test for.</param>
        public static bool IsLower(Rune rune) => NStack.Unicode.IsLower(rune.value);

        /// <summary>
        /// Reports whether the rune is a title case letter.
        /// </summary>
        /// <returns><c>true</c>, if the rune is a lower case letter, <c>false</c> otherwise.</returns>
        /// <param name="rune">The rune to test for.</param>
        public static bool IsTitle(Rune rune) => NStack.Unicode.IsTitle(rune.value);

        /// <summary>
        /// The types of cases supported.
        /// </summary>
        public enum Case
        {
            /// <summary>
            /// Upper case
            /// </summary>
            Upper = 0,

            /// <summary>
            /// Lower case
            /// </summary>
            Lower = 1,

            /// <summary>
            /// Title case capitalizes the first letter, and keeps the rest in lowercase.
            /// Sometimes it is not as straight forward as the uppercase, some characters require special handling, like
            /// certain ligatures and Greek characters.
            /// </summary>
            Title = 2
        };

        // To maps the rune to the specified case: Case.Upper, Case.Lower, or Case.Title
        /// <summary>
        /// To maps the rune to the specified case: Case.Upper, Case.Lower, or Case.Title
        /// </summary>
        /// <returns>The cased character.</returns>
        /// <param name="toCase">The destination case.</param>
        /// <param name="rune">Rune to convert.</param>
        public static Rune To(Case toCase, Rune rune)
        {
            uint rval = rune.value;
            switch (toCase)
            {
                case Case.Lower:
                    return new Rune(NStack.Unicode.To(NStack.Unicode.Case.Lower, rval));
                case Case.Title:
                    return new Rune(NStack.Unicode.To(NStack.Unicode.Case.Title, rval));
                case Case.Upper:
                    return new Rune(NStack.Unicode.To(NStack.Unicode.Case.Upper, rval));
            }
            return ReplacementChar;
        }


        /// <summary>
        /// ToUpper maps the rune to upper case.
        /// </summary>
        /// <returns>The upper cased rune if it can be.</returns>
        /// <param name="rune">Rune.</param>
        public static Rune ToUpper(Rune rune) => NStack.Unicode.ToUpper(rune.value);

        /// <summary>
        /// ToLower maps the rune to lower case.
        /// </summary>
        /// <returns>The lower cased rune if it can be.</returns>
        /// <param name="rune">Rune.</param>
        public static Rune ToLower(Rune rune) => NStack.Unicode.ToLower(rune.value);

        /// <summary>
        /// ToLower maps the rune to title case.
        /// </summary>
        /// <returns>The lower cased rune if it can be.</returns>
        /// <param name="rune">Rune.</param>
        public static Rune ToTitle(Rune rune) => NStack.Unicode.ToTitle(rune.value);

        /// <summary>
        /// SimpleFold iterates over Unicode code points equivalent under
        /// the Unicode-defined simple case folding.
        /// </summary>
        /// <returns>The simple-case folded rune.</returns>
        /// <param name="rune">Rune.</param>
        /// <remarks>
        /// SimpleFold iterates over Unicode code points equivalent under
        /// the Unicode-defined simple case folding. Among the code points
        /// equivalent to rune (including rune itself), SimpleFold returns the
        /// smallest rune > r if one exists, or else the smallest rune >= 0.
        /// If r is not a valid Unicode code point, SimpleFold(r) returns r.
        ///
        /// For example:
        /// <code>
        ///      SimpleFold('A') = 'a'
        ///      SimpleFold('a') = 'A'
        ///
        ///      SimpleFold('K') = 'k'
        ///      SimpleFold('k') = '\u212A' (Kelvin symbol, K)
        ///      SimpleFold('\u212A') = 'K'
        ///
        ///      SimpleFold('1') = '1'
        ///
        ///      SimpleFold(-2) = -2
        /// </code>
        /// </remarks>
        public static Rune SimpleFold(Rune rune) => NStack.Unicode.SimpleFold(rune.value);

        /// <summary>
        /// Implicit operator conversion from a rune to an unsigned integer
        /// </summary>
        /// <returns>The unsigned integer representation.</returns>
        /// <param name="rune">Rune.</param>
        public static implicit operator uint(Rune rune) => rune.value;

        /// <summary>
        /// Implicit operator conversion from a C# integer into a rune.
        /// </summary>
        /// <returns>Rune representing the C# integer</returns>
        /// <param name="value">32-bit Integer.</param>
        public static implicit operator Rune(int value) => new Rune((uint)value);

        /// <summary>
        /// Implicit operator conversion from a byte to an unsigned integer
        /// </summary>
        /// <returns>The unsigned integer representation.</returns>
        /// <param name="byt">Byte.</param>
        public static implicit operator Rune(byte byt) => new Rune(byt);

        /// <summary>
        /// Implicit operator conversion from a C# char into a rune.
        /// </summary>
        /// <returns>Rune representing the C# character</returns>
        /// <param name="ch">16-bit Character.</param>
        public static implicit operator Rune(char ch) => new Rune(ch);

        /// <summary>
        /// Implicit operator conversion from an unsigned integer into a rune.
        /// </summary>
        /// <returns>Rune representing the C# character</returns>
        /// <param name="value">32-bit unsigned integer.</param>
        public static implicit operator Rune(uint value) => new Rune(value);

        /// <summary>
        /// Serves as a hash function for a <see cref="T:System.Rune"/> object.
        /// </summary>
        /// <returns>A hash code for this instance that is suitable for use in hashing algorithms and data structures such as a hash table.</returns>
        public override int GetHashCode()
        {
            return (int)value;
        }

        /// <summary>
        /// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Rune"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:System.Rune"/>.</returns>
        public override string ToString()
        {
            var buff = new byte[4];
            var size = EncodeRune(this, buff, 0);
            return System.Text.Encoding.UTF8.GetString(buff, 0, size);
        }

        /// <summary>
        /// Determines whether the specified <see cref="object"/> is equal to the current <see cref="T:System.Rune"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare with the current <see cref="T:System.Rune"/>.</param>
        /// <returns><c>true</c> if the specified <see cref="object"/> is equal to the current <see cref="T:System.Rune"/>; otherwise, <c>false</c>.</returns>
        public override bool Equals(Object obj)
        {
            // Check for null values and compare run-time types.
            if (obj == null)
                return false;

            Rune p = (Rune)obj;
            return p.value == value;
        }
    }
    //=======================================================================
    // Code that interoperates with NStack.ustring.

    /// <summary>
    /// Helper class that implements <see cref="System.Rune"/> extensions for the <see cref="NStack.ustring"/> type.
    /// </summary>
    public static class RuneExtensions
    {
        /// <summary>
        /// FullRune reports whether the ustring begins with a full UTF-8 encoding of a rune.
        /// An invalid encoding is considered a full Rune since it will convert as a width-1 error rune.
        /// </summary>
        /// <returns><c>true</c>, if the bytes in p begin with a full UTF-8 encoding of a rune, <c>false</c> otherwise.</returns>
        /// <param name="str">The string to check.</param>
        public static bool FullRune(this ustring str)
        {
            if ((object)str == null)
                throw new ArgumentNullException(nameof(str));

            foreach (var rune in str)
            {
                if (rune == Rune.Error)
                {
                    return false;
                }
                ustring us = ustring.Make(rune);
                if (!Rune.FullRune(us.ToByteArray()))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// DecodeRune unpacks the first UTF-8 encoding in the ustring returns the rune and
        /// its width in bytes. 
        /// </summary>
        /// <returns>If p is empty it returns (RuneError, 0). Otherwise, if
        /// the encoding is invalid, it returns (RuneError, 1). Both are impossible
        /// results for correct, non-empty UTF-8.
        /// </returns>
        /// <param name="str">ustring to decode.</param>
        /// <param name="start">Starting offset to look into..</param>
        /// <param name="n">Number of bytes valid in the buffer, or -1 to make it the length of the buffer.</param>
        public static (Rune rune, int size) DecodeRune(this ustring str, int start = 0, int n = -1)
        {
            if ((object)str == null)
                throw new ArgumentNullException(nameof(str));
            if (start < 0)
                throw new ArgumentException("invalid offset", nameof(start));
            if (n < 0)
                n = str.Length - start;
            if (start > str.Length - n)
                throw new ArgumentException("Out of bounds");

            return Rune.DecodeRune(str.ToByteArray(), start, n);
        }

        /// <summary>
        /// DecodeLastRune unpacks the last UTF-8 encoding in the ustring.
        /// </summary>
        /// <returns>The last rune and its width in bytes.</returns>
        /// <param name="str">String to decode rune from;   if it is empty,
        /// it returns (RuneError, 0). Otherwise, if
        /// the encoding is invalid, it returns (RuneError, 1). Both are impossible
        /// results for correct, non-empty UTF-8.</param>
        /// <param name="end">Scan up to that point, if the value is -1, it sets the value to the length of the buffer.</param>
        /// <remarks>
        /// An encoding is invalid if it is incorrect UTF-8, encodes a rune that is
        /// out of range, or is not the shortest possible UTF-8 encoding for the
        /// value. No other validation is performed.</remarks> 
        public static (Rune rune, int size) DecodeLastRune(this ustring str, int end = -1)
        {
            if ((object)str == null)
                throw new ArgumentNullException(nameof(str));
            if (str.Length == 0)
                return (Rune.Error, 0);
            if (end == -1)
                end = str.Length;
            else if (end > str.Length)
                throw new ArgumentException("The end goes beyond the size of the buffer");

            return Rune.DecodeLastRune(str.ToByteArray(), end);
        }

        /// <summary>
        /// Returns the number of runes in a ustring.
        /// </summary>
        /// <returns>Number of runes.</returns>
        /// <param name="str">utf8 string.</param>
        public static int RuneCount(this ustring str)
        {
            if ((object)str == null)
                throw new ArgumentNullException(nameof(str));

            return Rune.RuneCount(str.ToByteArray());
        }

        /// <summary>
        /// Use to find the index of the first invalid utf8 byte sequence in a buffer
        /// </summary>
        /// <returns>The index of the first invalid byte sequence or -1 if the entire buffer is valid.</returns>
        /// <param name="str">String containing the utf8 buffer.</param>
        public static int InvalidIndex(this ustring str)
        {
            if ((object)str == null)
                throw new ArgumentNullException(nameof(str));

            return Rune.InvalidIndex(str.ToByteArray());
        }

        /// <summary>
        /// Reports whether the ustring consists entirely of valid UTF-8-encoded runes.
        /// </summary>
        /// <param name="str">String to validate.</param>
        public static bool Valid(this ustring str)
        {
            if ((object)str == null)
                throw new ArgumentNullException(nameof(str));

            return Rune.Valid(str.ToByteArray());
        }

        /// <summary>
        /// Given one byte from a utf8 string, return the number of expected bytes that make up the sequence.
        /// </summary>
        /// <returns>The number of UTF8 bytes expected given the first prefix.</returns>
        /// <param name="str">String to get the first byte of a UTF8 sequence.</param>
        public static int ExpectedSizeFromFirstByte(this ustring str)
        {
            if ((object)str == null)
                throw new ArgumentNullException(nameof(str));

            return Rune.ExpectedSizeFromFirstByte(str[0]);
        }
    }
}
