/* 
 * A title for your API
 *
 * No description provided (generated by Swagger Codegen https://github.com/swagger-api/swagger-codegen)
 *
 * OpenAPI spec version: v1
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using SwaggerDateConverter = IO.Swagger.Client.SwaggerDateConverter;

namespace IO.Swagger.Model
{
    /// <summary>
    /// Restaurant
    /// </summary>
    [DataContract]
    public partial class Restaurant :  IEquatable<Restaurant>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Restaurant" /> class.
        /// </summary>
        /// <param name="restaurantId">restaurantId.</param>
        /// <param name="nom">nom.</param>
        /// <param name="shortName">shortName.</param>
        /// <param name="serverName">serverName.</param>
        /// <param name="serverIpAddress">serverIpAddress.</param>
        /// <param name="email">email.</param>
        /// <param name="city">city.</param>
        /// <param name="address1">address1.</param>
        /// <param name="address2">address2.</param>
        /// <param name="address3">address3.</param>
        /// <param name="address4">address4.</param>
        /// <param name="zipCode">zipCode.</param>
        /// <param name="phoneNumber">phoneNumber.</param>
        /// <param name="faxNumber">faxNumber.</param>
        /// <param name="openingDate">openingDate.</param>
        /// <param name="permanentClosureDate">permanentClosureDate.</param>
        /// <param name="serverEvents">serverEvents.</param>
        public Restaurant(int? restaurantId = default(int?), string nom = default(string), string shortName = default(string), string serverName = default(string), string serverIpAddress = default(string), string email = default(string), string city = default(string), string address1 = default(string), string address2 = default(string), string address3 = default(string), string address4 = default(string), int? zipCode = default(int?), string phoneNumber = default(string), string faxNumber = default(string), DateTime? openingDate = default(DateTime?), DateTime? permanentClosureDate = default(DateTime?), List<ServerEvent> serverEvents = default(List<ServerEvent>))
        {
            this.RestaurantId = restaurantId;
            this.Nom = nom;
            this.ShortName = shortName;
            this.ServerName = serverName;
            this.ServerIpAddress = serverIpAddress;
            this.Email = email;
            this.City = city;
            this.Address1 = address1;
            this.Address2 = address2;
            this.Address3 = address3;
            this.Address4 = address4;
            this.ZipCode = zipCode;
            this.PhoneNumber = phoneNumber;
            this.FaxNumber = faxNumber;
            this.OpeningDate = openingDate;
            this.PermanentClosureDate = permanentClosureDate;
            this.ServerEvents = serverEvents;
        }
        
        /// <summary>
        /// Gets or Sets RestaurantId
        /// </summary>
        [DataMember(Name="RestaurantId", EmitDefaultValue=false)]
        public int? RestaurantId { get; set; }

        /// <summary>
        /// Gets or Sets Nom
        /// </summary>
        [DataMember(Name="Nom", EmitDefaultValue=false)]
        public string Nom { get; set; }

        /// <summary>
        /// Gets or Sets ShortName
        /// </summary>
        [DataMember(Name="ShortName", EmitDefaultValue=false)]
        public string ShortName { get; set; }

        /// <summary>
        /// Gets or Sets ServerName
        /// </summary>
        [DataMember(Name="ServerName", EmitDefaultValue=false)]
        public string ServerName { get; set; }

        /// <summary>
        /// Gets or Sets ServerIpAddress
        /// </summary>
        [DataMember(Name="ServerIpAddress", EmitDefaultValue=false)]
        public string ServerIpAddress { get; set; }

        /// <summary>
        /// Gets or Sets Email
        /// </summary>
        [DataMember(Name="Email", EmitDefaultValue=false)]
        public string Email { get; set; }

        /// <summary>
        /// Gets or Sets City
        /// </summary>
        [DataMember(Name="City", EmitDefaultValue=false)]
        public string City { get; set; }

        /// <summary>
        /// Gets or Sets Address1
        /// </summary>
        [DataMember(Name="Address_1", EmitDefaultValue=false)]
        public string Address1 { get; set; }

        /// <summary>
        /// Gets or Sets Address2
        /// </summary>
        [DataMember(Name="Address_2", EmitDefaultValue=false)]
        public string Address2 { get; set; }

        /// <summary>
        /// Gets or Sets Address3
        /// </summary>
        [DataMember(Name="Address_3", EmitDefaultValue=false)]
        public string Address3 { get; set; }

        /// <summary>
        /// Gets or Sets Address4
        /// </summary>
        [DataMember(Name="Address_4", EmitDefaultValue=false)]
        public string Address4 { get; set; }

        /// <summary>
        /// Gets or Sets ZipCode
        /// </summary>
        [DataMember(Name="ZipCode", EmitDefaultValue=false)]
        public int? ZipCode { get; set; }

        /// <summary>
        /// Gets or Sets PhoneNumber
        /// </summary>
        [DataMember(Name="PhoneNumber", EmitDefaultValue=false)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or Sets FaxNumber
        /// </summary>
        [DataMember(Name="FaxNumber", EmitDefaultValue=false)]
        public string FaxNumber { get; set; }

        /// <summary>
        /// Gets or Sets OpeningDate
        /// </summary>
        [DataMember(Name="OpeningDate", EmitDefaultValue=false)]
        public DateTime? OpeningDate { get; set; }

        /// <summary>
        /// Gets or Sets PermanentClosureDate
        /// </summary>
        [DataMember(Name="PermanentClosureDate", EmitDefaultValue=false)]
        public DateTime? PermanentClosureDate { get; set; }

        /// <summary>
        /// Gets or Sets ServerEvents
        /// </summary>
        [DataMember(Name="ServerEvents", EmitDefaultValue=false)]
        public List<ServerEvent> ServerEvents { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Restaurant {\n");
            sb.Append("  RestaurantId: ").Append(RestaurantId).Append("\n");
            sb.Append("  Nom: ").Append(Nom).Append("\n");
            sb.Append("  ShortName: ").Append(ShortName).Append("\n");
            sb.Append("  ServerName: ").Append(ServerName).Append("\n");
            sb.Append("  ServerIpAddress: ").Append(ServerIpAddress).Append("\n");
            sb.Append("  Email: ").Append(Email).Append("\n");
            sb.Append("  City: ").Append(City).Append("\n");
            sb.Append("  Address1: ").Append(Address1).Append("\n");
            sb.Append("  Address2: ").Append(Address2).Append("\n");
            sb.Append("  Address3: ").Append(Address3).Append("\n");
            sb.Append("  Address4: ").Append(Address4).Append("\n");
            sb.Append("  ZipCode: ").Append(ZipCode).Append("\n");
            sb.Append("  PhoneNumber: ").Append(PhoneNumber).Append("\n");
            sb.Append("  FaxNumber: ").Append(FaxNumber).Append("\n");
            sb.Append("  OpeningDate: ").Append(OpeningDate).Append("\n");
            sb.Append("  PermanentClosureDate: ").Append(PermanentClosureDate).Append("\n");
            sb.Append("  ServerEvents: ").Append(ServerEvents).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
  
        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as Restaurant);
        }

        /// <summary>
        /// Returns true if Restaurant instances are equal
        /// </summary>
        /// <param name="input">Instance of Restaurant to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Restaurant input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.RestaurantId == input.RestaurantId ||
                    (this.RestaurantId != null &&
                    this.RestaurantId.Equals(input.RestaurantId))
                ) && 
                (
                    this.Nom == input.Nom ||
                    (this.Nom != null &&
                    this.Nom.Equals(input.Nom))
                ) && 
                (
                    this.ShortName == input.ShortName ||
                    (this.ShortName != null &&
                    this.ShortName.Equals(input.ShortName))
                ) && 
                (
                    this.ServerName == input.ServerName ||
                    (this.ServerName != null &&
                    this.ServerName.Equals(input.ServerName))
                ) && 
                (
                    this.ServerIpAddress == input.ServerIpAddress ||
                    (this.ServerIpAddress != null &&
                    this.ServerIpAddress.Equals(input.ServerIpAddress))
                ) && 
                (
                    this.Email == input.Email ||
                    (this.Email != null &&
                    this.Email.Equals(input.Email))
                ) && 
                (
                    this.City == input.City ||
                    (this.City != null &&
                    this.City.Equals(input.City))
                ) && 
                (
                    this.Address1 == input.Address1 ||
                    (this.Address1 != null &&
                    this.Address1.Equals(input.Address1))
                ) && 
                (
                    this.Address2 == input.Address2 ||
                    (this.Address2 != null &&
                    this.Address2.Equals(input.Address2))
                ) && 
                (
                    this.Address3 == input.Address3 ||
                    (this.Address3 != null &&
                    this.Address3.Equals(input.Address3))
                ) && 
                (
                    this.Address4 == input.Address4 ||
                    (this.Address4 != null &&
                    this.Address4.Equals(input.Address4))
                ) && 
                (
                    this.ZipCode == input.ZipCode ||
                    (this.ZipCode != null &&
                    this.ZipCode.Equals(input.ZipCode))
                ) && 
                (
                    this.PhoneNumber == input.PhoneNumber ||
                    (this.PhoneNumber != null &&
                    this.PhoneNumber.Equals(input.PhoneNumber))
                ) && 
                (
                    this.FaxNumber == input.FaxNumber ||
                    (this.FaxNumber != null &&
                    this.FaxNumber.Equals(input.FaxNumber))
                ) && 
                (
                    this.OpeningDate == input.OpeningDate ||
                    (this.OpeningDate != null &&
                    this.OpeningDate.Equals(input.OpeningDate))
                ) && 
                (
                    this.PermanentClosureDate == input.PermanentClosureDate ||
                    (this.PermanentClosureDate != null &&
                    this.PermanentClosureDate.Equals(input.PermanentClosureDate))
                ) && 
                (
                    this.ServerEvents == input.ServerEvents ||
                    this.ServerEvents != null &&
                    this.ServerEvents.SequenceEqual(input.ServerEvents)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 41;
                if (this.RestaurantId != null)
                    hashCode = hashCode * 59 + this.RestaurantId.GetHashCode();
                if (this.Nom != null)
                    hashCode = hashCode * 59 + this.Nom.GetHashCode();
                if (this.ShortName != null)
                    hashCode = hashCode * 59 + this.ShortName.GetHashCode();
                if (this.ServerName != null)
                    hashCode = hashCode * 59 + this.ServerName.GetHashCode();
                if (this.ServerIpAddress != null)
                    hashCode = hashCode * 59 + this.ServerIpAddress.GetHashCode();
                if (this.Email != null)
                    hashCode = hashCode * 59 + this.Email.GetHashCode();
                if (this.City != null)
                    hashCode = hashCode * 59 + this.City.GetHashCode();
                if (this.Address1 != null)
                    hashCode = hashCode * 59 + this.Address1.GetHashCode();
                if (this.Address2 != null)
                    hashCode = hashCode * 59 + this.Address2.GetHashCode();
                if (this.Address3 != null)
                    hashCode = hashCode * 59 + this.Address3.GetHashCode();
                if (this.Address4 != null)
                    hashCode = hashCode * 59 + this.Address4.GetHashCode();
                if (this.ZipCode != null)
                    hashCode = hashCode * 59 + this.ZipCode.GetHashCode();
                if (this.PhoneNumber != null)
                    hashCode = hashCode * 59 + this.PhoneNumber.GetHashCode();
                if (this.FaxNumber != null)
                    hashCode = hashCode * 59 + this.FaxNumber.GetHashCode();
                if (this.OpeningDate != null)
                    hashCode = hashCode * 59 + this.OpeningDate.GetHashCode();
                if (this.PermanentClosureDate != null)
                    hashCode = hashCode * 59 + this.PermanentClosureDate.GetHashCode();
                if (this.ServerEvents != null)
                    hashCode = hashCode * 59 + this.ServerEvents.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }

}
