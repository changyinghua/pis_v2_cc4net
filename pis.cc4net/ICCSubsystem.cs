using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pis.cc4net
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICCSubsystem
    {

        /// <summary>
        /// 登入。
        /// </summary>
        /// <param name="user"></param>
        /// <param name="language"></param>
        /// <param name="staffGrade"></param>
         void Login(String user, bool isEnglish, String staffGrade);

        /// <summary>
        /// 登出。
        /// </summary>
        void Logout();

        /// <summary>
        /// This message has the same message format as log in message except with message type equal to “ASCII 13”. 
        /// The Sender to send this message to Receiver with modification only on the following field in the message:     
        /// </summary>
        void ChangeUserId();

        /// <summary>
        /// This message type allows the Sender sends the individual subsystem access mode to the Receiver. 
        /// </summary>
        /// <param name="accessPrivilege"></param>
        void ConfigPrivilege(string accessPrivilege);

        /// <summary>
        /// 錯誤訊息
        /// </summary>
        /// <param name="errorMsgType"></param>
        /// <param name="errorMessage"></param>
        void Error(string errorMsgType , string errorMessage);

        /// <summary>
        /// 將應用程式移至最上層。
        /// </summary>
        void MoveToTop();

        /// <summary>
        /// 設定應用程式位置
        /// </summary>
        /// <param name="xPos"></param>
        /// <param name="yPos"></param>
        void ChangePosition(int xPos , int yPos);

        /// <summary>
        /// 設定應用程式大小
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        void ChangeDimension(int width , int height);

        /// <summary>
        /// 設定應用程式熱鍵
        /// </summary>
        /// <param name="attributeType">
        /// 00:Apply All Attribute
        /// 01:alt+f4
        /// 02:alt+tab
        /// 03:Minimize Windows
        /// 04:Maximize/Restore Windows
        /// 05:Move Windows
        /// 06:Close Windows
        /// </param>
        /// <param name="attributeFlag">0:Disable attributeType , 1:Enable attribute</param>
        void Input(string attributeType, bool attributeFlag);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="counter"></param>
        void HealthCheck(int counter);

        void NewEmptyIncomingCallList();

        void NewEmptyOperationalList();
    }
}
