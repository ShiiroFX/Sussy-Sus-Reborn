using System;

public static class Extensions
{
    public static void Invoke(this EventHandler self)
    {
        self.Invoke(self, EventArgs.Empty);
    }
}
