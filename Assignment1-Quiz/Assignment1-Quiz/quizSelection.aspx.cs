﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Assignment1_Quiz
{
    public partial class quizSelection : System.Web.UI.Page
    {
        //List of currently loaded quizzes // Populate from text file "Quizzes.txt"
        public List<Quiz> quizCurrentList = new List<Quiz>();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSportCat_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Quiz quiz;

            string path = Server.MapPath("Quizzes.txt");
            string[] readFile = File.ReadAllLines(path);
            string[] splitLine;

            for (int i = 0; i < readFile.Length; i++)
            {
                splitLine = readFile[i].Split(',');
                if (splitLine[2] == btn.Text)
                {
                    quiz = new Quiz(splitLine[0], splitLine[1], splitLine[2]);
                    quizCurrentList.Add(quiz);
                }
            }

            //Add quiz dropdown list
            for (int i = 0; i < quizCurrentList.Count; i++)
            {
                lstQuizSelect.Items.Add(new ListItem(quizCurrentList.ElementAt(i)._quizName, quizCurrentList.ElementAt(i)._quizID));
            }
        }

        protected void startQuiz_Click(object sender, EventArgs e)
        {
            List<QuizQuestions> questions = new List<QuizQuestions>();

            QuizQuestions question;
            string path = Server.MapPath("QuizQuestions.txt");
            string[] readFile = File.ReadAllLines(path);
            string[] splitLine;


            for (int i = 0; i < readFile.Length; i++)
            {
                splitLine = readFile[i].Split(',');
                if (splitLine[0] == lstQuizSelect.SelectedValue)
                {
                    question = new QuizQuestions(splitLine[1], splitLine[2], splitLine[3], splitLine[4], splitLine[5], splitLine[6]);
                    questions.Add(question);
                }
            }

            Session.Add("selectedQuiz", quizCurrentList.ElementAt(lstQuizSelect.SelectedIndex));
            //Response.Redirect("question1.aspx");
        }
    }
}