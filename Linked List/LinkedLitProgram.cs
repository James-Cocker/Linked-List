using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Linked_List
{
    public partial class Form1 : Form
    {
        int HeadPointer = 2;            // Initialising variables
        int FreePointer = 5;
        Node[] LinkedList = new Node[10];


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CreateLinkedList();
        }


        struct Node         // Crating the structure for each Node
        {
            public string Name;
            public int Pointer;
        }

        void CreateLinkedList()         // Creating the hardcoded linked list when program is started
        {
            LinkedList[2].Name = "Apple";
            LinkedList[2].Pointer = 1;

            LinkedList[1].Name = "Cat";
            LinkedList[1].Pointer = 4;

            LinkedList[4].Name = "Fox";
            LinkedList[4].Pointer = 3;

            LinkedList[3].Name = "Horse";
            LinkedList[3].Pointer = 0;

            LinkedList[0].Name = "Mouse";
            LinkedList[0].Pointer = -1;         // -1 to represent the end of the Linked List

            int DataItems = FreePointer;

            // Go through and create the free space
            for (int FreeSpace = DataItems; FreeSpace < LinkedList.Length; FreeSpace++)
            {
                LinkedList[FreeSpace].Name = "Empty";

                if (FreeSpace != (LinkedList.Length - 1))
                {
                    LinkedList[FreeSpace].Pointer = FreeSpace + 1;
                }
                else
                {
                    LinkedList[FreeSpace].Pointer = -1;
                }
            }

            // Display to user in the labels
            DisplayLinkedList();
        }

        private void DisplayLinkedList()
        {
            lblHeadPointer.Text = "Head pointer: " + HeadPointer;           // Display pointers
            lblFreePointer.Text = "Free Pointer: " + FreePointer;

            // Loop through and assign each node to the Linked list or Free Space labels
            for (int ToDisplay = 0; ToDisplay < LinkedList.Length; ToDisplay++)
            {
                string Text = ToDisplay.ToString() + "      " + LinkedList[ToDisplay].Name + "     " + LinkedList[ToDisplay].Pointer + "\r\n";

                if (LinkedList[ToDisplay].Name != "Empty")
                {
                    lblDataOutput.Text += Text;
                }
                else
                {
                    lblFreeSpace.Text += Text;
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string Input = txtSearch.Text;

            if (Input != "")
            {
                // Initialise variables
                bool Found = false;
                int TempHead = HeadPointer;
                string MessageText = Input + " is found in: \n";

                // Loop through each item in the linked list and try to find item searching for
                for (int Item = 0; Item < LinkedList.Length; Item++)
                {
                    if (TempHead != -1)
                    {
                        if (LinkedList[TempHead].Name.ToUpper().Contains(Input.ToUpper()) && LinkedList[TempHead].Name != "Empty")
                        {
                            MessageText += LinkedList[TempHead].Name + ", memory location: " + TempHead + "\n";
                            Found = true;
                        }

                        TempHead = LinkedList[TempHead].Pointer;
                    }
                    else break;
                }

                // Display to user through a message box
                if (Found)
                {
                    MessageBox.Show(MessageText);
                }
                else
                {
                    MessageBox.Show(Input + " cannot be found in the linked list");
                }
            }

            // Delete whats in the Search box
            txtSearch.Text = "";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Initialise varibles
            string Input = txtAdd.Text;

            if (Input != "")
            {
                try
                {
                    // Add into memory
                    int TempFreePointer = LinkedList[FreePointer].Pointer;
                    LinkedList[FreePointer].Name = Input;
                    LinkedList[FreePointer].Pointer = HeadPointer;
                    HeadPointer = FreePointer;
                    FreePointer = TempFreePointer;

                    // Display
                    lblDataOutput.Text = "";
                    lblFreeSpace.Text = "";
                    DisplayLinkedList();
                }
                catch
                {
                    MessageBox.Show("No free memory");
                }
            }

            // Delete whats in the Add box
            txtAdd.Text = "";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int Location = Int32.Parse(txtDelete.Text);         // Check input is an int

                if (LinkedList[Location].Name != "Empty")           // If the slot is isnt an empty slot
                {
                    Console.WriteLine("Checkpoint ");
                    int TempHead = HeadPointer;
                    int TempPointer = LinkedList[Location].Pointer;         // Look at pointer

                    if (Location == HeadPointer)            // Set head pointer to our deleted cell's pointer if the head pointer pointed to our deleted cell
                    {
                        HeadPointer = TempPointer;
                    }
                    else               // Otherwise find which node points to that deleted cell and set its pointer to the deleted cell's pointer
                    {
                        for (int Item = 0; Item < LinkedList.Length; Item++)            // Loop through however many items there are
                        {
                            if (TempHead != -1)         // if its not the end of the list
                            {
                                if (LinkedList[TempHead].Pointer == Location)           // check if we found the node that points to the node we are deleting
                                {
                                    LinkedList[TempHead].Pointer = TempPointer;
                                    break;
                                }

                                TempHead = LinkedList[TempHead].Pointer;
                            }
                            else break;
                        }
                    }
                    
                    // Set pointers back to correct values
                    int TempFree = FreePointer;
                    FreePointer = Location;
                    LinkedList[Location].Pointer = TempFree;
                    LinkedList[Location].Name = "Empty";

                    // Display
                    lblDataOutput.Text = "";
                    lblFreeSpace.Text = "";
                    DisplayLinkedList();
                }
            }
            catch
            {
                MessageBox.Show("Enter a correct memory slot");
            }

            // Delete whats in the Delete box
            txtDelete.Text = "";
        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            // Initialise variables
            int TempHead = HeadPointer;
            string Output = "";

            // Loop through the pointers and add to a string
            for (int Item = 0; Item < LinkedList.Length; Item++)
            {
                if (TempHead != -1)
                {
                    Output += LinkedList[TempHead].Name + ", ";

                    TempHead = LinkedList[TempHead].Pointer;
                }
                else break;
            }

            // Output results to user
            if (Output.Length > 0)
            {
                Output = Output.Remove(Output.Length - 2, 2);       // Remove last two characters from the last ', '
            }
            else
            {
                Output = "Nothing found";
            }

            MessageBox.Show(Output, "Msg Boxes", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
