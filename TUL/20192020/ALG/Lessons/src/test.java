
/**
 * @author Aliaksei Kalosha
 */
import java.util.Scanner;


public class test {
    private static Scanner sc = new Scanner(System.in);

    public static void main(String[] args) {
        int xSize;
        int ySize;
        while ((xSize = sc.nextInt()) > 0) {
            ySize = sc.nextInt();
            int[][] m = readMatrix(xSize, ySize);
            boolean isSpiral = true;
            int x = 0;
            int y = ySize - 1;
            System.out.println("=================");
            GO:
            while (x < xSize && y > 0) {
                System.out.println("1");
                for (int i = 1; i < ySize; i++) {
                    System.out.println("1   " + m[x][i - 1] + " > " + m[x][i]);
                    if (m[x][i - 1] > m[x][i]) {
                        isSpiral = false;
                        break GO;
                    }
                }
                System.out.println("2");
                for (int i = 1; i < xSize; i++) {
                    System.out.println("2   " + m[i - 1][y] + ">" + m[i][y]);
                    if (m[i - 1][y] > m[i][y]) {
                        isSpiral = false;
                        break GO;
                    }
                }
                x--;
                System.out.println("3");
                for (int i = y - 1; i > 0; i--) {
                    System.out.println("3   " + m[x][i] + ">" + m[x][i + 1]);
                    if (m[x][i] > m[x][i + 1]) {
                        isSpiral = false;
                        break GO;
                    }
                }
                y--;
            }
            System.out.println(isSpiral ? 1 : 0);
        }
    }

    private static int[][] readMatrix(int xSize, int ySize) {
        int[][] m = new int[xSize][ySize];
        for (int i = 0; i < xSize; i++) {
            for (int j = 0; j < ySize; j++) {
                m[i][j] = sc.nextInt();
            }
        }
        return m;
    }

}
