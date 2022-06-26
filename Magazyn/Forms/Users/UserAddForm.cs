﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Warehouse.Classes;
using Warehouse.Forms.Base;
using Warehouse.Models;
using Warehouse.Models.Dictionares;

namespace Warehouse.Forms.Users
{

    public partial class UserAddForm : BaseAddEditForm
    {
        #region Fields
        public EventHandler ReloadUsers;
        #endregion
        #region Constructor
        public UserAddForm()
        {
            InitializeComponent();
            InitializeData();
            ValidateControls();
        }
        #endregion
        #region PrivateMethods
        private void ValidateControls()
        {
            if (string.IsNullOrWhiteSpace(txtAddFirstName.Text))
            {
                epFirstName.SetError(txtAddFirstName, "First name is required");
            }
            else
            {
                epFirstName.Clear();
            }

            if (string.IsNullOrWhiteSpace(txtAddLastName.Text))
            {
                epLastName.SetError(txtAddLastName, "Last name is required");
            }
            else
            {
                epLastName.Clear();
            }

            if (string.IsNullOrWhiteSpace(txtAddPhoneNumber.Text))
            {
                epPhoneNumber.SetError(txtAddPhoneNumber, "Phone number is required");
            }
            else
            {
                epPhoneNumber.Clear();
            }

            if (string.IsNullOrWhiteSpace(txtAddMail.Text))
            {
                epMail.SetError(txtAddMail, "Mail is required");
            }
            else
            {
                epMail.Clear();
            }
        }

        private void InitializeData()
        {
            IList<SexModel> sexes = new List<SexModel>()
            {
                new SexModel("woman"),
                new SexModel("man"),
                new SexModel(string.Empty)
            };
            bsSex.DataSource = sexes;
            cBAddSex.Text = string.Empty;
        }

        private bool ValidateForm()
        {
            StringBuilder sbErrorMessage = new StringBuilder();

            string lastNameErrorMessage = epLastName.GetError(txtAddLastName);
            if (!string.IsNullOrEmpty(lastNameErrorMessage))
            {
                sbErrorMessage.Append(lastNameErrorMessage);
            }

            string firstNameErrorMessage = epFirstName.GetError(txtAddFirstName);
            if (!string.IsNullOrEmpty(firstNameErrorMessage))
            {
                sbErrorMessage.Append(firstNameErrorMessage);
            }
            string phoneNumberErrorMessage = epPhoneNumber.GetError(txtAddPhoneNumber);
            if (!string.IsNullOrEmpty(phoneNumberErrorMessage))
            {
                sbErrorMessage.Append(phoneNumberErrorMessage);
            }
            string MailErrorMessage = epMail.GetError(txtAddMail);
            if (!string.IsNullOrEmpty(MailErrorMessage))
            {
                sbErrorMessage.Append(MailErrorMessage);
            }

            if (sbErrorMessage.Length > 0)
            {
                MessageBox.Show(
                    sbErrorMessage.ToString(),
                    "Dodawanie pracownika",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }



            return true;
        }
        #endregion
        #region Events
        private void btnSaveUser_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void btnCancelUser_Click(object sender, EventArgs e)
        {
            Cancel();
        }

        private void txtAddFirstName_TextChanged(object sender, EventArgs e)
        {
            ValidateControls();
        }

        private void txtAddLastName_TextChanged(object sender, EventArgs e)
        {
            ValidateControls();
        }

        private void txtAddPhoneNumber_TextChanged(object sender, EventArgs e)
        {
            ValidateControls();
        }

        private void txtAddMail_TextChanged(object sender, EventArgs e)
        {
            ValidateControls();
        }

        private void txtAddPhoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtAddPhoneNumber_Validated(object sender, EventArgs e)
        {
            //string phnumber = txtAddPhoneNumber.Text;
            //if(!string.IsNullOrWhiteSpace(phnumber))
            //{
            //epPhoneNumber.SetError(txtAddPhoneNumber, "Wrong phone number");
            //}
            //else
            //{
            //epPhoneNumber.Clear();
            //}
        }
        #endregion
        #region Override
        protected override void Cancel()
        {
            Close();
        }
        protected override void Save()
        {
            if (ValidateForm())
            {
                UserModel user = new UserModel()
                {
                    LastName = txtAddLastName.Text,
                    FirstName = txtAddFirstName.Text,
                    Sex = new SexModel(cBAddSex.Text),
                    DateBirth = dTAddDate.Value,
                    PhoneNumber = txtAddPhoneNumber.Text,
                    Email = txtAddMail.Text,
                    Status = new StatusModel("Wprowadzony")
                };

                //employee = CreateEmployee(employee);
                user.UserId = 5;
                user.Code = 5;

                ReloadUsers?.Invoke(btnSaveUser, new UserEventArgs(user));
                MessageBox.Show("Saved");
                Close();
            }
        }
        #endregion










    }
}
