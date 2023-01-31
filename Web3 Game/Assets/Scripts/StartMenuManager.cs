using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Thirdweb;
using System.Threading.Tasks;

public class StartMenuManager : MonoBehaviour
{

    private ThirdwebSDK sdk;
    public GameObject ConnectedState;
    public GameObject DisconnectedState;
    public GameObject ConnectWalletState;
    public GameObject SelectWallet;
    public GameObject HasNFTState;
    public GameObject NoNFTState;
    public Toggle DragonToggle;
    public GameObject StartGameButton;

    // Start is called before the first frame update
    void Start()
    {
        sdk = new ThirdwebSDK("mumbai");
        ConnectedState.SetActive(false);
        DisconnectedState.SetActive(true);
        HasNFTState.SetActive(false);
        NoNFTState.SetActive(false);
        DragonToggle.isOn = false;
    }

    void Update()
    {
        if(DragonToggle.isOn)
        {
            StartGameButton.SetActive(true);
        }
        else
        {
            StartGameButton.SetActive(false);
        }
    }

    public void SetSelectWallet()
    {
        ConnectWalletState.SetActive(false);
        SelectWallet.SetActive(true);
    }

    public async void ConnectMetaMask()
    {
        string address = 
            await sdk
            .wallet
            .Connect(new WalletConnection() {
                provider = WalletProvider.MetaMask,
                chainId = 80001
            });
        
        DisconnectedState.SetActive(false);
        ConnectedState.SetActive(true);

        ConnectedState
            .transform
            .Find("UserAddress")
            .GetComponent<TMPro.TextMeshProUGUI>()
            .text = address;
            
        string checkBalance = await CheckBalance(address);

        float balanceFloat = float.Parse(checkBalance);

        if(balanceFloat > 0)
        {
            HasNFTState.SetActive(true);
            print("Has NFT");
        } else {
            NoNFTState.SetActive(true);
            print("No NFT");
        }
    }

    public async Task<string> CheckBalance(string address)
    {
        Contract contract = sdk.GetContract("0x1a24bD29aC136BC20191F0B79C4Ad4BbeAea6f66");

        string tokenId = "0";

        string balance = await contract.Read<string>("balanceOf", address, tokenId);
        print(balance);
        return balance;
    }

    public async void ClaimDragon()
    {
        string address = 
            await sdk
            .wallet
            .Connect(new WalletConnection() {
                provider = WalletProvider.MetaMask,
                chainId = 80001
            });
        Contract contract = sdk.GetContract("0x1a24bD29aC136BC20191F0B79C4Ad4BbeAea6f66");
        await contract.ERC1155.ClaimTo(address, "0", 1);
    }
}
