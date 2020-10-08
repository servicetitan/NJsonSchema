//-----------------------------------------------------------------------
// <copyright file="TypeScriptDateTimeType.cs" company="NJsonSchema">
//     Copyright (c) Rico Suter. All rights reserved.
// </copyright>
// <license>https://github.com/RicoSuter/NJsonSchema/blob/master/LICENSE.md</license>
// <author>Rico Suter, mail@rsuter.com</author>
//-----------------------------------------------------------------------

namespace NJsonSchema.CodeGeneration.TypeScript
{
    /// <summary>Specifies the TypeScript date time type handling.</summary>
    public enum TypeScriptDateTimeType
    {
        /// <summary>Uses the JavaScript Date object for date time handling.</summary>
        Date,

        /// <summary>Uses the Moment.js for date time handling.</summary>
        MomentJS,

        /// <summary>Uses the strings for date time handling (no conversion).</summary>
        String,

        /// <summary>Uses the Moment.js for date time with offset handling.</summary>
        OffsetMomentJS,

        /// <summary>Uses Luxon for date time handling.</summary>
        Luxon,

        /// <summary>Uses the DayJS.js for date time handling.</summary>
        DayJS,
    }
}