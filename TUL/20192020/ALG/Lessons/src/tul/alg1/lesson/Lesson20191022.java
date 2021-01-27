package tul.alg1.lesson;

public class Lesson20191022 {

    public static void main(String[] args) {

        {
            int a = 10;
            do a++; while (a < 10);
        }
        int n = (int) Math.round(Math.random() * 10 - 5);
        double a = 2;
        // přiklad cyclusa  while
        {
            int i = 0;
            double v = 1;
            EXSAMPLE:
            while (i < n) {
                if (false) {
                    //break;
                    break EXSAMPLE;
                } else if (false) {
                    //continue;
                    continue EXSAMPLE;
                }
                //dělej něco, napřikald počitej kladnou mocninu
                v *= a;
                i++;
            }
        }
        //přikald cyclusa do-while
        {
            int i = 0;
            double v = 1;
            EXSAMPLE:
            do {
                if (false) {
                    //break;
                    break EXSAMPLE;
                } else if (false) {
                    //continue;
                    continue EXSAMPLE;
                }
                //dělej něco, napřikald počitej kladnou mocninu
                v *= a;
                i++;
            } while (i < n);
        }
        //přiklad for
        {
            double v = 1;
            EXSAMPLE:
            for (int i = 0; i < n; i++) {
                if (false) {
                    //break;
                    break EXSAMPLE;
                } else if (false) {
                    //continue;
                    continue EXSAMPLE;
                }
                //dělej něco, napřikald počitej kladnou mocninu
                v *= a;
            }
        }
        //přiklad for
        {
            double v = 1;
            EXSAMPLE:
            for (int i = 0; i < n; i++, v *= v * a) {
                if (false) {
                    //break;
                    break EXSAMPLE;
                } else if (false) {
                    //continue;
                    continue EXSAMPLE;
                }
            }
        }
        {
            int i = (int) (Math.random() * 13 - 3);
            switch (i) {
                case -1:
                case -2:
                case -3:
                    System.out.println("miń než nula");
                case 0:
                    System.out.println("nula");
                    break;
                case 1:
                    System.out.println("jeden");
                    break;
                case 2:
                    System.out.println("dva");
                    break;
                case 3:
                    System.out.println("tří");
                    break;
                case 4:
                    System.out.println("čtyří");
                    break;
                case 5:
                    System.out.println("pět");
                    break;
                case 6:
                default:
                    System.out.println("víc než pět");
                    break;
            }
            //(new Calculator()).main(new String[0]);
        }
        {
            try {
                assert 2!=2 : "Dva se rovna dva";
                System.out.println("Try throw exception");
                throw new Exception("Test");
            }catch (Exception e){
                System.out.println("catch " + e);
            }
            finally {
                System.out.println("Finally");
            }
        }


    }
}
