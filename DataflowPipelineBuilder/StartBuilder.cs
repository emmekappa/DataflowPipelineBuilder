﻿
using System.Threading.Tasks.Dataflow;

namespace DataflowPipelineBuilder
{
    public class StartBuilder<TOrigin, TSource> : IBuilder<TOrigin, TSource>
    {
        readonly IPropagatorBlock<TOrigin, TSource> _start;

        internal StartBuilder(IPropagatorBlock<TOrigin, TSource> start) => _start = start;

        public IBuilder<TOrigin, TTarget> Then<TTarget>(IPropagatorBlock<TSource, TTarget> block)
        {
            _start.LinkTo(block, new DataflowLinkOptions { PropagateCompletion = true });

            return new MiddleBuilder<TOrigin, TSource, TTarget>(_start, block);
        }

        public IPropagatorBlock<TOrigin, TSource> End() => _start;
    }
}
