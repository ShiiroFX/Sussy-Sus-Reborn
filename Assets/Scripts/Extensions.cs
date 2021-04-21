using System;

public static class Extensions
{
    public static void Invoke(this EventHandler self) => self.Invoke(self, EventArgs.Empty);

    public static bool InRange(this float self, float lowerBound, float upperBound) => lowerBound <= self && self <= upperBound;

    public static bool StrictInRange(this float self, float lowerBound, float upperBound) => lowerBound < self && self < upperBound;

    public static (float, float) PlusOrMinus(this float self, float variance) => (self + variance, self - variance);
}
