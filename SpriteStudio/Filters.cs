using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpriteStudio
{

    /// <summary>
    ///   Are not allowed to draw shapes, just simple math functions
    /// </summary>
    public interface IPerPixelFilter {
        void OperatePixel(ref ColorSpaces.SuperColor input);

        FilterParameter<Object>[] filterParamaters { get; set; }
    }

    public interface FilterParameter<T>
    {
        T currentValue { get; set; }
        T defaultValue { get; set; }
    }

    public class Filters
    {
        public class BlackAndWhite : IPerPixelFilter
        {
            public void OperatePixel(ref ColorSpaces.SuperColor input)
            {
                float vari = (input.Red + input.Blue + input.Green) / 3;
                input.Red = vari;
                input.Green = vari;
                input.Blue = vari;
            }

            public FilterParameter<object>[] filterParamaters
            {
                get
                {
                    throw new NotImplementedException();
                }
                set
                {
                    throw new NotImplementedException();
                }
            }
        }
    }

}
