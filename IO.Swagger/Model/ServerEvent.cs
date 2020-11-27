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
    /// ServerEvent
    /// </summary>
    [DataContract]
    public partial class ServerEvent :  IEquatable<ServerEvent>, IValidatableObject
    {
        /// <summary>
        /// Defines Event
        /// </summary>
        public enum EventEnum
        {
            
            /// <summary>
            /// Enum NUMBER_0 for value: 0
            /// </summary>
            
            NUMBER_0 = 0,
            
            /// <summary>
            /// Enum NUMBER_1 for value: 1
            /// </summary>
            
            NUMBER_1 = 1,
            
            /// <summary>
            /// Enum NUMBER_2 for value: 2
            /// </summary>
            
            NUMBER_2 = 2
        }

        /// <summary>
        /// Gets or Sets Event
        /// </summary>
        [DataMember(Name="Event", EmitDefaultValue=false)]
        public EventEnum? Event { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="ServerEvent" /> class.
        /// </summary>
        /// <param name="serverEventId">serverEventId.</param>
        /// <param name="_event">_event.</param>
        /// <param name="date">date.</param>
        /// <param name="upTimes">upTimes.</param>
        /// <param name="detail">detail.</param>
        /// <param name="restaurant">restaurant.</param>
        public ServerEvent(int? serverEventId = default(int?), EventEnum? _event = default(EventEnum?), DateTime? date = default(DateTime?), DateTime? upTimes = default(DateTime?), string detail = default(string), Restaurant restaurant = default(Restaurant))
        {
            this.ServerEventId = serverEventId;
            this.Event = _event;
            this.Date = date;
            this.UpTimes = upTimes;
            this.Detail = detail;
            this.Restaurant = restaurant;
        }
        
        /// <summary>
        /// Gets or Sets ServerEventId
        /// </summary>
        [DataMember(Name="ServerEventId", EmitDefaultValue=false)]
        public int? ServerEventId { get; set; }


        /// <summary>
        /// Gets or Sets Date
        /// </summary>
        [DataMember(Name="Date", EmitDefaultValue=false)]
        public DateTime? Date { get; set; }

        /// <summary>
        /// Gets or Sets UpTimes
        /// </summary>
        [DataMember(Name="UpTimes", EmitDefaultValue=false)]
        public DateTime? UpTimes { get; set; }

        /// <summary>
        /// Gets or Sets Detail
        /// </summary>
        [DataMember(Name="Detail", EmitDefaultValue=false)]
        public string Detail { get; set; }

        /// <summary>
        /// Gets or Sets Restaurant
        /// </summary>
        [DataMember(Name="Restaurant", EmitDefaultValue=false)]
        public Restaurant Restaurant { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ServerEvent {\n");
            sb.Append("  ServerEventId: ").Append(ServerEventId).Append("\n");
            sb.Append("  Event: ").Append(Event).Append("\n");
            sb.Append("  Date: ").Append(Date).Append("\n");
            sb.Append("  UpTimes: ").Append(UpTimes).Append("\n");
            sb.Append("  Detail: ").Append(Detail).Append("\n");
            sb.Append("  Restaurant: ").Append(Restaurant).Append("\n");
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
            return this.Equals(input as ServerEvent);
        }

        /// <summary>
        /// Returns true if ServerEvent instances are equal
        /// </summary>
        /// <param name="input">Instance of ServerEvent to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ServerEvent input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.ServerEventId == input.ServerEventId ||
                    (this.ServerEventId != null &&
                    this.ServerEventId.Equals(input.ServerEventId))
                ) && 
                (
                    this.Event == input.Event ||
                    (this.Event != null &&
                    this.Event.Equals(input.Event))
                ) && 
                (
                    this.Date == input.Date ||
                    (this.Date != null &&
                    this.Date.Equals(input.Date))
                ) && 
                (
                    this.UpTimes == input.UpTimes ||
                    (this.UpTimes != null &&
                    this.UpTimes.Equals(input.UpTimes))
                ) && 
                (
                    this.Detail == input.Detail ||
                    (this.Detail != null &&
                    this.Detail.Equals(input.Detail))
                ) && 
                (
                    this.Restaurant == input.Restaurant ||
                    (this.Restaurant != null &&
                    this.Restaurant.Equals(input.Restaurant))
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
                if (this.ServerEventId != null)
                    hashCode = hashCode * 59 + this.ServerEventId.GetHashCode();
                if (this.Event != null)
                    hashCode = hashCode * 59 + this.Event.GetHashCode();
                if (this.Date != null)
                    hashCode = hashCode * 59 + this.Date.GetHashCode();
                if (this.UpTimes != null)
                    hashCode = hashCode * 59 + this.UpTimes.GetHashCode();
                if (this.Detail != null)
                    hashCode = hashCode * 59 + this.Detail.GetHashCode();
                if (this.Restaurant != null)
                    hashCode = hashCode * 59 + this.Restaurant.GetHashCode();
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
