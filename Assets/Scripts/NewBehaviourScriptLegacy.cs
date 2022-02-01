using UnityEngine;
using System.IO;
using System;


public class NewBehaviourScript : MonoBehaviour
{
    //Global Variables
    public string filepath1;
    public string filepath2;
    public string output;
    double[,] Numbers;
    int rows;
    int columns;
    char seperator;
    double sum = 0;
    GameObject Cube;
    GameObject Sphere;

    // Start is called before the first frame update
    void Start()
    {
        //Defining the directory of the .txt file and its name to read it
        filepath1 = "test.txt";
        filepath2 = Directory.GetParent(Environment.CurrentDirectory).ToString();
        filepath2 = Path.Combine(filepath2, filepath1);

        //.txt file has 76 rows and 16 columns and the separator between columns is a tab (\t)
        rows = 76;
        columns = 16;
        seperator = '\t';

        //Defining the directory of the .txt file and its name to write in it
        output = "ouput.txt";

        //Finding two objects in the scene as "Cube" and "Sphere"
        Cube = GameObject.Find("Cube");
        Sphere = GameObject.Find("Sphere");

        //Reading a Text File
        ReadFile();

        //Writing in a Text File
        WriteFile();
    }

    // Update is called once per frame
    void Update()
    {
        //Moving objects (Cube and Sphere) by pressing the arrow keys
        if (Input.GetKeyDown("right"))
        {
            MovingObjects_Right();
        }
        if (Input.GetKeyDown("left"))
        {
            MovingObjects_Left();
        }

    }
    //Reading a Text File and doing the calculations
    private void ReadFile()
    {
        int counter = 0;
        Numbers = new double[rows, columns];

        // Reading a text file line by line
        var lines = File.ReadAllLines(File.Exists(filepath1) ? filepath1 : filepath2);

        //Storing the file content in a two dimensional array as Numbers[,]
        foreach (string line in lines)
        {
            if (counter < rows)
            {
                string[] rows = new string[16];
                int numCount = 0;
                int i = 0;

                //Saving numbers of each row in the an array as rows[] 
                while (i < line.Length)
                {
                    if (line[i] != seperator)
                    {
                        rows[numCount] = "";
                        while (line[i] != seperator)
                        {
                            rows[numCount] += line[i];
                            i++;
                            if (i >= line.Length) break;
                        }
                        numCount++;
                    }
                    i++;
                }

                //converting the format of the saved numbers from string to double and save them in a two dimensional array as Numbers[,]
                for (int ui = 0; ui < columns; ui++)
                {
                    Numbers[counter, ui] = Convert.ToDouble(rows[ui]);
                }
                counter++;
            }
        }

        //caculating the sum of the column #16
        for (int i = 0; i < rows; i++)
            sum += Numbers[i, columns - 1];
    }

    //Writing in a Text File
    private void WriteFile()
    {
        using (StreamWriter outputFile = new StreamWriter(output))
        {
            for (int i = 0; i < rows; i++)
                outputFile.WriteLine(Numbers[i, columns-1]);//.ToString());
        }
    }

    //Moving the position of two objects (Cube and Sphere) in the X dimension by adding the sum of column # 16
    private void MovingObjects_Right()
    {
        //Moving the Cube
        Vector3 pos_cube = Cube.transform.position;
        pos_cube.x += (float)sum;
        Cube.transform.position = pos_cube;

        //Moving the Sphere
        Vector3 pos_sphere = Sphere.transform.position;
        pos_sphere.x += (float)sum;
        Sphere.transform.position = pos_sphere;


    }

    //Moving the position of two objects (Cube and Sphere) in the X dimension by subtracting the sum of column # 16
    private void MovingObjects_Left()
    {
        //Moving the Cube
        Vector3 pos_cube = Cube.transform.position;
        pos_cube.x -= (float)sum;
        Cube.transform.position = pos_cube;

        //Moving the Sphere
        Vector3 pos_sphere = Sphere.transform.position;
        pos_sphere.x -= (float)sum;
        Sphere.transform.position = pos_sphere;


    }
}
