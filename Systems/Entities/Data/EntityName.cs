using System;
using Godot;
using Halcyon.Utilities.Extensions;
using static Halcyon.Utilities.Extensions.CsvExtensions;

namespace Halcyon.Entities.Data
{
    /// <summary> A person's full name with reference to their personal and clan names. </summary>
    /// <remarks Uses: https://github.com/sigpwned/popular-names-by-country-dataset </remarks>
    [Tool]
    public partial class EntityName : Resource, IEquatable<EntityName>
    {
        /// <summary> An entity's common, personal name. </summary>
        public GivenName FirstName { get; init; } = GivenName.Empty;

        /// <summary> An entity's family / clan name. </summary>
        public Surname LastName { get; init; } = Surname.Empty;


        /// <inheritdoc/>
        public override String ToString() => $"{FirstName.Male} {LastName.Romanised}";


        /// <summary> An empty, default name. </summary>
        public static EntityName Empty => new EntityName();


        /// <summary> Generate a random name. </summary>
        /// <param name="gender"> The gender of the name to generate. A none indicates that all names should be considered. </param>
        /// <returns> The generated name. </returns>
        public static EntityName Random(EntityGender gender)
        {
            GivenName[] firstNames = CsvExtensions.LoadData<GivenName>("res://Content/Data/Names/FirstNames.csv");
            Surname[] lastNames = CsvExtensions.LoadData<Surname>("res://Content/Data/Names/ActorLastNames.csv");

            return new EntityName
            {
                FirstName = firstNames.GetRandomElement() ?? GivenName.Empty,
                LastName = lastNames.GetRandomElement() ?? Surname.Empty
            };
        }

        /// <inheritdoc/>
        public override Int32 GetHashCode() => HashCode.Combine(FirstName, LastName);


        /// <inheritdoc/>
        public Boolean Equals(EntityName? other) => other != null ? FirstName.Equals(other.FirstName) && LastName.Equals(other.LastName) : false;
    }


    /// <summary> An entity's common, personal name. </summary>
    public record GivenName : IParseable<GivenName> // TODO - Make like Male names and Female equivalent.
    {
        /// <summary> The name's male-equivalent. </summary>
        public String Male { get; init; } = String.Empty;

        /// <summary> The name's female-equivalent. </summary>
        public String Female { get; init; } = String.Empty;


        /// <summary> An empty, default name. </summary>
        public static GivenName Empty => new GivenName();


        /// <inheritdoc/>
        public static GivenName Parse(String[] header, String[] data)
        {
            Int32 maleIndex = header.IndexOf("Male Name");
            Int32 femaleIndex = header.IndexOf("Female Name");

            String nameName = maleIndex != -1 ? data[maleIndex] : String.Empty;
            String femaleName = femaleIndex != -1 ? data[femaleIndex] : String.Empty;

            return new GivenName
            {
                Male = nameName,
                Female = femaleName
            };
        }
    }


    /// <summary> An entity's family / clan name. </summary>
    public record Surname : IParseable<Surname> // TODO - Make resource editable in inspector. Add ties to clan details.
    {
        /// <summary> The common Alpha-2 designation of the name's country of origin. </summary>
        public String CountryISO { get; init; } = String.Empty;

        /// <summary> The name as it appears in its local language, potentially using non-Latin characters. </summary>
        public String Localised { get; init; } = String.Empty;

        /// <summary> The name as it appears in in English, should only use Latin characters. </summary>
        public String Romanised { get; init; } = String.Empty;


        /// <summary> An empty, default name. </summary>
        public static Surname Empty => new Surname();


        /// <inheritdoc/>
        public static Surname Parse(String[] header, String[] data)
        {
            Int32 countryIndex = header.IndexOf("Country");
            Int32 localisedIndex = header.IndexOf("Localized Name");
            Int32 romanisedIndex = header.IndexOf("Romanized Name");

            return new Surname
            {
                CountryISO = countryIndex != -1 ? data[countryIndex] : String.Empty,
                Localised = localisedIndex != -1 ? data[localisedIndex] : String.Empty,
                Romanised = romanisedIndex != -1 ? data[romanisedIndex] : String.Empty
            };
        }
    }
}
