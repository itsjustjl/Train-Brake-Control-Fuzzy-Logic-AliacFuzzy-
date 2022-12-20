using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DotFuzzy;
namespace AliacFuzzyLogic
{
    public partial class Form1 : Form
    {
        FuzzyEngine fe;
        MembershipFunctionCollection speed, distance,brake;
        LinguisticVariable myspeed, mydistance, mybrake;
        FuzzyRuleCollection myrules;
        

        public Form1()
        {
            InitializeComponent();
        }

    
        public void setMembers()
        {

            speed = new MembershipFunctionCollection();
            speed.Add(new MembershipFunction("SLOW", 20.0, 30.0, 40.0, 50.0));
            speed.Add(new MembershipFunction("AVERAGE", 40.0, 50.0, 75.0, 100.0));
            speed.Add(new MembershipFunction("FAST", 80.0, 100.0, 135.0, 150.0));
            speed.Add(new MembershipFunction("VFAST", 140.0, 150.0, 200.0, 220.0));
            myspeed = new LinguisticVariable("SPEED", speed);


            distance = new MembershipFunctionCollection();
            distance.Add(new MembershipFunction("CLOSE", 15.0, 20.0, 25.0, 35.0));
            distance.Add(new MembershipFunction("FAR", 23.0, 35.0, 55.0, 60.0));
            distance.Add(new MembershipFunction("VFAR", 48.0, 60.0, 90.0, 120.0));
            mydistance = new LinguisticVariable("DISTANCE", distance);

            brake = new MembershipFunctionCollection();
            brake.Add(new MembershipFunction("VLIGHT", 0.0, 3.0, 6.0, 10.0));
            brake.Add(new MembershipFunction("LIGHT", 8.0, 10.0, 20.0, 30.0));
            brake.Add(new MembershipFunction("FULL", 27.0, 39.0, 47.0, 60.0));
            mybrake = new LinguisticVariable("BRAKE", brake);

            
        
        }

        public void setRules()
        {
          myrules = new FuzzyRuleCollection();

          myrules.Add(new FuzzyRule("IF (SPEED IS SLOW) AND (DISTANCE IS CLOSE) THEN BRAKE IS LIGHT")); 
          myrules.Add(new FuzzyRule("IF (SPEED IS SLOW) AND (DISTANCE IS FAR) THEN BRAKE IS VLIGHT"));
          myrules.Add(new FuzzyRule("IF (SPEED IS SLOW) AND (DISTANCE IS VFAR) THEN BRAKE IS VLIGHT"));

          myrules.Add(new FuzzyRule("IF (SPEED IS AVERAGE) AND (DISTANCE IS CLOSE) THEN BRAKE IS LIGHT"));
          myrules.Add(new FuzzyRule("IF (SPEED IS AVERAGE) AND (DISTANCE IS FAR) THEN BRAKE IS LIGHT"));
          myrules.Add(new FuzzyRule("IF (SPEED IS AVERAGE) AND (DISTANCE IS VFAR) THEN BRAKE IS VLIGHT"));

          myrules.Add(new FuzzyRule("IF (SPEED IS FAST) AND (DISTANCE IS CLOSE) THEN BRAKE IS FULL"));
          myrules.Add(new FuzzyRule("IF (SPEED IS FAST) AND (DISTANCE IS FAR) THEN BRAKE IS LIGHT"));
          myrules.Add(new FuzzyRule("IF (SPEED IS FAST) AND (DISTANCE IS VFAR) THEN BRAKE IS VLIGHT"));

          myrules.Add(new FuzzyRule("IF (SPEED IS VFAST) AND (DISTANCE IS CLOSE) THEN BRAKE IS FULL"));
          myrules.Add(new FuzzyRule("IF (SPEED IS VFAST) AND (DISTANCE IS FAR) THEN BRAKE IS LIGHT"));
          myrules.Add(new FuzzyRule("IF (SPEED IS VFAST) AND (DISTANCE IS VFAR) THEN BRAKE IS LIGHT"));
        }

        public void setFuzzyEngine()
        {
            fe = new FuzzyEngine();
            fe.LinguisticVariableCollection.Add(myspeed);
            fe.LinguisticVariableCollection.Add(mydistance);
            fe.LinguisticVariableCollection.Add(mybrake);
            fe.FuzzyRuleCollection = myrules;
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void defuziffyToolStripMenuItem_Click(object sender, EventArgs e)
        {
         
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setMembers();
            setRules();
            //setFuzzyEngine();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            myspeed.InputValue=(Convert.ToDouble(textBox1.Text));
            myspeed.Fuzzify("SLOW");
            
            
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            mydistance.InputValue = (Convert.ToDouble(textBox2.Text));
            mydistance.Fuzzify("CLOSE");
            
        }

        public void fuziffyvalues()
        {
            myspeed.InputValue = (Convert.ToDouble(textBox1.Text));
            myspeed.Fuzzify("SLOW");
            mydistance.InputValue = (Convert.ToDouble(textBox2.Text));
            mydistance.Fuzzify("CLOSE");
        
        }
        public void defuzzy()
        {
            setFuzzyEngine();
            fe.Consequent = "BRAKE";
            textBox3.Text = "" + fe.Defuzzify();
        }

        public void computenewspeed()
        {

            double oldspeed = Convert.ToDouble(textBox1.Text);
            double oldbrake = Convert.ToDouble(textBox3.Text);
            double oldistance = Convert.ToDouble(textBox2.Text);
            double newspeed = ((1 - 0.1) * (oldspeed)) + (oldbrake - (0.1 * oldistance));
            textBox1.Text = "" + newspeed;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            setFuzzyEngine();
            fe.Consequent = "BRAKE";
            textBox3.Text = "" + fe.Defuzzify();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            computenewspeed();
        }

      

        private void Form1_Load(object sender, EventArgs e)
        {
            setMembers();
            setRules();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            fuziffyvalues();
            defuzzy();
            computenewspeed();
        }

       
    }
}
