using System;
using System.Linq;

namespace NImageMagick
{
    public enum FilterType
    {
        UndefinedFilter,
        PointFilter,
        BoxFilter,
        TriangleFilter,
        HermiteFilter,
        HanningFilter,
        HammingFilter,
        BlackmanFilter,
        GaussianFilter,
        QuadraticFilter,
        CubicFilter,
        CatromFilter,
        MitchellFilter,
        JincFilter,
        SincFilter,
        SincFastFilter,
        KaiserFilter,
        WelshFilter,
        ParzenFilter,
        BohmanFilter,
        BartlettFilter,
        LagrangeFilter,
        LanczosFilter,
        LanczosSharpFilter,
        Lanczos2Filter,
        Lanczos2SharpFilter,
        RobidouxFilter,
        RobidouxSharpFilter,
        CosineFilter,
        SplineFilter,
        SentinelFilter  /* a count of all the filters, not a real filter */
    }
}
