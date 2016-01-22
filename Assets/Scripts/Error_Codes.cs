using UnityEngine;
using System.Collections;

public class Error_Codes : MonoBehaviour {

    public int Error_Code_Number;
    public string Error_Text;

    public void GetText()
    {
        switch (Error_Code_Number)
        {
            case 0:
                Error_Text = "System Appears to be operating normally";
                break;

            case 1:
                Error_Text = "\n" + "Error #1: Packet Loss." + "\n" + "\n" + "Packet Loss inhibits the ability of the terminal to send and recieve data from the network. " + "\n" + "Severe packet loss can result in lost orders, frozen or inoperable termainls.";
                break;

            case 2:
                Error_Text = "\n" + "Error #2: Input,Output errors on Hard Drive. " + "\n" + "\n" + "IO errors are caused when sectors on the terminal's HDD can no longer be read or written to. " + "\n" + "IO errors can result in the loss of data, slow performance, crashes, freezes and eventually, an inoperable terminal.";
                break;

            case 3:
                Error_Text = "\n" + "Error #3: Printer Port Errors. " + "\n" + "\n" + "" + "\n" + "Printer errors will result in the terminal being unable to print receipts, unable open the cash drawer, and freezing after placing or cashing out an order.";
                break;

            case 4:
                Error_Text = "\n" + "Error #4: Run Level is not '4'. " + "\n" + "\n" + "A terminal that is not on a Run Level of '4' will be unable to communicate with other devices on the network. " + "\n" + "This may lead to a frozen terminal showing the message 'Awaiting Connection to POS application'";
                break;

            case 5:
                Error_Text = "\n" + "Error #5: Printer Simlink issues. " + "\n" + "\n" + "Printer was found attached and configured, but an error was encountered with the Simlink used for printing. " + "\n" + "This may cause the terminal to be unable to print or open the cashdrawer as well as potentially freeze during cashout.";
                break;

            case 6:
                Error_Text = "\n" + "Error #6: Printer is Configured but not attached. " + "\n" + "\n" + "A printer has been configured for this terminal but none was found to be attached. " + "\n" + "If a printer is NOT meant to be used on this location, a Help Desk Analyst will need to update your configuration. " + "\n" + "Otherwise, an issue may be causing your printer to not appear as attached. This commonly means the printer is unplugged from the terminal or powered off.";
                break;

            case 7:
                Error_Text = "\n" + "Error #7: Printer Attached but is not configured. " + "\n" + "\n" + "In order to print and open a cash drawer, the printer must be configured. " + "\n" + "This must be done by the Help Desk.";
                break;

            case 8:
                Error_Text = "\n" + "Error #8: Touch Screen not detected. " + "\n" + "\n" + "Without a touch screen connected to the terminal, only mouse input will be allowed." + "\n";
                break;

            case 9:
                Error_Text = "\n" + "Error #9: Kitchen Device Status is not 'LIVE'. " + "\n" + "\n" + "A Kitchen Device with a non 'LIVE' status is either disconnected, not initialized, or not functioning properly." + "\n";
                break;

            case 10:
                Error_Text = "\n" + "Error #10: No Bump Bar Attached to KMX device. " + "\n" + "\n" + "A KMX device with no bump bar attached will be unable to bump orders or print tickets." + "\n" + "This is most commonly caused by a disconnected bump bar, excessive uptime, or a damaged device" + "\n";
                break;

            case 11:
                Error_Text = "\n" + "Error #11: Printer out of Paper. " + "\n" + "\n" + "" + "\n" + "Printer out of paper result in the terminal being unable to print receipts, unable open the cash drawer, and freezing after placing or cashing out an order.";
                break;
        }
    }

}
