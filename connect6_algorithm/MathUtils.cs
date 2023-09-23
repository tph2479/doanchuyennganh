public static class MathUtils
{
    public static double[] Softmax(double[] x)
    {
        double[] probs = new double[x.Length];
        double maxVal = x.Max();
        double sumExp = 0;

        for (int i = 0; i < x.Length; i++)
        {
            probs[i] = Math.Exp(x[i] - maxVal);
            sumExp += probs[i];
        }

        for (int i = 0; i < x.Length; i++)
        {
            probs[i] /= sumExp;
        }

        return probs;
    }
}