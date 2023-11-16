using System;

public class Matrix3x3
{
    private float[,] matrix;

    public Matrix3x3(float[,] matrix)
    {
        if (matrix.GetLength(0) != 3 || matrix.GetLength(1) != 3)
        {
            throw new ArgumentException("Matrix must be 3x3");
        }

        this.matrix = matrix;
    }

    public Matrix3x3 Inverse()
    {
        float a = matrix[0, 0];
        float b = matrix[0, 1];
        float c = matrix[0, 2];
        float d = matrix[1, 0];
        float e = matrix[1, 1];
        float f = matrix[1, 2];
        float g = matrix[2, 0];
        float h = matrix[2, 1];
        float i = matrix[2, 2];

        float determinant = a * (e * i - f * h) - b * (d * i - f * g) + c * (d * h - e * g);

        if (determinant == 0)
        {
            throw new InvalidOperationException("Matrix is singular and cannot be inverted");
        }

        float[,] inverseMatrix = new float[3, 3];

        inverseMatrix[0, 0] = e * i - f * h;
        inverseMatrix[0, 1] = c * h - b * i;
        inverseMatrix[0, 2] = b * f - c * e;
        inverseMatrix[1, 0] = f * g - d * i;
        inverseMatrix[1, 1] = a * i - c * g;
        inverseMatrix[1, 2] = c * d - a * f;
        inverseMatrix[2, 0] = d * h - e * g;
        inverseMatrix[2, 1] = b * g - a * h;
        inverseMatrix[2, 2] = a * e - b * d;

        return new Matrix3x3(inverseMatrix);
    }
}