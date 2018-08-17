namespace Cronofy
{
    using System;
    using System.Collections.Generic;
    using Requests;

    /// <summary>
    /// Sequenced availability request builder.
    /// </summary>
    public sealed class SequencedAvailabilityRequestBuilder : IBuilder<SequencedAvailabilityRequest>
    {
        /// <summary>
        /// The available periods for the request.
        /// </summary>
        private readonly IList<AvailabilityRequest.AvailablePeriod> availablePeriods =
            new List<AvailabilityRequest.AvailablePeriod>();

        /// <summary>
        /// The sequences for the builder.
        /// </summary>
        private readonly IList<SequencedAvailabilityRequest.SequenceRequest> sequences =
            new List<SequencedAvailabilityRequest.SequenceRequest>();
        
        /// <summary>
        /// Adds an available period to the request.
        /// </summary>
        /// <param name="start">
        /// The start of the available period.
        /// </param>
        /// <param name="end">
        /// The end of the available period.
        /// </param>
        /// <returns>
        /// A reference to the <see cref="SequencedAvailabilityRequestBuilder"/>.
        /// </returns>
        public SequencedAvailabilityRequestBuilder AddAvailablePeriod(DateTimeOffset start, DateTimeOffset end)
        {
            var period = new AvailabilityRequest.AvailablePeriod
            {
                Start = start,
                End = end,
            };

            this.availablePeriods.Add(period);

            return this;
        }

        /// <summary>
        /// Adds the sequence to this request.
        /// </summary>
        /// <param name="builder">
        /// The sequence builder.
        /// </param>
        /// <returns>
        /// A reference to the <see cref="SequencedAvailabilityRequestBuilder"/>.
        /// </returns>
        public SequencedAvailabilityRequestBuilder AddSequence(SequenceRequestBuilder builder)
        {
            this.sequences.Add(builder.Build());
            return this;
        }

        /// <inheritdoc />
        public SequencedAvailabilityRequest Build()
        {
            return new SequencedAvailabilityRequest()
            {
                AvailablePeriods = this.availablePeriods,
                Sequence = this.sequences,
            };
        }
    }
}