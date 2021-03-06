PGDMP                         y            newdb    10.16    13.3 ?    ?           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            ?           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            ?           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            ?           1262    12938    newdb    DATABASE     b   CREATE DATABASE newdb WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'Russian_Russia.1251';
    DROP DATABASE newdb;
                postgres    false            ?           0    0    DATABASE newdb    COMMENT     K   COMMENT ON DATABASE newdb IS 'default administrative connection database';
                   postgres    false    3028                        3079    16384 	   adminpack 	   EXTENSION     A   CREATE EXTENSION IF NOT EXISTS adminpack WITH SCHEMA pg_catalog;
    DROP EXTENSION adminpack;
                   false            ?           0    0    EXTENSION adminpack    COMMENT     M   COMMENT ON EXTENSION adminpack IS 'administrative functions for PostgreSQL';
                        false    1            ?            1255    24900     count_service(character varying)    FUNCTION       CREATE FUNCTION public.count_service(character varying) RETURNS bigint
    LANGUAGE sql IMMUTABLE STRICT
    AS $_$select count(history_service.id_service) from history_service inner join service on _name = $1 where history_service.id_service = service.id_service;$_$;
 7   DROP FUNCTION public.count_service(character varying);
       public          postgres    false            ?            1255    25387    insert_next_df()    FUNCTION     ?   CREATE FUNCTION public.insert_next_df() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN
