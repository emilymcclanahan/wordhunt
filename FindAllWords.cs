using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindAllWords : MonoBehaviour
{
    char[,] board = new char[4,4];
    bool[,] visited = new bool[4,4];

    void Start() {
        int k=0;
        for (int i=0; i<4; i++) {
            for (int j=0; j<4; j++) {
                board[i,j] = Play.instance.lettersOnBoard[k];
                k++;
            }
        }
        //for (int i = 0; i < 3; i++) {
          //  for (int j = 0; j < 3; j++) {
                FindWords(board, visited, 0, 0, "");
          //  }
      //  }
    }

    static void FindWords(char [,]boggle, bool [,]visited, int i, int j, string str) {
       // Mark current cell as visited and
       // append current character to str



       visited[i, j] = true;
       str = str + boggle[i, j];
       Debug.Log(str);
       // If str is present in dictionary,
       // then print it
       // if (Play.instance.IsValidWord(str)) {
       //    Debug.Log(str);
       // }

       // Traverse 8 adjacent cells of boggle[i,j]
       for (int row = i - 1; row <= i + 1 && row < 4; row++) {
           for (int col = j - 1; col <= j + 1 && col < 4; col++) {
               if (row >= 0 && col >= 0 && !visited[row, col]) {
                  FindWords(boggle, visited, row, col, str);
               }
           }
       }

       // Erase current character from string and
       // mark visited of current cell as false
       str = "" + str[str.Length - 1];
       visited[i, j] = false;
   }
}
