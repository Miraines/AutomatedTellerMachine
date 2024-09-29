    using Itmo.ObjectOrientedProgramming.Lab1.Interfaces;
    using Itmo.ObjectOrientedProgramming.Lab1.Models;
    using Itmo.ObjectOrientedProgramming.Lab1.TypeResults;

    namespace Itmo.ObjectOrientedProgramming.Lab1.Process;

    public class ProcessTrain
    {
        private readonly ITrain _train;
        private readonly Route _route;

        public ProcessTrain(ITrain train, Route route)
        {
            _train = train;
            _route = route;
        }

        public Result Process()
        {
            return CalculateRouteTime();
        }

        public double GetSegmentLength(int segmentIndex)
        {
            IRouteSegment segment = _route.GetSegment(segmentIndex);
            return segment.Distance;
        }

        private Result CalculateRouteTime()
        {
            double totalTime = 0;

            for (int i = 0; i < _route.Count(); ++i)
            {
                IRouteSegment segment = _route.GetSegment(i);
                Result segmentTimeResult = segment.CalculateSegmentTime(_train);

                if (segmentTimeResult is Result.Failure failure)
                {
                    return new Result.Failure($"Failed to pass segment {i + 1}: {failure.ErrorMessage}");
                }
                else if (segmentTimeResult is Result.SuccessWithTime successWithTime)
                {
                    totalTime += successWithTime.Time;
                }
            }

            if (!_route.FinalSegment.CheckSpeed(_train))
            {
                return new Result.Failure("The train exceeded the speed limit at the end of the route");
            }

            return new Result.SuccessWithTime(totalTime);
        }
    }