INSERT INTO disease_factors (p_id) VALUES (CAST (TG_ARGV[0] AS INTEGER));
RETURN NEW;
END;
$$;
 '   DROP FUNCTION public.insert_next_df();
       public          postgres    false            ?            1259    25112    add_info    TABLE     ?   CREATE TABLE public.add_info (
    ai_id integer NOT NULL,
    p_id integer NOT NULL,
    snils character(14),
    kod_lgoty character(7),
    pasport character varying(150),
    oms character varying(150),
    strahov character varying(50)
);
    DROP TABLE public.add_info;
       public            postgres    false            ?            1259    25110    add_info_ai_id_seq    SEQUENCE     ?   CREATE SEQUENCE public.add_info_ai_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 )   DROP SEQUENCE public.add_info_ai_id_seq;
       public          postgres    false    198            ?           0    0    add_info_ai_id_seq    SEQUENCE OWNED BY     I   ALTER SEQUENCE public.add_info_ai_id_seq OWNED BY public.add_info.ai_id;
          public          postgres    false    197            ?            1259    25119    current_diagnosis    TABLE     ?   CREATE TABLE public.current_diagnosis (
    p_id integer NOT NULL,
    cd_date_mkb date NOT NULL,
    mkb_id integer NOT NULL,
    cd_date_ilar date NOT NULL,
    ilar_id integer NOT NULL
);
 %   DROP TABLE public.current_diagnosis;
       public            postgres    false            ?            1259    25126 
   diagn_ilar    TABLE     p   CREATE TABLE public.diagn_ilar (
    ilar_id integer NOT NULL,
    ilar_name character varying(100) NOT NULL
);
    DROP TABLE public.diagn_ilar;
       public            postgres    false            ?            1259    25124    diagn_ilar_ilar_id_seq    SEQUENCE     ?   CREATE SEQUENCE public.diagn_ilar_ilar_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 -   DROP SEQUENCE public.diagn_ilar_ilar_id_seq;
       public          postgres    false    201            ?           0    0    diagn_ilar_ilar_id_seq    SEQUENCE OWNED BY     Q   ALTER SEQUENCE public.diagn_ilar_ilar_id_seq OWNED BY public.diagn_ilar.ilar_id;
          public          postgres    false    200            ?            1259    25134 	   diagn_mkb    TABLE     ?   CREATE TABLE public.diagn_mkb (
    mkb_id integer NOT NULL,
    mkb_kod character varying(10) NOT NULL,
    mkb_name character varying(100) NOT NULL
);
    DROP TABLE public.diagn_mkb;
       public            postgres    false            ?            1259    25132    diagn_mkb_mkb_id_seq    SEQUENCE     ?   CREATE SEQUENCE public.diagn_mkb_mkb_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 +   DROP SEQUENCE public.diagn_mkb_mkb_id_seq;
       public          postgres    false    203            ?           0    0    diagn_mkb_mkb_id_seq    SEQUENCE OWNED BY     M   ALTER SEQUENCE public.diagn_mkb_mkb_id_seq OWNED BY public.diagn_mkb.mkb_id;
          public          postgres    false    202            ?            1259    25140    diagnosis_by_debut    TABLE     ?   CREATE TABLE public.diagnosis_by_debut (
    p_id integer NOT NULL,
    mkb_id integer NOT NULL,
    dbd_date_mkb date NOT NULL,
    dbd_date_ilar date NOT NULL,
    ilar_id integer NOT NULL
);
 &   DROP TABLE public.diagnosis_by_debut;
       public            postgres    false            ?            1259    25145    disability_status    TABLE     ?   CREATE TABLE public.disability_status (
    p_id integer NOT NULL,
    disabbility_now character varying(80),
    certif_of_dis character varying(50),
    social_psckage character varying(50),
    date_last date
);
 %   DROP TABLE public.disability_status;
       public            postgres    false            ?            1259    25150    disease_factors    TABLE     y  CREATE TABLE public.disease_factors (
    p_id integer NOT NULL,
    trauma character(1) DEFAULT '0'::bpchar,
    infection character(1) DEFAULT '0'::bpchar,
    vaccine character varying(100) DEFAULT '0'::character varying,
    hypothermia character(1) DEFAULT '0'::bpchar,
    insolation character(1) DEFAULT '0'::bpchar,
    other_factor character(1) DEFAULT '0'::bpchar
);
 #   DROP TABLE public.disease_factors;
       public            postgres    false            ?            1259    25163    doctors    TABLE     ?   CREATE TABLE public.doctors (
    doc_id integer NOT NULL,
    doc_surname character varying(25) NOT NULL,
    doc_name character varying(25) NOT NULL,
    doc_patr character varying(30)
);
    DROP TABLE public.doctors;
       public            postgres    false            ?            1259    25161    doctors_doc_id_seq    SEQUENCE     ?   CREATE SEQUENCE public.doctors_doc_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 )   DROP SEQUENCE public.doctors_doc_id_seq;
       public          postgres    false    208            ?           0    0    doctors_doc_id_seq    SEQUENCE OWNED BY     I   ALTER SEQUENCE public.doctors_doc_id_seq OWNED BY public.doctors.doc_id;
          public          postgres    false    207            ?            1259    25171    family    TABLE     ?   CREATE TABLE public.family (
    p_id integer NOT NULL,
    fam_id integer NOT NULL,
    fam_struct character varying(30) NOT NULL,
    fam_guardian character(1) NOT NULL
);
    DROP TABLE public.family;
       public            postgres    false            ?            1259    25178    family_anamnesis    TABLE     ?   CREATE TABLE public.family_anamnesis (
    p_id integer NOT NULL,
    psoriasis character(1) NOT NULL,
    uveitis character(1) NOT NULL,
    rheumatic character(1) NOT NULL,
    tumors character(1) NOT NULL
);
 $   DROP TABLE public.family_anamnesis;
       public            postgres    false            ?            1259    25169    family_fam_id_seq    SEQUENCE     ?   CREATE SEQUENCE public.family_fam_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 (   DROP SEQUENCE public.family_fam_id_seq;
       public          postgres    false    210            ?           0    0    family_fam_id_seq    SEQUENCE OWNED BY     G   ALTER SEQUENCE public.family_fam_id_seq OWNED BY public.family.fam_id;
          public          postgres    false    209            ?            1259    25185    fathers    TABLE     (  CREATE TABLE public.fathers (
    f_id integer NOT NULL,
    f_is character(1) NOT NULL,
    f_surname character varying(25) NOT NULL,
    f_name character varying(25) NOT NULL,
    f_patr character varying(30),
    f_dob date,
    f_status character varying(100),
    fam_id integer NOT NULL
);
    DROP TABLE public.fathers;
       public            postgres    false            ?            1259    25183    fathers_f_id_seq    SEQUENCE     ?   CREATE SEQUENCE public.fathers_f_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 '   DROP SEQUENCE public.fathers_f_id_seq;
       public          postgres    false    213            ?           0    0    fathers_f_id_seq    SEQUENCE OWNED BY     E   ALTER SEQUENCE public.fathers_f_id_seq OWNED BY public.fathers.f_id;
          public          postgres    false    212            ?            1259    25194    federal_centers    TABLE     ?   CREATE TABLE public.federal_centers (
    fc_id integer NOT NULL,
    fc_hczd character(1),
    fc_fgu character(1),
    fc_niir character(1),
    fc_dgkb character(1),
    fc_mgmy character(1)
);
 #   DROP TABLE public.federal_centers;
       public            postgres    false            ?            1259    25192    federal_centers_fc_id_seq    SEQUENCE     ?   CREATE SEQUENCE public.federal_centers_fc_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 0   DROP SEQUENCE public.federal_centers_fc_id_seq;
       public          postgres    false    215            ?           0    0    federal_centers_fc_id_seq    SEQUENCE OWNED BY     W   ALTER SEQUENCE public.federal_centers_fc_id_seq OWNED BY public.federal_centers.fc_id;
          public          postgres    false    214            ?            1259    25202    hospital    TABLE     ?   CREATE TABLE public.hospital (
    hos_id integer NOT NULL,
    hos_name character varying(200) NOT NULL,
    hos_city character varying(50) NOT NULL
);
    DROP TABLE public.hospital;
       public            postgres    false            ?            1259    25200    hospital_hos_id_seq    SEQUENCE     ?   CREATE SEQUENCE public.hospital_hos_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 *   DROP SEQUENCE public.hospital_hos_id_seq;
       public          postgres    false    217            ?           0    0    hospital_hos_id_seq    SEQUENCE OWNED BY     K   ALTER SEQUENCE public.hospital_hos_id_seq OWNED BY public.hospital.hos_id;
          public          postgres    false    216            ?            1259    25210    inf_about_consent    TABLE     ?   CREATE TABLE public.inf_about_consent (
    iac_id integer NOT NULL,
    iac_status character(1) NOT NULL,
    iac_date date
);
 %   DROP TABLE public.inf_about_consent;
       public            postgres    false            ?            1259    25208    inf_about_consent_iac_id_seq    SEQUENCE     ?   CREATE SEQUENCE public.inf_about_consent_iac_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 3   DROP SEQUENCE public.inf_about_consent_iac_id_seq;
       public          postgres    false    219            ?           0    0    inf_about_consent_iac_id_seq    SEQUENCE OWNED BY     ]   ALTER SEQUENCE public.inf_about_consent_iac_id_seq OWNED BY public.inf_about_consent.iac_id;
          public          postgres    false    218            ?            1259    25216    info_disease    TABLE     ?   CREATE TABLE public.info_disease (
    p_id integer NOT NULL,
    start_date date NOT NULL,
    date_of_appeal date NOT NULL
);
     DROP TABLE public.info_disease;
       public            postgres    false            ?            1259    25223    mothers    TABLE     (  CREATE TABLE public.mothers (
    m_id integer NOT NULL,
    m_is character(1) NOT NULL,
    m_surname character varying(25) NOT NULL,
    m_name character varying(25) NOT NULL,
    m_patr character varying(30),
    m_dob date,
    m_status character varying(100),
    fam_id integer NOT NULL
);
    DROP TABLE public.mothers;
       public            postgres    false            ?            1259    25221    mothers_m_id_seq    SEQUENCE     ?   CREATE SEQUENCE public.mothers_m_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 '   DROP SEQUENCE public.mothers_m_id_seq;
       public          postgres    false    222            ?           0    0    mothers_m_id_seq    SEQUENCE OWNED BY     E   ALTER SEQUENCE public.mothers_m_id_seq OWNED BY public.mothers.m_id;
          public          postgres    false    221            ?            1259    25363 	   pat_state    TABLE     ;  CREATE TABLE public.pat_state (
    p_id integer NOT NULL,
    ps_remission character(1) NOT NULL,
    ps_activity character varying(100) NOT NULL,
    ps_index_chaq numeric(2,1) NOT NULL,
    CONSTRAINT pat_state_ps_index_chaq_check CHECK (((ps_index_chaq >= (0)::numeric) AND (ps_index_chaq <= (3)::numeric)))
);
    DROP TABLE public.pat_state;
       public            postgres    false            ?            1259    25232    patients    TABLE     ?  CREATE TABLE public.patients (
    p_id integer NOT NULL,
    p_surname character varying(25) NOT NULL,
    p_name character varying(25) NOT NULL,
    p_patr character varying(30),
    p_dob date NOT NULL,
    p_gender character varying(8) NOT NULL,
    p_nationality character varying(50),
    p_live character varying(5) NOT NULL,
    p_phone bigint,
    p_email character varying(50),
    doc_id integer NOT NULL,
    hos_id integer NOT NULL,
    fc_id integer NOT NULL,
    iac_id integer NOT NULL
);
    DROP TABLE public.patients;
       public            postgres    false            ?            1259    25230    patients_p_id_seq    SEQUENCE     ?   CREATE SEQUENCE public.patients_p_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 (   DROP SEQUENCE public.patients_p_id_seq;
       public          postgres    false    224            ?           0    0    patients_p_id_seq    SEQUENCE OWNED BY     G   ALTER SEQUENCE public.patients_p_id_seq OWNED BY public.patients.p_id;
          public          postgres    false    223            ?            1259    25389    polzovateli    TABLE     e   CREATE TABLE public.polzovateli (
    login character varying(30),
    pass character varying(30)
);
    DROP TABLE public.polzovateli;
       public            postgres    false            ?            1259    25246    reg_addresses    TABLE     ?   CREATE TABLE public.reg_addresses (
    p_id integer NOT NULL,
    reg_id integer NOT NULL,
    reg_region character varying(50) NOT NULL,
    reg_district character varying(100) NOT NULL,
    reg_adress character varying(200) NOT NULL
);
 !   DROP TABLE public.reg_addresses;
       public            postgres    false            ?            1259    25244    reg_addresses_reg_id_seq    SEQUENCE     ?   CREATE SEQUENCE public.reg_addresses_reg_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 /   DROP SEQUENCE public.reg_addresses_reg_id_seq;
       public          postgres    false    226            ?           0    0    reg_addresses_reg_id_seq    SEQUENCE OWNED BY     U   ALTER SEQUENCE public.reg_addresses_reg_id_seq OWNED BY public.reg_addresses.reg_id;
          public          postgres    false    225            ?            1259    25255    res_addresses    TABLE     ?   CREATE TABLE public.res_addresses (
    p_id integer NOT NULL,
    res_id integer NOT NULL,
    res_region character varying(50) NOT NULL,
    res_district character varying(100) NOT NULL,
    res_adress character varying(200) NOT NULL
);
 !   DROP TABLE public.res_addresses;
       public            postgres    false            ?            1259    25253    res_addresses_res_id_seq    SEQUENCE     ?   CREATE SEQUENCE public.res_addresses_res_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 /   DROP SEQUENCE public.res_addresses_res_id_seq;
       public          postgres    false    228            ?           0    0    res_addresses_res_id_seq    SEQUENCE OWNED BY     U   ALTER SEQUENCE public.res_addresses_res_id_seq OWNED BY public.res_addresses.res_id;
          public          postgres    false    227            ?
           2604    25115    add_info ai_id    DEFAULT     p   ALTER TABLE ONLY public.add_info ALTER COLUMN ai_id SET DEFAULT nextval('public.add_info_ai_id_seq'::regclass);
 =   ALTER TABLE public.add_info ALTER COLUMN ai_id DROP DEFAULT;
       public          postgres    false    198    197    198            ?
           2604    25129    diagn_ilar ilar_id    DEFAULT     x   ALTER TABLE ONLY public.diagn_ilar ALTER COLUMN ilar_id SET DEFAULT nextval('public.diagn_ilar_ilar_id_seq'::regclass);
 A   ALTER TABLE public.diagn_ilar ALTER COLUMN ilar_id DROP DEFAULT;
       public          postgres    false    201    200    201            ?
           2604    25137    diagn_mkb mkb_id    DEFAULT     t   ALTER TABLE ONLY public.diagn_mkb ALTER COLUMN mkb_id SET DEFAULT nextval('public.diagn_mkb_mkb_id_seq'::regclass);
 ?   ALTER TABLE public.diagn_mkb ALTER COLUMN mkb_id DROP DEFAULT;
       public          postgres    false    203    202    203            ?
           2604    25166    doctors doc_id    DEFAULT     p   ALTER TABLE ONLY public.doctors ALTER COLUMN doc_id SET DEFAULT nextval('public.doctors_doc_id_seq'::regclass);
 =   ALTER TABLE public.doctors ALTER COLUMN doc_id DROP DEFAULT;
       public          postgres    false    207    208    208            ?
           2604    25174    family fam_id    DEFAULT     n   ALTER TABLE ONLY public.family ALTER COLUMN fam_id SET DEFAULT nextval('public.family_fam_id_seq'::regclass);
 <   ALTER TABLE public.family ALTER COLUMN fam_id DROP DEFAULT;
       public          postgres    false    209    210    210            ?
           2604    25188    fathers f_id    DEFAULT     l   ALTER TABLE ONLY public.fathers ALTER COLUMN f_id SET DEFAULT nextval('public.fathers_f_id_seq'::regclass);
 ;   ALTER TABLE public.fathers ALTER COLUMN f_id DROP DEFAULT;
       public          postgres    false    212    213    213            ?
           2604    25197    federal_centers fc_id    DEFAULT     ~   ALTER TABLE ONLY public.federal_centers ALTER COLUMN fc_id SET DEFAULT nextval('public.federal_centers_fc_id_seq'::regclass);
 D   ALTER TABLE public.federal_centers ALTER COLUMN fc_id DROP DEFAULT;
       public          postgres    false    215    214    215            ?
           2604    25205    hospital hos_id    DEFAULT     r   ALTER TABLE ONLY public.hospital ALTER COLUMN hos_id SET DEFAULT nextval('public.hospital_hos_id_seq'::regclass);
 >   ALTER TABLE public.hospital ALTER COLUMN hos_id DROP DEFAULT;
       public          postgres    false    216    217    217            ?
           2604    25213    inf_about_consent iac_id    DEFAULT     ?   ALTER TABLE ONLY public.inf_about_consent ALTER COLUMN iac_id SET DEFAULT nextval('public.inf_about_consent_iac_id_seq'::regclass);
 G   ALTER TABLE public.inf_about_consent ALTER COLUMN iac_id DROP DEFAULT;
       public          postgres    false    218    219    219            ?
           2604    25226    mothers m_id    DEFAULT     l   ALTER TABLE ONLY public.mothers ALTER COLUMN m_id SET DEFAULT nextval('public.mothers_m_id_seq'::regclass);
 ;   ALTER TABLE public.mothers ALTER COLUMN m_id DROP DEFAULT;
       public          postgres    false    222    221    222            ?
           2604    25235    patients p_id    DEFAULT     n   ALTER TABLE ONLY public.patients ALTER COLUMN p_id SET DEFAULT nextval('public.patients_p_id_seq'::regclass);
 <   ALTER TABLE public.patients ALTER COLUMN p_id DROP DEFAULT;
       public          postgres    false    224    223    224            ?
           2604    25249    reg_addresses reg_id    DEFAULT     |   ALTER TABLE ONLY public.reg_addresses ALTER COLUMN reg_id SET DEFAULT nextval('public.reg_addresses_reg_id_seq'::regclass);
 C   ALTER TABLE public.reg_addresses ALTER COLUMN reg_id DROP DEFAULT;
       public          postgres    false    226    225    226            ?
           2604    25258    res_addresses res_id    DEFAULT     |   ALTER TABLE ONLY public.res_addresses ALTER COLUMN res_id SET DEFAULT nextval('public.res_addresses_res_id_seq'::regclass);
 C   ALTER TABLE public.res_addresses ALTER COLUMN res_id DROP DEFAULT;
       public          postgres    false    227    228    228            ?          0    25112    add_info 
   TABLE DATA           X   COPY public.add_info (ai_id, p_id, snils, kod_lgoty, pasport, oms, strahov) FROM stdin;
    public          postgres    false    198   ?       ?          0    25119    current_diagnosis 
   TABLE DATA           ]   COPY public.current_diagnosis (p_id, cd_date_mkb, mkb_id, cd_date_ilar, ilar_id) FROM stdin;
    public          postgres    false    199   ?       ?          0    25126 
   diagn_ilar 
   TABLE DATA           8   COPY public.diagn_ilar (ilar_id, ilar_name) FROM stdin;
    public          postgres    false    201   `?       ?          0    25134 	   diagn_mkb 
   TABLE DATA           >   COPY public.diagn_mkb (mkb_id, mkb_kod, mkb_name) FROM stdin;
    public          postgres    false    203   ȹ       ?          0    25140    diagnosis_by_debut 
   TABLE DATA           `   COPY public.diagnosis_by_debut (p_id, mkb_id, dbd_date_mkb, dbd_date_ilar, ilar_id) FROM stdin;
    public          postgres    false    204   p?       ?          0    25145    disability_status 
   TABLE DATA           l   COPY public.disability_status (p_id, disabbility_now, certif_of_dis, social_psckage, date_last) FROM stdin;
    public          postgres    false    205   ź       ?          0    25150    disease_factors 
   TABLE DATA           r   COPY public.disease_factors (p_id, trauma, infection, vaccine, hypothermia, insolation, other_factor) FROM stdin;
    public          postgres    false    206   L?       ?          0    25163    doctors 
   TABLE DATA           J   COPY public.doctors (doc_id, doc_surname, doc_name, doc_patr) FROM stdin;
    public          postgres    false    208   ??       ?          0    25171    family 
   TABLE DATA           H   COPY public.family (p_id, fam_id, fam_struct, fam_guardian) FROM stdin;
    public          postgres    false    210   +?       ?          0    25178    family_anamnesis 
   TABLE DATA           W   COPY public.family_anamnesis (p_id, psoriasis, uveitis, rheumatic, tumors) FROM stdin;
    public          postgres    false    211   ??       ?          0    25185    fathers 
   TABLE DATA           a   COPY public.fathers (f_id, f_is, f_surname, f_name, f_patr, f_dob, f_status, fam_id) FROM stdin;
    public          postgres    false    213   ??       ?          0    25194    federal_centers 
   TABLE DATA           \   COPY public.federal_centers (fc_id, fc_hczd, fc_fgu, fc_niir, fc_dgkb, fc_mgmy) FROM stdin;
    public          postgres    false    215   ??       ?          0    25202    hospital 
   TABLE DATA           >   COPY public.hospital (hos_id, hos_name, hos_city) FROM stdin;
    public          postgres    false    217   ??       ?          0    25210    inf_about_consent 
   TABLE DATA           I   COPY public.inf_about_consent (iac_id, iac_status, iac_date) FROM stdin;
    public          postgres    false    219   g?       ?          0    25216    info_disease 
   TABLE DATA           H   COPY public.info_disease (p_id, start_date, date_of_appeal) FROM stdin;
    public          postgres    false    220   ??       ?          0    25223    mothers 
   TABLE DATA           a   COPY public.mothers (m_id, m_is, m_surname, m_name, m_patr, m_dob, m_status, fam_id) FROM stdin;
    public          postgres    false    222   ??       ?          0    25363 	   pat_state 
   TABLE DATA           S   COPY public.pat_state (p_id, ps_remission, ps_activity, ps_index_chaq) FROM stdin;
    public          postgres    false    229   ??       ?          0    25232    patients 
   TABLE DATA           ?   COPY public.patients (p_id, p_surname, p_name, p_patr, p_dob, p_gender, p_nationality, p_live, p_phone, p_email, doc_id, hos_id, fc_id, iac_id) FROM stdin;
    public          postgres    false    224   <?       ?          0    25389    polzovateli 
   TABLE DATA           2   COPY public.polzovateli (login, pass) FROM stdin;
    public          postgres    false    230   ??       ?          0    25246    reg_addresses 
   TABLE DATA           [   COPY public.reg_addresses (p_id, reg_id, reg_region, reg_district, reg_adress) FROM stdin;
    public          postgres    false    226   ??       ?          0    25255    res_addresses 
   TABLE DATA           [   COPY public.res_addresses (p_id, res_id, res_region, res_district, res_adress) FROM stdin;
    public          postgres    false    228   ??       ?           0    0    add_info_ai_id_seq    SEQUENCE SET     @   SELECT pg_catalog.setval('public.add_info_ai_id_seq', 4, true);
          public          postgres    false    197            ?           0    0    diagn_ilar_ilar_id_seq    SEQUENCE SET     E   SELECT pg_catalog.setval('public.diagn_ilar_ilar_id_seq', 1, false);
          public          postgres    false    200            ?           0    0    diagn_mkb_mkb_id_seq    SEQUENCE SET     C   SELECT pg_catalog.setval('public.diagn_mkb_mkb_id_seq', 1, false);
          public          postgres    false    202            ?           0    0    doctors_doc_id_seq    SEQUENCE SET     A   SELECT pg_catalog.setval('public.doctors_doc_id_seq', 1, false);
          public          postgres    false    207            ?           0    0    family_fam_id_seq    SEQUENCE SET     @   SELECT pg_catalog.setval('public.family_fam_id_seq', 1, false);
          public          postgres    false    209            ?           0    0    fathers_f_id_seq    SEQUENCE SET     ?   SELECT pg_catalog.setval('public.fathers_f_id_seq', 1, false);
          public          postgres    false    212            ?           0    0    federal_centers_fc_id_seq    SEQUENCE SET     G   SELECT pg_catalog.setval('public.federal_centers_fc_id_seq', 5, true);
          public          postgres    false    214            ?           0    0    hospital_hos_id_seq    SEQUENCE SET     B   SELECT pg_catalog.setval('public.hospital_hos_id_seq', 1, false);
          public          postgres    false    216            ?           0    0    inf_about_consent_iac_id_seq    SEQUENCE SET     K   SELECT pg_catalog.setval('public.inf_about_consent_iac_id_seq', 10, true);
          public          postgres    false    218            ?           0    0    mothers_m_id_seq    SEQUENCE SET     ?   SELECT pg_catalog.setval('public.mothers_m_id_seq', 1, false);
          public          postgres    false    221            ?           0    0    patients_p_id_seq    SEQUENCE SET     ?   SELECT pg_catalog.setval('public.patients_p_id_seq', 1, true);
          public          postgres    false    223            ?           0    0    reg_addresses_reg_id_seq    SEQUENCE SET     F   SELECT pg_catalog.setval('public.reg_addresses_reg_id_seq', 1, true);
          public          postgres    false    225            ?           0    0    res_addresses_res_id_seq    SEQUENCE SET     F   SELECT pg_catalog.setval('public.res_addresses_res_id_seq', 1, true);
          public          postgres    false    227            ?
           2606    25118    add_info add_info_pk 
   CONSTRAINT     [   ALTER TABLE ONLY public.add_info
    ADD CONSTRAINT add_info_pk PRIMARY KEY (ai_id, p_id);
 >   ALTER TABLE ONLY public.add_info DROP CONSTRAINT add_info_pk;
       public            postgres    false    198    198            ?
           2606    25123 &   current_diagnosis current_diagnosis_pk 
   CONSTRAINT     f   ALTER TABLE ONLY public.current_diagnosis
    ADD CONSTRAINT current_diagnosis_pk PRIMARY KEY (p_id);
 P   ALTER TABLE ONLY public.current_diagnosis DROP CONSTRAINT current_diagnosis_pk;
       public            postgres    false    199            ?
           2606    25144 (   diagnosis_by_debut diagnosis_by_debut_pk 
   CONSTRAINT     h   ALTER TABLE ONLY public.diagnosis_by_debut
    ADD CONSTRAINT diagnosis_by_debut_pk PRIMARY KEY (p_id);
 R   ALTER TABLE ONLY public.diagnosis_by_debut DROP CONSTRAINT diagnosis_by_debut_pk;
       public            postgres    false    204            ?
           2606    25149 &   disability_status disability_status_pk 
   CONSTRAINT     f   ALTER TABLE ONLY public.disability_status
    ADD CONSTRAINT disability_status_pk PRIMARY KEY (p_id);
 P   ALTER TABLE ONLY public.disability_status DROP CONSTRAINT disability_status_pk;
       public            postgres    false    205            ?
           2606    25160 "   disease_factors disease_factors_pk 
   CONSTRAINT     b   ALTER TABLE ONLY public.disease_factors
    ADD CONSTRAINT disease_factors_pk PRIMARY KEY (p_id);
 L   ALTER TABLE ONLY public.disease_factors DROP CONSTRAINT disease_factors_pk;
       public            postgres    false    206            ?
           2606    25168    doctors doctors_pk 
   CONSTRAINT     T   ALTER TABLE ONLY public.doctors
    ADD CONSTRAINT doctors_pk PRIMARY KEY (doc_id);
 <   ALTER TABLE ONLY public.doctors DROP CONSTRAINT doctors_pk;
       public            postgres    false    208                       2606    25182 $   family_anamnesis family_anamnesis_pk 
   CONSTRAINT     d   ALTER TABLE ONLY public.family_anamnesis
    ADD CONSTRAINT family_anamnesis_pk PRIMARY KEY (p_id);
 N   ALTER TABLE ONLY public.family_anamnesis DROP CONSTRAINT family_anamnesis_pk;
       public            postgres    false    211                        2606    25177    family family_pk 
   CONSTRAINT     R   ALTER TABLE ONLY public.family
    ADD CONSTRAINT family_pk PRIMARY KEY (fam_id);
 :   ALTER TABLE ONLY public.family DROP CONSTRAINT family_pk;
       public            postgres    false    210                       2606    25191    fathers fathers_pk 
   CONSTRAINT     Z   ALTER TABLE ONLY public.fathers
    ADD CONSTRAINT fathers_pk PRIMARY KEY (f_id, fam_id);
 <   ALTER TABLE ONLY public.fathers DROP CONSTRAINT fathers_pk;
       public            postgres    false    213    213                       2606    25199 "   federal_centers federal_centers_pk 
   CONSTRAINT     c   ALTER TABLE ONLY public.federal_centers
    ADD CONSTRAINT federal_centers_pk PRIMARY KEY (fc_id);
 L   ALTER TABLE ONLY public.federal_centers DROP CONSTRAINT federal_centers_pk;
       public            postgres    false    215            	           2606    25207    hospital hospital_pk 
   CONSTRAINT     V   ALTER TABLE ONLY public.hospital
    ADD CONSTRAINT hospital_pk PRIMARY KEY (hos_id);
 >   ALTER TABLE ONLY public.hospital DROP CONSTRAINT hospital_pk;
       public            postgres    false    217            ?
           2606    25131    diagn_ilar ilar_pk 
   CONSTRAINT     U   ALTER TABLE ONLY public.diagn_ilar
    ADD CONSTRAINT ilar_pk PRIMARY KEY (ilar_id);
 <   ALTER TABLE ONLY public.diagn_ilar DROP CONSTRAINT ilar_pk;
       public            postgres    false    201                       2606    25215 &   inf_about_consent inf_about_consent_pk 
   CONSTRAINT     h   ALTER TABLE ONLY public.inf_about_consent
    ADD CONSTRAINT inf_about_consent_pk PRIMARY KEY (iac_id);
 P   ALTER TABLE ONLY public.inf_about_consent DROP CONSTRAINT inf_about_consent_pk;
       public            postgres    false    219                       2606    25220    info_disease info_disease_pk 
   CONSTRAINT     \   ALTER TABLE ONLY public.info_disease
    ADD CONSTRAINT info_disease_pk PRIMARY KEY (p_id);
 F   ALTER TABLE ONLY public.info_disease DROP CONSTRAINT info_disease_pk;
       public            postgres    false    220            ?
           2606    25139    diagn_mkb mkb_pk 
   CONSTRAINT     R   ALTER TABLE ONLY public.diagn_mkb
    ADD CONSTRAINT mkb_pk PRIMARY KEY (mkb_id);
 :   ALTER TABLE ONLY public.diagn_mkb DROP CONSTRAINT mkb_pk;
       public            postgres    false    203                       2606    25229    mothers mothers_pk 
   CONSTRAINT     Z   ALTER TABLE ONLY public.mothers
    ADD CONSTRAINT mothers_pk PRIMARY KEY (fam_id, m_id);
 <   ALTER TABLE ONLY public.mothers DROP CONSTRAINT mothers_pk;
       public            postgres    false    222    222                       2606    25368    pat_state pat_state_pk 
   CONSTRAINT     V   ALTER TABLE ONLY public.pat_state
    ADD CONSTRAINT pat_state_pk PRIMARY KEY (p_id);
 @   ALTER TABLE ONLY public.pat_state DROP CONSTRAINT pat_state_pk;
       public            postgres    false    229                       2606    25243    patients patients_p_email_un 
   CONSTRAINT     Z   ALTER TABLE ONLY public.patients
    ADD CONSTRAINT patients_p_email_un UNIQUE (p_email);
 F   ALTER TABLE ONLY public.patients DROP CONSTRAINT patients_p_email_un;
       public            postgres    false    224                       2606    25241    patients patients_pk 
   CONSTRAINT     T   ALTER TABLE ONLY public.patients
    ADD CONSTRAINT patients_pk PRIMARY KEY (p_id);
 >   ALTER TABLE ONLY public.patients DROP CONSTRAINT patients_pk;
       public            postgres    false    224                       2606    25252    reg_addresses reg_addresses_pk 
   CONSTRAINT     f   ALTER TABLE ONLY public.reg_addresses
    ADD CONSTRAINT reg_addresses_pk PRIMARY KEY (p_id, reg_id);
 H   ALTER TABLE ONLY public.reg_addresses DROP CONSTRAINT reg_addresses_pk;
       public            postgres    false    226    226                       2606    25261    res_addresses res_addresses_pk 
   CONSTRAINT     f   ALTER TABLE ONLY public.res_addresses
    ADD CONSTRAINT res_addresses_pk PRIMARY KEY (res_id, p_id);
 H   ALTER TABLE ONLY public.res_addresses DROP CONSTRAINT res_addresses_pk;
       public            postgres    false    228    228            ?
           1259    25116    add_info_idx    INDEX     H   CREATE UNIQUE INDEX add_info_idx ON public.add_info USING btree (p_id);
     DROP INDEX public.add_info_idx;
       public            postgres    false    198            ?
           1259    25175    family__idx    INDEX     E   CREATE UNIQUE INDEX family__idx ON public.family USING btree (p_id);
    DROP INDEX public.family__idx;
       public            postgres    false    210                       1259    25189    fathers__idx    INDEX     I   CREATE UNIQUE INDEX fathers__idx ON public.fathers USING btree (fam_id);
     DROP INDEX public.fathers__idx;
       public            postgres    false    213                       1259    25227    mothers__idx    INDEX     I   CREATE UNIQUE INDEX mothers__idx ON public.mothers USING btree (fam_id);
     DROP INDEX public.mothers__idx;
       public            postgres    false    222                       1259    25236    patients__idx    INDEX     K   CREATE UNIQUE INDEX patients__idx ON public.patients USING btree (iac_id);
 !   DROP INDEX public.patients__idx;
       public            postgres    false    224                       1259    25250    reg_addresses__idx    INDEX     S   CREATE UNIQUE INDEX reg_addresses__idx ON public.reg_addresses USING btree (p_id);
 &   DROP INDEX public.reg_addresses__idx;
       public            postgres    false    226                       1259    25259    res_addresses__idx    INDEX     S   CREATE UNIQUE INDEX res_addresses__idx ON public.res_addresses USING btree (p_id);
 &   DROP INDEX public.res_addresses__idx;
       public            postgres    false    228            3           2620    25388    disease_factors next_df    TRIGGER     w   CREATE TRIGGER next_df BEFORE DELETE ON public.disease_factors FOR EACH ROW EXECUTE PROCEDURE public.insert_next_df();
 0   DROP TRIGGER next_df ON public.disease_factors;
       public          postgres    false    206    232                       2606    25262    add_info add_info_patients_fk    FK CONSTRAINT     ~   ALTER TABLE ONLY public.add_info
    ADD CONSTRAINT add_info_patients_fk FOREIGN KEY (p_id) REFERENCES public.patients(p_id);
 G   ALTER TABLE ONLY public.add_info DROP CONSTRAINT add_info_patients_fk;
       public          postgres    false    198    224    2837                       2606    25267    current_diagnosis cd_ilar_fk    FK CONSTRAINT     ?   ALTER TABLE ONLY public.current_diagnosis
    ADD CONSTRAINT cd_ilar_fk FOREIGN KEY (ilar_id) REFERENCES public.diagn_ilar(ilar_id);
 F   ALTER TABLE ONLY public.current_diagnosis DROP CONSTRAINT cd_ilar_fk;
       public          postgres    false    201    2803    199                        2606    25272    current_diagnosis cd_mkb_fk    FK CONSTRAINT     ?   ALTER TABLE ONLY public.current_diagnosis
    ADD CONSTRAINT cd_mkb_fk FOREIGN KEY (mkb_id) REFERENCES public.diagn_mkb(mkb_id);
 E   ALTER TABLE ONLY public.current_diagnosis DROP CONSTRAINT cd_mkb_fk;
       public          postgres    false    199    2805    203            !           2606    25277     current_diagnosis cd_patients_fk    FK CONSTRAINT     ?   ALTER TABLE ONLY public.current_diagnosis
    ADD CONSTRAINT cd_patients_fk FOREIGN KEY (p_id) REFERENCES public.patients(p_id);
 J   ALTER TABLE ONLY public.current_diagnosis DROP CONSTRAINT cd_patients_fk;
       public          postgres    false    2837    224    199            "           2606    25282    diagnosis_by_debut dbd_ilar_fk    FK CONSTRAINT     ?   ALTER TABLE ONLY public.diagnosis_by_debut
    ADD CONSTRAINT dbd_ilar_fk FOREIGN KEY (ilar_id) REFERENCES public.diagn_ilar(ilar_id);
 H   ALTER TABLE ONLY public.diagnosis_by_debut DROP CONSTRAINT dbd_ilar_fk;
       public          postgres    false    2803    201    204            #           2606    25287    diagnosis_by_debut dbd_mkb_fk    FK CONSTRAINT     ?   ALTER TABLE ONLY public.diagnosis_by_debut
    ADD CONSTRAINT dbd_mkb_fk FOREIGN KEY (mkb_id) REFERENCES public.diagn_mkb(mkb_id);
 G   ALTER TABLE ONLY public.diagnosis_by_debut DROP CONSTRAINT dbd_mkb_fk;
       public          postgres    false    2805    203    204            $           2606    25292 "   diagnosis_by_debut dbd_patients_fk    FK CONSTRAINT     ?   ALTER TABLE ONLY public.diagnosis_by_debut
    ADD CONSTRAINT dbd_patients_fk FOREIGN KEY (p_id) REFERENCES public.patients(p_id);
 L   ALTER TABLE ONLY public.diagnosis_by_debut DROP CONSTRAINT dbd_patients_fk;
       public          postgres    false    2837    224    204            &           2606    25297    disease_factors df_patients_fk    FK CONSTRAINT        ALTER TABLE ONLY public.disease_factors
    ADD CONSTRAINT df_patients_fk FOREIGN KEY (p_id) REFERENCES public.patients(p_id);
 H   ALTER TABLE ONLY public.disease_factors DROP CONSTRAINT df_patients_fk;
       public          postgres    false    2837    206    224            %           2606    25302 /   disability_status disability_status_patients_fk    FK CONSTRAINT     ?   ALTER TABLE ONLY public.disability_status
    ADD CONSTRAINT disability_status_patients_fk FOREIGN KEY (p_id) REFERENCES public.patients(p_id);
 Y   ALTER TABLE ONLY public.disability_status DROP CONSTRAINT disability_status_patients_fk;
       public          postgres    false    205    2837    224            (           2606    25307 %   family_anamnesis fam_anam_patients_fk    FK CONSTRAINT     ?   ALTER TABLE ONLY public.family_anamnesis
    ADD CONSTRAINT fam_anam_patients_fk FOREIGN KEY (p_id) REFERENCES public.patients(p_id);
 O   ALTER TABLE ONLY public.family_anamnesis DROP CONSTRAINT fam_anam_patients_fk;
       public          postgres    false    224    211    2837            '           2606    25312    family family_patients_fk    FK CONSTRAINT     z   ALTER TABLE ONLY public.family
    ADD CONSTRAINT family_patients_fk FOREIGN KEY (p_id) REFERENCES public.patients(p_id);
 C   ALTER TABLE ONLY public.family DROP CONSTRAINT family_patients_fk;
       public          postgres    false    2837    210    224            )           2606    25317    fathers fathers_family_fk    FK CONSTRAINT     |   ALTER TABLE ONLY public.fathers
    ADD CONSTRAINT fathers_family_fk FOREIGN KEY (fam_id) REFERENCES public.family(fam_id);
 C   ALTER TABLE ONLY public.fathers DROP CONSTRAINT fathers_family_fk;
       public          postgres    false    213    2816    210            *           2606    25322 %   info_disease info_disease_patients_fk    FK CONSTRAINT     ?   ALTER TABLE ONLY public.info_disease
    ADD CONSTRAINT info_disease_patients_fk FOREIGN KEY (p_id) REFERENCES public.patients(p_id);
 O   ALTER TABLE ONLY public.info_disease DROP CONSTRAINT info_disease_patients_fk;
       public          postgres    false    220    2837    224            +           2606    25327    mothers mothers_family_fk    FK CONSTRAINT     |   ALTER TABLE ONLY public.mothers
    ADD CONSTRAINT mothers_family_fk FOREIGN KEY (fam_id) REFERENCES public.family(fam_id);
 C   ALTER TABLE ONLY public.mothers DROP CONSTRAINT mothers_family_fk;
       public          postgres    false    2816    222    210            2           2606    25369    pat_state pat_state_patients_fk    FK CONSTRAINT     ?   ALTER TABLE ONLY public.pat_state
    ADD CONSTRAINT pat_state_patients_fk FOREIGN KEY (p_id) REFERENCES public.patients(p_id);
 I   ALTER TABLE ONLY public.pat_state DROP CONSTRAINT pat_state_patients_fk;
       public          postgres    false    224    229    2837            ,           2606    25332    patients patients_doctors_fk    FK CONSTRAINT     ?   ALTER TABLE ONLY public.patients
    ADD CONSTRAINT patients_doctors_fk FOREIGN KEY (doc_id) REFERENCES public.doctors(doc_id);
 F   ALTER TABLE ONLY public.patients DROP CONSTRAINT patients_doctors_fk;
       public          postgres    false    2813    224    208            -           2606    25337 $   patients patients_federal_centers_fk    FK CONSTRAINT     ?   ALTER TABLE ONLY public.patients
    ADD CONSTRAINT patients_federal_centers_fk FOREIGN KEY (fc_id) REFERENCES public.federal_centers(fc_id);
 N   ALTER TABLE ONLY public.patients DROP CONSTRAINT patients_federal_centers_fk;
       public          postgres    false    224    215    2823            .           2606    25342    patients patients_hospital_fk    FK CONSTRAINT     ?   ALTER TABLE ONLY public.patients
    ADD CONSTRAINT patients_hospital_fk FOREIGN KEY (hos_id) REFERENCES public.hospital(hos_id);
 G   ALTER TABLE ONLY public.patients DROP CONSTRAINT patients_hospital_fk;
       public          postgres    false    2825    224    217            /           2606    25347 &   patients patients_inf_about_consent_fk    FK CONSTRAINT     ?   ALTER TABLE ONLY public.patients
    ADD CONSTRAINT patients_inf_about_consent_fk FOREIGN KEY (iac_id) REFERENCES public.inf_about_consent(iac_id);
 P   ALTER TABLE ONLY public.patients DROP CONSTRAINT patients_inf_about_consent_fk;
       public          postgres    false    219    224    2827            0           2606    25352 '   reg_addresses reg_addresses_patients_fk    FK CONSTRAINT     ?   ALTER TABLE ONLY public.reg_addresses
    ADD CONSTRAINT reg_addresses_patients_fk FOREIGN KEY (p_id) REFERENCES public.patients(p_id);
 Q   ALTER TABLE ONLY public.reg_addresses DROP CONSTRAINT reg_addresses_patients_fk;
       public          postgres    false    2837    224    226            1           2606    25357 '   res_addresses res_addresses_patients_fk    FK CONSTRAINT     ?   ALTER TABLE ONLY public.res_addresses
    ADD CONSTRAINT res_addresses_patients_fk FOREIGN KEY (p_id) REFERENCES public.patients(p_id);
 Q   ALTER TABLE ONLY public.res_addresses DROP CONSTRAINT res_addresses_patients_fk;
       public          postgres    false    224    228    2837            ?   ?   x????m1?k?Z@)R??%??A?t?d?+b?`$?
?
?$<?R???+|?c??-?&???????l?G??k??????l?-`%	?ϯ??H?8?i???`?X?fQ?????.v?_?ڧm?e?Xb??hh<u?? ?I ?A ;c?A?9?S(???N?ڥ???????TA??K?r?%?ۙ????s$?i????$@??Փ?4?5)Z53!k????aY?
???      ?   D   x?U???0C?s??Q???0??-B*߷?p??̬?B??'S??-?zY?`?i1;՟???v?      ?   X   x?3??0??֋/???x?	H6\l??w??;.l r?.6 ????8/,???zaυ???UM2?]?$?U6?3/F??? ?P?      ?   ?   x?m???@?ϻUlDЃ?X?	jL?;0$.??0ӑ?p??i^??f????19<Ѱd? ?,?=K??c??c?	3/h??й?X?l?J^?Vq0?#ܫ??u9??j=?? 1k?fe?Y?Ry?=0??-?????????X7?Sb????<      ?   E   x?U???@?s܋Q?d?b??>BZ?=?4
?SefMv?ag?j?FE????e???b?????p?      ?   w   x?3??0?b˅}.칰???{/l??0??V???;.??]??yaP~+P??b??~N##C]]c.cbM?ҭpa˅@???/?B?1?52?2??S]#.Cj?Ƅ+F??? ???      ?   @   x?3?4@@.#0}aY?\X?`d?paÅ?.l???b???????zP??P}?`^? ?       ?      x?%?11??1H?wxGCBGC?NQN
	?7???T???z??,D??؈,??????C/?T??2:?C??g?.???H??n5???|Id~???c?c?x?:'????B3????/??/?UN;?Ycp?      ?   J   x?3?4??0?¾?/콰?b????????@Ɩ;.6]?za??~MN.#N#?s?؄ӄx?1z\\\ ?nj?      ?      x?3?4?@.#8?LY&P1C?=... wi-      ?   ?   x?u?An?0E??SpWq?pE+?X???r? ??$W?s?~Pٰ????~??'???*?0?(آcQ?e???7?%?l??Ͳ?F3?r?/?Z?n?gb?fyEEp?????#?S?`d??摍RO9?9{????O\$6?G??ݳ?iY?D???ѣ????:R3??O??	UK?;?"??	???wK?_?
)?Ĥ?wǿ??a]P?????q̍ޭ????1??$?v      ?   ,   x?3?4????#.#??2?2?B\&? !.Sdݜ?\1z\\\ ???      ?   s   x?3??0??.L估YO??.6^l????&?{/l?J?b3???%&U.V0430"?ܘ?x"P1???v_? VTr?_??.?? g??A?Q?4CB???qqq ;?g?      ?   1   x?3?4?4202?50?52?2?s?u??L88c???s??qqq n	      ?   B   x?-??? ???.??@K?K??B??t???I?^xS?E??B?	g??,
??c?+{W??_? ~mJ      ?   ?   x?u?Qn?0D?ק?Fq?ܥ?	??@HHTU?	"Wߨ??T????웱?8??P??-??[T??0??r??7'?B?.?6??$|qv?6???P?3?b?X6$?<??o?&
V??MQ?q??=$7??/x??Se??y??ډV?0?l?ZR?S?Oj.?(?tO?x?k?_g??I*X*??)?#|???3?"?
????????_z???&?׏??[??;???      ?   =   x?3?4?4R??x??????x??N=C.#??!6	c?:??L8?:RRӋRS?1z\\\ K?'0      ?   8  x???=N?@??ٻ8????qN??B)Ҹ?w?D$??!0!F?8?fo?۵B?h?X??f?}?F?s?{?]?wȎ?+nQ??????"^#ݲ?y??-Iű?b)??/<?4??_???Ci}??߆???Pg&79?.I!$)?????:??Һ?7n?~b??]0?H?Ql?*?=C?????Lp?%E?H??<R???????ܺ???-z????w?????X???&??:?f??&%"???#NFW?V???~?5?p:?????m	۸v????(=Le???????W??bR???r<Lo?Z	I1!??
      ?      x?KL????L?\1z\\\ 4?      ?   ?   x???Mn?0???)| ?ꄊ??0!HUH?`m???(j????I?n*eae?yyߛIMj?A???8??:^???dC&?????Q!??d]???W
?p?E??Â?\&t?8"???,lf?n???5??P?8;ə?????ŉȓ??U?k-??&֚]?k?:ó?}?9?E?og]FM#????|??1??[????b?+?o????V$?U+??u?;?yc???q?f?޾$??7?9?      ?   ?   x???Mn?0???)| ?ꄊ??0!HUH?`m???(j????I?n*eae?yyߛIMj?A???8??:^???dC&?????Q!??d]???W
?p?E??Â?\&t?8"???,lf?n???5??P?8;ə?????ŉȓ??U?k-??&֚]?k?:ó?}?9?E?og]FM#????|??1??[????b?+?o????V$?U+??u?;?yc???q?f?޾$??7?9?     