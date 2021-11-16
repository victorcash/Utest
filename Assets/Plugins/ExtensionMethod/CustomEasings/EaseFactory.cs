using System;
using DG.Tweening;

public static class EaseFactory
{
    public static EaseFunction EaseAttackDecay(float attackRatio, float decayRatio)
    {
        if (attackRatio + decayRatio > 1f)
        {
            throw new ArgumentException("Sum of attack and decay ratio must not be bigger than 1.");
        }

        return (time, duration, overshootOrAmplitude, period) =>
        {
            var attackDuration = attackRatio * duration;
            var decayDuration = decayRatio * duration;
            var linearRatio = 1 - 2f / 3f * attackRatio - 2f / 3f * decayRatio;
            var linearOutRatio = 1f;
            var linearDuration = duration - attackDuration - decayDuration;
            var m = linearOutRatio / linearRatio;

            float value;
            if (time <= attackDuration)
            {
                value = Bezier.GetPoint(0f, 0f, 0f, m * attackRatio / 3f, time / attackDuration);
            }
            else if (time >= duration - decayDuration)
            {
                value = 1 - Bezier.GetPoint(0f, 0f, 0f, m * decayRatio / 3f, 1 - (time - attackDuration - linearDuration) / decayDuration);
            }
            else
            {
                value = m / 3f * attackRatio + m * (time - attackDuration) / duration;
            }

            return value;
        };
    }
}
