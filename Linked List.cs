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
        int HeadPointer = 2;
        int FreePointer = 5;
        Node[] LinkedList = new Node[10];

        // Create a node structure for each element
        struct Node
        {
            public string Name;
            public int Pointer;
        }

        public Form1()
        {
            InitializeComponent();
        }

        void CreateLinkedList()
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
            LinkedList[0].Pointer = -1;         // -1 as that is the end of the linked list

            int DataItems = FreePointer;

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

            DisplayLinkedList();
        }

        private void DisplayLinkedList()
        {
            lblHeadPointer.Text = "Head pointer: " + HeadPointer;
            lblFreePointer.Text = "Free Pointer: " + FreePointer;

            // Loops through the 'linked list' and displays all nodes
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

        // Searches through the linked list via the pointers
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string Input = txtSearch.Text;

            if (Input != "")
            {
                bool Found = false;
                int TempHead = HeadPointer;
                string MessageText = Input + " is found in: \n";

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

                // Display findings on screen
                if (Found)
                {
                    MessageBox.Show(MessageText);
                }
                else
                {
                    MessageBox.Show(Input + " cannot be found in the linked list");
                }
            }
        }

        // Adding an item into the linked list
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string Input = txtAdd.Text;

            if (Input != "")
            {
                // If the following does not work that means the memory is full
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
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CreateLinkedList();
        }
    }
}
