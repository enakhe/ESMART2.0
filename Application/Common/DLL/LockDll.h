#ifndef __MAKE_CARD_H__
#define __MAKE_CARD_H__
    


#ifdef __cplusplus
	extern "C" { 
#endif



/*=============================================================================
��������                        LS_SelectDoorLockType
;
�����ܣ�ѡ��Ƭ����
��  �룺DoorType -- ��������(Ҳ����ʹ�õĿ�Ƭ����)
��  ��: ��
����ֵ����������
=============================================================================*/
int __stdcall LS_SelectDoorLockType(int DoorType);

/*=============================================================================
��������                        LS_OpenPort
;
�����ܣ��򿪶˿�, ���Ӷ�����
��  �룺Port -- �˿ں�, ���ڿ���1~4, USB����Ϊ5
��  ��: ��
����ֵ����������
=============================================================================*/
int __stdcall LS_OpenPort(int Port);

/*=============================================================================
��������                        LS_ReadSystemPas
;
�����ܣ���ȡ��Ȩ���еĿͻ���, ���Զ�ȡMifare��������
��  �룺��
��  ��: Password -- 16���ַ�, ǰ8��Ϊ�ͻ���, ��9~10��ΪMifare��������
����ֵ����������
=============================================================================*/
int __stdcall	LS_ReadSystemPas(char *funPsw, char *Password, char *cardSnr);


/*=============================================================================
��������                        LS_SystemInitialization
;
�����ܣ�ϵͳ��ʼ��, Ҳ�������ÿͻ����Mifare�����ŵȵ���̬����
��  �룺Password -- 16���ַ�. ǰ8��Ϊ�ͻ���, ��9~10��ΪMifare��������
��  ��: ��
����ֵ����������
=============================================================================*/
int __stdcall LS_SystemInitialization(char *funPsw, char *Password);

/*=============================================================================
��������                        LS_ReadRom
;
�����ܣ���ȡ����(��Ƭ������Ψһ�����к�)
��  ��: ��
��  ��: cardSnr         --  ����: 16���ַ�
����ֵ����������
=============================================================================*/
int __stdcall LS_ReadRom(char *cardSnr);


/*=============================================================================
��������                        LS_MakeStaffCardEx1
;
�����ܣ�����Ա����
��  �룺
        RoomList    --  �����б�: ���50���ͷ�,  ¥����.¥���.����.�ӷ���  ����:  "001.002.00003.A   001.002.00005"
        AreaList    --  �����б�: ���8������, ����: "001.002.003.004.005.006.007.008"
        SDateTime   --  ��ʼ���ڣ�������ʱ����
        EDateTime   --  �������ڣ�������ʱ����
        Time1       --  ʱ���1: ��ʼʱ���Сʱ/���� + ����ʱ���Сʱ/����, 4
        Time2       --  ʱ���2
        Time3       --  ʱ���3
        FacultyNo   --  �ֿ���Ժϵ ����, 1~255
        MajorNo     --  �ֿ���רҵ ����, 1~65535
        ClassNo     --  �ֿ��˰༶ ����, 1~65535�� Ԥ���� ��ʱ����
        UserName    -- �û���, ���13��Ӣ����ĸ����6������
        iStaffProhibitedDayOfWeek -- һ�����ڽ��������.  bit0==1��ʾ��һ����, bit6==1��ʾ���ս���.  (bit0~bit7��ʾ��һ������)
        cStaffProhibitedDays --���������. ʮ�������ַ���, һ���ֽ�ռ�����ַ�, ����"1122AACD", ��ʾdays[]={0x11,0x22,0xAA,0xCD} ��. ��SDateTime�����ڿ�ʼ, days[0].bit0~bit7��ʾ��SDateTime��ʼ�ĵ�һ~�ڰ���(SDateTime���������һ��). 
                        days[1].bit0~bit7��ʾ��SDateTime��ʼ�ĵھ�~��ʮ����. ĳbit==1, ���ʾ�����ڽ�ֹ����.  ���366��. 

        options     --  ѡ��: bit0: ����; δ�õ���λ������
��  ��: cardSnr         -- ����: 8
����ֵ����������
ע��:   
char    cStaffProhibitedDays[200]; // Ա�������������(��SDateTime�����ڿ�ʼ, SDateTime���һ��) 
=============================================================================*/
int __stdcall LS_MakeStaffCardEx1(char *cardSnr, char *RoomList, char *AreaList, char *SDateTime,char *EDateTime, char *STime1, char* ETime1, char *STime2, char* ETime2, char *STime3, char* ETime3, \
                                     int FacultyNo, int MajorNo, int ClassNo, char *UserName, char *LostCardSn, int iStaffFlags, int iReplaceNo, int iStaffProhibitedDayOfWeek, char *cStaffProhibitedDays, int *iRFU);

/*=============================================================================
��������                        LS_ReadStaffCard
;
�����ܣ���ȡԱ������Ϣ
��  ��: ��
��  ����
        RoomList    --  �����б�: ���50���ͷ�,  ¥����.¥���.����.�ӷ���  ����:  "001.002.00003.A   001.002.00005"
        AreaList    --  �����б�: ���8������, ����: "001.002.003.004.005.006.007.008"
        SDateTime   --  ��ʼ���ڣ�������ʱ����
        EDateTime   --  �������ڣ�������ʱ����
        Time1       --  ʱ���1: ��ʼʱ���Сʱ/���� + ����ʱ���Сʱ/����, 4
        Time2       --  ʱ���2
        Time3       --  ʱ���3
        FacultyNo   --  �ֿ���Ժϵ ����, 1~255
        MajorNo     --  �ֿ���רҵ ����, 1~65535
        ClassNo     --  �ֿ��˰༶ ����, 1~65535�� Ԥ���� ��ʱ����
        UserName    -- �û���, ���13��Ӣ����ĸ����6������

��  ��: cardSnr         -- ����: 8
����ֵ����������
ע��:   
=============================================================================*/
int __stdcall LS_ReadStaffCard(char *cardSnr, char *RoomList, char *AreaList, char *SDateTime,char *EDateTime, char *STime1, char* ETime1, char *STime2, char* ETime2, char *STime3, char* ETime3, \
                                     int *FacultyNo, int *MajorNo, int *ClassNo, char *UserName, char *LostCardSn, int *iFlags);

/*=============================================================================
��������                        LS_CancelCard
;
�����ܣ�ע����Ƭ/��Ƭ����/�˷�
��  ��: ��
��  ����

��  ��: cardSnr         -- ����: 8
����ֵ����������
ע��:   
=============================================================================*/
int __stdcall LS_CancelCard(char *cardSnr);

/*=============================================================================
��������                        LS_BeepOk
;
�����ܣ�����������, ��ʾ�����ɹ�
��  �룺��
��  ��: ��
����ֵ����������
=============================================================================*/
void LS_BeepOk();

/*=============================================================================
��������                        LS_BeepFailure
;
�����ܣ�����������, ��ʾ����ʧ��
��  �룺��
��  ��: ��
����ֵ����������
=============================================================================*/
void LS_BeepFailure();


/*=============================================================================
��������                        LS_MakeInstallCard
;
�����ܣ�������װ��, �����趨�Ƿ�ʵ�ֱ��Ϳ����湦��.
��  ��: Building    --  ¥����
        Floor       --  ¥���, 1~255
        Room        --  ����, ��ʽ: ¥����.¥���.����.�ӷ���  ����:  "001.002.00003.A   001.002.00005"
        RoomType    --  �ͷ�������, ������ͨ�ͷ�/�׷����С��/¥�����ŵ�
        RoomDigits  --  ��¥���ͷ���ŵ�λ��(�������׼��), ���õ�Ϊ4����5
        LockSetting --  �����������ú�ѡ��ο�Defines.h
��  ��: cardSnr         --  ����: 16���ַ�
��  ��: Building='2', Floor=8, Room="20805", RoomType=1, RoomDigits=5, LockSetting=7
����ֵ����������
=============================================================================*/
int __stdcall LS_MakeInstallCard(char *cardSnr,int Building, int Floor, char *Room,int RoomType, int iAreaNo1, int iAreaNo2, int LockSetting, int iReplaceNo);


/*=============================================================================
��������				   LS_MakeLockSettingCardEx1

��  �룺
        iAreaNo -- �����
        iForbidCardType -- Ҫ��ֹ��������Ŀ�Ƭ. ���������Ŀ�Ƭ, ��Ӧ����(��Ƭ���� + 128)
        cForbidDateTime -- �������, ���ڿ�Ƭ�Զ��ָ�����
        SDateTime   --  ��ʼ���ڣ�������ʱ����
        EDateTime   --  �������ڣ�������ʱ����
        iFlags      --  ��Ƭ��־
        cSTime1     --  ʱ���1����ʼʱ��: ʱ����, �ַ�����ʽ "hh:mm:ss" 
        cETime1     --  ʱ���1�Ľ���ʱ��: ʱ����, �ַ�����ʽ "hh:mm:ss" 
        cSTime2     --  ʱ���2����ʼʱ��: ʱ����, �ַ�����ʽ "hh:mm:ss" 
        cETime2     --  ʱ���2�Ľ���ʱ��: ʱ����, �ַ�����ʽ "hh:mm:ss" 
        cSTime3     --  ʱ���3����ʼʱ��: ʱ����, �ַ�����ʽ "hh:mm:ss" 
        cETime3     --  ʱ���3�Ľ���ʱ��: ʱ����, �ַ�����ʽ "hh:mm:ss" 
                  
��  ��: cardSnr         -- ����: 8
����ֵ����������
ע��:         
=============================================================================*/
int __stdcall LS_MakeLockSettingCardEx1(char *cardSnr, int iAreaNo, int iForbidCardType, char *cForbidDateTime, char *SDateTime, char *EDateTime, int iRFU, char *cRFU, int iFlags, int iReplaceNo,\
                                        char *cSTime1, char *cETime1, char *cSTime2, char *cETime2,  char *cSTime3, char *cETime3);



/*=============================================================================
��������				   LS_MakeParaSettingCard
                               �����������ÿ�
��  �룺
        sysPara -- ϵͳ�����ṹ��
        bMaskBits1 -- λ�������ֽ�1
        bMaskBits2 -- λ�������ֽ�2
        bMaskBits3 -- λ�������ֽ�3
        bMaskBits4 -- bit0��Ӧ��"����ģʽ"
        bMaskBits5 -- bit0��Ӧ��"����ʱ��"
        iFlags      --  ��Ƭ��־
                  
��  ��: cardSnr         -- ����: 8
����ֵ����������
ע��:         
=============================================================================*/
int __stdcall LS_MakeParaSettingCard(char *cardSnr, SYS_PARA_SETTING sysPara, BYTE bMaskBits1, BYTE bMaskBits2, BYTE bMaskBits3, BYTE bMaskBits4, BYTE bMaskBits5, int iFlags);

/*=============================================================================
��������                        LS_MakeTimeCardEx1
;
�����ܣ�����Уʱ����չ1
��  ��: DateTime    --  Ҫ���õ�����ʱ��, ������ʱ����, �ַ�����ʽ"YYYY-MM-DD hh:mm:ss"
        cSummerTimeStart - ����ʱ��ʼ(BCD��): ��|�ڼ������ڼ�|����|������Сʱ|, ��ռ2�ַ�, ����4�·ݵ����һ�������յ�2:00 ����1��Сʱ, ���ַ���Ϊ"04|57|02|01|",  05��ʾ���һ������
        cSummerTimeEnd -   ����ʱ����(BCD��): ��|�ڼ������ڼ�|����|������Сʱ|, ��ռ2�ַ�, ����10�·ݵĵڶ��������ĵ�2:00 ����1��Сʱ,  ���ַ���Ϊ"10|24|02|01|"
        iFlags -- ��־�ֽ�

        ��  ��: cardSnr         --  ����: 16���ַ�
        ��  ��: DateTime="2008-06-06 12:30:00", iReplaceNo=0, cSummerTimeStart="04|57|02|01|", cSummerTimeEnd="10|24|02|01|"
����ֵ����������
=============================================================================*/
int __stdcall LS_MakeTimeCardEx1(char *cardSnr,char *DateTime, int iReplaceNo, char *cSummerTimeStart, char *cSummerTimeEnd, int iFlags);

/*=============================================================================
��������                        LS_MakeChiefCard
;
�����ܣ������ܿ�
��  ��: SDateTime   --  ��Ч�ڵ���ʼ����:  ������ʱ����, �ַ�����ʽ"YYYY-MM-DD hh:mm:ss", ��ΪԤ������
        EDateTime   --  ��Ч�ڵĽ�������:  ������ʱ����, �ַ�����ʽ"YYYY-MM-DD hh:mm:ss", ֻʹ�����ڲ���
        iFlags  --  ��Ƭ��־�ֽ�
��  ��: cardSnr         --  ����: 16���ַ�
��  ��: SDateTime="2008-06-06 00:00:00", EDateTime="2009-06-06 00:00:00"
����ֵ����������
=============================================================================*/
int __stdcall LS_MakeChiefCard(char *cardSnr, char *SDateTime, char *EDateTime, int iFlags, int iReplaceNo);

/*=============================================================================
��������                        LS_MakeEmergentCard
;
�����ܣ�����Ӧ����
��  ��: SDateTime   --  ��Ч�ڵ���ʼ����:  ������ʱ����, �ַ�����ʽ"YYYY-MM-DD hh:mm:ss", ��ΪԤ������
        EDateTime   --  ��Ч�ڵĽ�������:  ������ʱ����, �ַ�����ʽ"YYYY-MM-DD hh:mm:ss", ֻʹ�����ڲ���
        iFlags  --  ��־�ֽ�
��  ��: cardSnr         --  ����: 16���ַ�
��  ��: SDateTime="2008-06-06 00:00:00", EDateTime="2009-06-06 00:00:00"
����ֵ����������
=============================================================================*/
int __stdcall LS_MakeEmergentCard(char *cardSnr, char *SDateTime, char *EDateTime, int iFlags, int iReplaceNo);

/*=============================================================================
��������                        LS_MakeBuildingCard
;
�����ܣ�����¥����
��  ��: BuildingList --  ¥���б�, ���� "001.002.003.004.005"
        SDateTime   --  ��Ч�ڵ���ʼ����:  ������ʱ����, �ַ�����ʽ"YYYY-MM-DD hh:mm:ss", ��ΪԤ������
        EDateTime   --  ��Ч�ڵĽ�������:  ������ʱ����, �ַ�����ʽ"YYYY-MM-DD hh:mm:ss", ֻʹ�����ڲ���
        iFlags- ��־�ֽ�
��  ��: cardSnr         --  ����: 16���ַ�
��  ��: BuildingList='001.002.003.004.005', SDateTime="2008-06-06 00:00:00", EDateTime="2009-06-06 00:00:00"
����ֵ����������
=============================================================================*/
int __stdcall	LS_MakeBuildingCard(char *cardSnr, char *BuildingList, char *SDateTime, char *EDateTime, int iFlags, int iReplaceNo);

/*=============================================================================
��������                        LS_MakeFloorCard
;
�����ܣ�����¥�㿨
��  ��: Building    --  ¥����
        nStartFloor --  ��ʼ¥���: 1~255
        nEndFloor   --  ����¥���: 1~255, ��nStartFloorһ�������Ч¥�㷶Χ
        cSTime1     --  ʱ���1����ʼʱ��: ʱ����, �ַ�����ʽ "hh:mm:ss" 
        cETime1     --  ʱ���1�Ľ���ʱ��: ʱ����, �ַ�����ʽ "hh:mm:ss" 
        cSTime2     --  ʱ���2����ʼʱ��: ʱ����, �ַ�����ʽ "hh:mm:ss" 
        cETime2     --  ʱ���2�Ľ���ʱ��: ʱ����, �ַ�����ʽ "hh:mm:ss" 
        cSTime3     --  ʱ���3����ʼʱ��: ʱ����, �ַ�����ʽ "hh:mm:ss" 
        cETime3     --  ʱ���3�Ľ���ʱ��: ʱ����, �ַ�����ʽ "hh:mm:ss" 
        SDateTime   --  ��Ч�ڵ���ʼ����:  ������ʱ����, �ַ�����ʽ"YYYY-MM-DD hh:mm:ss", ��ΪԤ������
        EDateTime   --  ��Ч�ڵĽ�������:  ������ʱ����, �ַ�����ʽ"YYYY-MM-DD hh:mm:ss", ֻʹ�����ڲ���
        iFlags --  ��־�ֽ�

��  ��: cardSnr         --  ����: 16���ַ�

��  ��: Building='1', nStartFloor=1, nEndFloor=10, cSTime1="00:00:00", cETime1="23:59:00"
        cSTime2="00:00:00", cETime2="00:00:00", cSTime3="00:00:00", cETime3="00:00:00"
        SDateTime="2008-06-06 00:00:00", EDateTime="2009-06-06 00:00:00"

����ֵ����������
=============================================================================*/
int __stdcall LS_MakeFloorCard(char *cardSnr, int Building,  char *FloorList, char *cSTime1, char *cETime1, char *cSTime2, char *cETime2,  char *cSTime3, char *cETime3, char *SDateTime, char *EDateTime, int iFlags, int iReplaceNo);


/*=============================================================================
��������                        LS_MakeLostCard
;
�����ܣ�������ʧ��
��  ��: LostRom     --  Ҫ��ʧ�Ŀ���: 16���ַ�
��  ��: cardSnr         --  ����:  16���ַ�
��  ��: LostRom="1122334400000000"
����ֵ����������
=============================================================================*/
int __stdcall LS_MakeLostCard(char *cardSnr,char *LostRom, int iReplaceNo);

/*=============================================================================
��������                        LS_MakeUnLostCard
;
�����ܣ�����ȡ����ʧ��
��  ��: UnLostRom   --  Ҫȡ����ʧ�Ŀ���: 16���ַ�
��  ��: cardSnr         --  ����: 16���ַ�
��  ��: UnLostRom="1122334400000000"
����ֵ����������
=============================================================================*/
int __stdcall LS_MakeUnLostCard(char *cardSnr,char *UnLostRom, int iReplaceNo);

/*=============================================================================
��������                        LS_MakeDataCard
;
�����ܣ��������ݵ�����
��  ��: iWantLen        --  ϣ����ȡ���ֽ���, 57��ֻ֧��256��������
��  ��: cardSnr         --  ����: 16���ַ�
����ֵ����������
=============================================================================*/
int __stdcall LS_MakeDataCard(char *cardSnr, int iWantLen, int iReplaceNo);


/*=============================================================================
��������                        LS_ReadDataCard
;
�����ܣ������ݿ������ݶ�ȡ���ڴ���, ����ȡ������������. �˺�Ϳ�����
        ReadOneOpenDoorRecord���ڴ���������ȡ���ż�¼.
��  ��: lockInfo    --  ������Ϣ�ṹ��ָ��, ���������������Ϣ, ����Ϊ0
        datFromFile --  �Ƿ���ļ���ȡ��Ϣ��һ����0�����ֳֻ���ȡ��
        infoPsw     --  ��֤�룬һ����0
        dat         --  ������¼ԭʼ����ָ��, һ������Ϊ0
        iWantLen    --  ָ��Ҫ��ȡ���ֽ���, һ������Ϊ0.
��  ��: lockInfo    --  ������Ϣ�ṹ��
        dat         --  ���ż�¼ԭʼ����(16���������ַ�����ʾ,��СΪ32K�ֽ�)
��  ��: lockinfo=0, datFromFile=0, infoPsw=0, dat=0
����ֵ����������
=============================================================================*/
int __stdcall LS_ReadDataCard(LOCK_INFO *lockInfo, int datFromFile, int infoPsw, char *dat, int iWantLen);


/*=============================================================================
��������                        LS_ReadOneOpenDoorRecord
;
�����ܣ���ȡһ�����ż�¼
��  ��: ��
��  ��: Building     --  ¥����
        Room        --  ����, ��ͨ��5���ַ�, �׼䷿���滹��һ����ĸ��ʾ�׼��
        OpenKind    --  ��������
        OpenTime    --  ����ʱ��, ������ʱ����, �ַ�����ʽ"YYYY-MM-DD hh:mm:ss"
        cardSnr         --  ����: 16���ַ�
����ֵ����������
=============================================================================*/
int __stdcall	LS_ReadOneOpenDoorRecord(int *Building, char *Room, int *Kind, char *OpenTime, char *cardSnr);


/*=============================================================================
��������                        LS_DownloadRoomDataEx1
;
�����ܣ����ذ�װ���ݵ��ֳֻ���
��  ��: roomCnt -- roomList�еĿͷ�������
        roomList --  ROOM_INFO�����飬���δ�Ŷ���ͷ����ݡ�
        cSummerTimeStart - ����ʱ��ʼ: ��|�ڼ���������|����|������Сʱ|, ��ռ2�ַ�, ����4�·ݵ����һ�������յ�2:00 ����1��Сʱ, ���ַ���Ϊ"04|05|02|01|",  05��ʾ���һ������
        cSummerTimeEnd -   ����ʱ����: ��|�ڼ���������|����|������Сʱ|, ��ռ2�ַ�, ����10�·ݵĵڶ��������յ�2:00 ����1��Сʱ,  ���ַ���Ϊ"10|02|02|01|"
        iFlags -- ��־�ֽ�

��  ��: 
����ֵ����������
ע�⣺  ���صĿͷ����ݱ��밴��¥���š�¥��š���������Ȼ�����δ����roomList�С�
=============================================================================*/
int __stdcall LS_DownloadRoomDataEx1(int roomCnt, ROOM_INFO *roomList, char *cSummerTimeStart, char *cSummerTimeEnd, int iDownloadFlags);


/*=============================================================================
��������                        LS_MakeCheckoutCard
;
�����ܣ������˷���
��  ��: Building    --  ¥����
        FloorList   --  ¥���б�
        iFlags      --  �μ�Defines.h
        SDateTime   --  ��Ч�ڵ���ʼ����:  ������ʱ����, �ַ�����ʽ"YYYY-MM-DD hh:mm:ss", ��ΪԤ������
        EDateTime   --  ��Ч�ڵĽ�������:  ������ʱ����, �ַ�����ʽ"YYYY-MM-DD hh:mm:ss", ֻʹ�����ڲ���
        cStopTime   --  ��ֹʱ��: ������ʱ����, �ַ�����ʽ"YYYY-MM-DD hh:mm:ss", �ڴ�ʱ��֮ǰ���ı��Ϳ���Ա������ʧЧ.
                        һ����ǰ��Сʱ, ����������12:00, ������Ϊ11:30. 
��  ��: cardSnr         --  ����: 16���ַ�

��  ��: Building='1', StartFloor=1, EndFloor=10, WholeSys=0, WholeBuilding=0
        SDateTime="2008-06-06 00:00:00", EDateTime="2009-06-06 00:00:00", cStopTime="2000-00-00 00:00:00"
        ����ֵ����������
=============================================================================*/
int __stdcall LS_MakeCheckoutCard(char *cardSnr, int Building, char *FloorList, char *SDateTime, char *EDateTime, char *cStopTime, int iFlags, int iReplaceNo);


/*************************************************************************
��������                LS_GetCardInformation
�����ܣ���ȡ��Ƭ��Ϣ
�Ρ�����datFromFile --  �Ƿ���ļ�����ȡ��Ƭ��Ϣ��һ����0���ӿ�Ƭ�ж�ȡ��
        infoPsw     --  ��֤�룬һ����0.
        CardInfo    --  ��Ƭ���ݽṹ��ָ��
        dat         --  ��Ƭ����ָ��, һ����0

�����  CardInfo    -- ��Ƭ���ݽṹ��
        dat         -- ��Ƭ����(16���������ַ�����ʾ, 100�ֽ�)��һ�㲻�����
����ֵ����������
�衡����ע��Ҫ�ȸ�����������,���ú�������Ȩ�뵽��̬��.���ú�����
*************************************************************************/
int __stdcall LS_GetCardInformation(CARD_INFO *CardInfo, int datFromFile, int infoPsw, char *dat);


/*=============================================================================
��������                       LS_SaveRegisterCode
;
�����ܣ�������ע��
��  ��: RegCode      --  ע����, 27���ַ�, ��ʽ:xxxxxx-xxxxxx-xxxxxx-xxxxxx
��  ��: ��
����ֵ����������
=============================================================================*/
int __stdcall LS_SaveRegisterCode(char *RegCode);


/*=============================================================================
��������                       LS_GenMachineCode
;
�����ܣ����ɻ�����
��  ��: oprType     -- ��������, �������ע��ʱ��0
��  ��: machineCode -- ��Ȩ�ţ�27���ַ�, ��ʽ:xxxxxx-xxxxxx-xxxxxx-xxxxxx
����ֵ����������
=============================================================================*/
int __stdcall LS_GenMachineCode(char *machineCode, int oprType);


/*=============================================================================
��������                        LS_ChkRegisterInfo
;
�����ܣ�ע����, ���ע������,������ע��; ����ʹ������
��  ��: ��
��  ��: allowedDays -- ʣ����Ȩ����, 65535Ϊ������ʹ��, 0��������ʹ��
����ֵ����������
=============================================================================*/
int __stdcall LS_ChkRegisterInfo(int *allowedDays);


/*=============================================================================
��������                        LS_GenClientCode
;
�����ܣ��Զ�������Ȩ��
��  �룺iflags   -- iflags.0==1, ��ʾ����������Ȩ��(��װʩ����)
��  ��: Password -- 16���ַ�, ��Ӧ8�ֽ�, ǰ4�ֽ�Ϊ�ͻ���, ��5���ֽ�M1��������, 
                    ��6���ֽ�Ϊ������.
����ֵ����������
=============================================================================*/
int __stdcall LS_GenClientCode(char *funPsw, char *Password, int iflags);


/*=============================================================================
��������                        LS_GetClientFromCard
;
�����ܣ����ܿ��лָ���Ȩ��
��  �룺
��  ��: ��
����ֵ����������
=============================================================================*/
int __stdcall LS_GetClientFromCard(char *funPsw, char *Password, int iflags);


///////////////////////////////////////////////////////////////////////////////////////
int __stdcall LS_ClosePort(int Port);
int __stdcall LS_ReadGuestCard(char *cardSnr,char *Room, char *SDateTime, char *EDateTime);

/*=============================================================================
��������                        LS_MakeGuestCard_EX1
;
�����ܣ��������Ϳ��� ���������Ϳ���־�Ϳյ���־
��  �룺RoomList    --  �����б�:   �ַ���, M1�������4���ͷ���������ֻ֧��һ���ͷ������� "1.2.8203.A  1.2.8205"
        AreaList    --  �����б�
        ElevatorFloorList   --  ����¥���б�(���4��), ���������ʹ�õĵ���¥��. ���� "001.002.003.004.005"
        SDateTime   --  ��סʱ�䣺  ������ʱ����, �ַ�����ʽ "YYYY-MM-DD hh:mm:ss"
        EDateTime   --  Ԥ��ʱ�䣺  ������ʱ����, �ַ�����ʽ "YYYY-MM-DD hh:mm:ss"
        iFlags      --  ���Ϳ�ѡ��, �μ�Defines�е�GUEST_FLAGS����
��  ��: cardSnr         -- ����:        16���ַ�
��  ��: Room="1.2.8203.A", AreaList="001.002.005", ElevatorFloorList="001.002.003.004.005", DateTime="2008-06-06 12:30:59", EDateTime="2008-06-07 12:00:00"
        iFlags=0
����ֵ����������
=============================================================================*/
int __stdcall LS_MakeGuestCard_EX1(char *cardSnr, char *RoomList, char *AreaList, char *ElevatorFloorList, char *SDateTime,char *EDateTime, int iFlags);
int __stdcall LS_MakeGuestCard(char *cardSnr, char *RoomList, char *AreaList, char *SDateTime,char *EDateTime, int iFlags);
/*=============================================================================
��������                        LS_MakeBackupCard
;
�����ܣ������󱸿�
��  �룺Room        --  ����:       �ַ���, ���� "001.002.00003.A"
        SDateTime   --  ��סʱ�䣺  ������ʱ����, �ַ�����ʽ "YYYY-MM-DD hh:mm:ss"
        EDateTime   --  Ԥ��ʱ�䣺  ������ʱ����, �ַ�����ʽ "YYYY-MM-DD hh:mm:ss"
        iFlags      --  ���Ϳ�ѡ��, �μ�Defines�еĶ���
��  ��: cardSnr         -- ����:        16���ַ�
��  ��: Room="001.002.00003.A", SDateTime="2008-06-06 12:30:59", EDateTime="2008-06-07 12:00:00"
        iFlags=0
����ֵ����������
=============================================================================*/
int __stdcall LS_MakeBackupCard(char *cardSnr, char *Room, char *SDateTime,char *EDateTime, int iFlags, int iReplaceNo);

/*=============================================================================
��������                       LS_VerifyOperate
;
�����ܣ���֤����, ���ڲ��������, ������֤����߱Ƚ���֤��, ����һЩ��Ҫ��֤�����
        �Ĳ���.
��  ��: mode        -- ����ģʽ: 1=���������; 2=������֤��; 3=�Ƚ���֤��
        verifyCode  -- ��֤�� 
        funPsw      -- ��������

��  ��: randomCode  -- �����, 5λ��
        verifyCode  -- ��֤��, 5λ��
����ֵ����������
=============================================================================*/
int __stdcall LS_VerifyOperate(int mode, char *randomCode, char *verifyCode, char *funPsw);


/*=============================================================================
��������                        LS_MakeSampleChief
;
�����ܣ����������ܿ�
��  ��: ��
��  ��: ��
����ֵ����������
=============================================================================*/
int __stdcall LS_MakeSampleChief(char *cardSnr);

/*=============================================================================
��������                        LS_HandsimConfig
;
�����ܣ������ֳֻ�
��  ��: roomCnt -- roomList�еĿͷ�������
        roomList --  ROOM_INFO�����飬���δ�Ŷ���ͷ����ݡ�
		configMask:  ����, ���������ֳֻ��ϵ���Щ����
��  ��: 
ע�⣺//configMask ��ȡֵ
#define MS_OPERATOR_PSW		(1<<0)		//���²���Ա����
#define MS_MODULE_EN		(1<<1)		//���¹���ʹ��
#define MS_PARA1_UPDATE		(1<<2)		//�������ò���1
#define MS_PARA2_UPDATE		(1<<3)		//�������ò���2
=============================================================================*/
int __stdcall LS_HandsimConfig(UINT configMask, HANDSIM_INFO *handsimInfo) ;


/*=============================================================================
��������                        LS_GetEnabledModules
;
�����ܣ������Щ����ģ���Ѿ�ʹ��
��  ��: ��
��  ��: enabledModules -- bit0==1: �����ƴ�����;  bit1==1: ����FIAS�ӿ�
����ֵ����������
=============================================================================*/
int __stdcall LS_GetEnabledModules(int *enabledModules, int *iRFU1, char *cRFU1);

/*=============================================================================
��������                        LS_DS2460Read
;
�����ܣ���ȡDS2460
��  ��: addr -- ��ʼ��ַ
		len  -- ��ȡ���ֽ���
��  ��: buf  -- ��ȡ����Ϣ��buf��
����ֵ����������
=============================================================================*/
int __stdcall LS_DS2460Read(int addr, int len, BYTE *buf, char *psw);

/*=============================================================================
��������                        LS_DS2460Write
;
�����ܣ�дDS2460
��  ��: Addr -- ��ʼ��ַ�� 0~255
		len  -- д����ֽ�����1~64
		buf  -- ���ͻ�����, BYTE������
��  ��: ��
����ֵ����������
=============================================================================*/
int __stdcall LS_DS2460Write(int addr, int len, BYTE *buf, char *psw);

/*=============================================================================
��������                        LS_MakeElevatorCard
;
�����ܣ��������ݿ�
��  ��: Building    --  ¥����
        FloorList   --  ¥���б�(���5��), ���� "001.002.003.004.005"
        AreaList    --  �����б�: ���8������, ����: "001.002.003.004.005.006.007.008"
        cSTime1     --  ʱ���1����ʼʱ��: ʱ����, �ַ�����ʽ "hh:mm:ss" 
        cETime1     --  ʱ���1�Ľ���ʱ��: ʱ����, �ַ�����ʽ "hh:mm:ss" 
        cSTime2     --  ʱ���2����ʼʱ��: ʱ����, �ַ�����ʽ "hh:mm:ss" 
        cETime2     --  ʱ���2�Ľ���ʱ��: ʱ����, �ַ�����ʽ "hh:mm:ss" 
        cSTime3     --  ʱ���3����ʼʱ��: ʱ����, �ַ�����ʽ "hh:mm:ss" 
        cETime3     --  ʱ���3�Ľ���ʱ��: ʱ����, �ַ�����ʽ "hh:mm:ss" 
        SDateTime   --  ��Ч�ڵ���ʼ����:  ������ʱ����, �ַ�����ʽ"YYYY-MM-DD hh:mm:ss", ��ΪԤ������
        EDateTime   --  ��Ч�ڵĽ�������:  ������ʱ����, �ַ�����ʽ"YYYY-MM-DD hh:mm:ss", ֻʹ�����ڲ���
        iFlags --  ��־�ֽ�

��  ��: cardSnr         --  ����: 16���ַ�

��  ��: Building='1', FloorList="001.002.003.004.005", AreaList="001.002.003", cSTime1="00:00:00", cETime1="23:59:00"
        cSTime2="00:00:00", cETime2="00:00:00", cSTime3="00:00:00", cETime3="00:00:00"
        SDateTime="2008-06-06 00:00:00", EDateTime="2009-06-06 00:00:00"

����ֵ����������
=============================================================================*/
int __stdcall LS_MakeElevatorCard(char *cardSnr, int Building, char *FloorList, char *AreaList, char *SDateTime,char *EDateTime, char *cSTime1, char* cETime1, char *cSTime2, char* cETime2, char *cSTime3, char* cETime3, int iFlags, int iReplaceNo);


/*=============================================================================
��������				   LS_MakeJoinNetCard

��  �룺��
                  
��  ��: cardSnr         -- ����: 8
����ֵ����������
ע��:         
=============================================================================*/
int __stdcall LS_MakeJoinNetCard(char *cardSnr, char *cPara1, char *cPara2, int iPara3, int iPara4);

/*=============================================================================
��������				   M1_MakeExitNetCard

��  �룺��
                  
��  ��: cardSnr         -- ����: 8
����ֵ����������
ע��:         
=============================================================================*/
int __stdcall LS_MakeExitNetCard(char *cardSnr, char *cPara1, char *cPara2, int iPara3, int iPara4);


/*=============================================================================
��������				   LS_MakeParaSettingCard
                               �����Ž��������ÿ�
��  �룺
        para -- �Ž�/���ݲ����ṹ��
        iFlags      --  ��Ƭ��־
                  
��  ��: cardSnr         -- ����: 8
����ֵ����������
ע��:         
=============================================================================*/
int __stdcall LS_MakeAcsElevatorSettingCard(char *cardSnr, ACS_ELEVATOR_SET para, int iFlags);


int __stdcall LS_MakeTestCard(char *cardSnr);
int __stdcall LS_MakeFactoryCard(char *funPsw, char *UserPas,char *cardSnr);
int __stdcall LS_MakeStopCard(char *cardSnr, int iReplaceNo);
int __stdcall LS_MakeClientCard(char *funPsw,char *ClientPsw, int M1ClientCardSector, int M1LockNewSecotr, char *cardSnr);
int __stdcall LS_MakeManageCard(char *cardSn, int RFU1, int RFU2, int RFU3, char *RFU4, char *RFU5, int iReplaceNo);
int __stdcall LS_GetReaderSn(char *ReaderSn);
int __stdcall LS_ReadMem(int addr, int len, char *buf, int psw);
int __stdcall LS_WriteMem(int addr, int len, char *buf, int psw);
int __stdcall LS_GenRegisterCode(char *machineCode, int AllowedDays, int enabledModules, char* pswOptions, OUT char *regCode);
int __stdcall LS_MakeUpdateCard(char *cardSn);
void __stdcall LS_GetBeep();
void __stdcall LS_PCBeep();

#ifdef __cplusplus
   }
#endif

#endif              // __MAKE_CARD_H__