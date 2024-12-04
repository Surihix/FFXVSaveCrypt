# FFXVSaveCrypt
This tool allows you to decrypt and encrypt save data related files from FINAL FANATSY XV.

The program should be launched from command prompt with a function switch along with a valid input file. a list of valid function switches and the supported files, are given below.

**Function Switches:**
<br>``-d`` - For decryption
<br>``-e`` - For encryption

**Supported files:**
<br>- ``avatar0.save``
<br>- ``avatar0_mod.save``
<br>- ``gameplay0.save``
<br>- ``gameplay_mission_temporary0.save``
<br>- ``live_config_data.save``
<br>- ``snapshotlink.sl``
<br>- ``system_data.save``
<br>- ``universal_data.save``

<br>**Commandline usage examples:**
<br>`` FFXVSaveCrypt.exe -d "gameplay0.save" ``
<br>`` FFXVSaveCrypt.exe -e "gameplay0.save" ``

<br>**Note:** A backup of the file that you have specified after the function switch, will be created when using the tool's functions.

# Credits
- [yretenai](https://github.com/yretenai) - for helping with the crypto
  
