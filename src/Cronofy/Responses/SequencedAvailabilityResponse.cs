namespace Cronofy.Responses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;

    /// <summary>
    /// Class for the deserialization of an availability response.
    /// </summary>
    internal sealed class SequencedAvailabilityResponse
    {
        /// <summary>
        /// Gets or sets the sequences of the response.
        /// </summary>
        /// <value>
        /// The sequences of the response.
        /// </value>
        [JsonProperty("sequences")]
        public IEnumerable<SequenceAvailabilityResponse> Sequences { get; set; }

        /// <summary>
        /// Converts the response to an <see cref="AvailableSequences"/>.
        /// </summary>
        /// <returns>
        /// An <see cref="AvailableSequences"/>.
        /// </returns>
        public AvailableSequences ToSequence()
        {
            return new AvailableSequences
            {
                Sequences = this.Sequences.Select(p => p.ToSequence()).ToArray(),
            };
        }

        /// <summary>
        /// Class for the deserialization of an availability response.
        /// </summary>
        internal sealed class SequenceAvailabilityResponse
        {
            /// <summary>
            /// Gets or sets the available periods of the response.
            /// </summary>
            /// <value>
            /// The available periods of the response.
            /// </value>
            [JsonProperty("sequence")]
            public IEnumerable<SequencePeriodResponse> Sequence { get; set; }

            /// <summary>
            /// Converts the response to a Sequence.
            /// </summary>
            /// <returns>
            /// A Sequence.
            /// </returns>
            public IEnumerable<AvailableSequences.SequenceItem> ToSequence()
            {
                return this.Sequence.Select(p => p.ToSequenceItem()).ToArray();
            }

            /// <summary>
            /// Class for the deserialization of an available period response.
            /// </summary>
            internal sealed class SequencePeriodResponse
            {
                /// <summary>
                /// Gets or sets the id of the sequence.
                /// </summary>
                /// <value>
                /// The id of the sequence.
                /// </value>
                [JsonProperty("sequence_id")]
                public string SequenceId { get; set; }

                /// <summary>
                /// Gets or sets the start time of the period.
                /// </summary>
                /// <value>
                /// The start time of the period.
                /// </value>
                [JsonProperty("start")]
                [JsonConverter(typeof(TimestampConverter))]
                public DateTime Start { get; set; }

                /// <summary>
                /// Gets or sets the end time of the period.
                /// </summary>
                /// <value>
                /// The end time of the period.
                /// </value>
                [JsonProperty("end")]
                [JsonConverter(typeof(TimestampConverter))]
                public DateTime End { get; set; }

                /// <summary>
                /// Gets or sets the participants for the period.
                /// </summary>
                /// <value>
                /// The participants for the period.
                /// </value>
                [JsonProperty("participants")]
                public ParticipantResponse[] Participants { get; set; }

                /// <summary>
                /// Converts the response to an <see cref="Sequence"/>.
                /// </summary>
                /// <returns>
                /// An <see cref="Sequence"/>.
                /// </returns>
                public AvailableSequences.SequenceItem ToSequenceItem()
                {
                    return new AvailableSequences.SequenceItem
                    {
                        Start = this.Start,
                        End = this.End,
                        SequenceId = this.SequenceId,
                        Participants = this.Participants.Select(p => p.ToParticipant()).ToArray(),
                    };
                }

                /// <summary>
                /// Class for the deserialization of the participants within an
                /// available period response.
                /// </summary>
                internal sealed class ParticipantResponse
                {
                    /// <summary>
                    /// Gets or sets the subject for the participant.
                    /// </summary>
                    /// <value>
                    /// The subject for the participant.
                    /// </value>
                    [JsonProperty("sub")]
                    public string Sub { get; set; }

                    /// <summary>
                    /// Returns the participant as an
                    /// <see cref="AvailablePeriod.Participant"/>.
                    /// </summary>
                    /// <returns>
                    /// The participant as an
                    /// <see cref="AvailablePeriod.Participant"/>.
                    /// </returns>
                    public AvailablePeriod.Participant ToParticipant()
                    {
                        return new AvailablePeriod.Participant
                        {
                            Sub = this.Sub,
                        };
                    }
                }
            }
        }
    }
}