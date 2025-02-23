PGDMP  .    	                |            ShopDataBase    16.0    16.0     �           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            �           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            �           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            �           1262    32770    ShopDataBase    DATABASE     �   CREATE DATABASE "ShopDataBase" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Russian_Russia.1251';
    DROP DATABASE "ShopDataBase";
                postgres    false            �            1259    32791 
   categories    TABLE     e   CREATE TABLE public.categories (
    "Id" integer NOT NULL,
    "Name" character varying NOT NULL
);
    DROP TABLE public.categories;
       public         heap    postgres    false            �            1259    32857    categories_Id_seq    SEQUENCE     �   ALTER TABLE public.categories ALTER COLUMN "Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."categories_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    218            �            1259    32822    orders_t    TABLE     �   CREATE TABLE public.orders_t (
    "Id" integer NOT NULL,
    "UserID" integer NOT NULL,
    "ProductKeyID" integer NOT NULL,
    "DatePurchase" date,
    "Count" integer
);
    DROP TABLE public.orders_t;
       public         heap    postgres    false            �            1259    40967    orders_t_Id_seq    SEQUENCE     �   ALTER TABLE public.orders_t ALTER COLUMN "Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."orders_t_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    219            �            1259    32786 
   products_t    TABLE     <  CREATE TABLE public.products_t (
    "Id" integer NOT NULL,
    "Name" character varying(45) NOT NULL,
    "Description" character varying(255),
    "DateAdded" date NOT NULL,
    "Image" character varying(255),
    "Price" double precision NOT NULL,
    "CategoryID" integer NOT NULL,
    "CountInStock" integer
);
    DROP TABLE public.products_t;
       public         heap    postgres    false            �            1259    32853    products_t_Id_seq    SEQUENCE     �   ALTER TABLE public.products_t ALTER COLUMN "Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."products_t_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    217            �            1259    32776    roles_t    TABLE     j   CREATE TABLE public.roles_t (
    "Id" integer NOT NULL,
    "NameRole" character varying(45) NOT NULL
);
    DROP TABLE public.roles_t;
       public         heap    postgres    false            �            1259    32771    users_t    TABLE       CREATE TABLE public.users_t (
    "Id" integer NOT NULL,
    "NickName" character varying(20) NOT NULL,
    "Login" character varying(20) NOT NULL,
    "Password" character varying(255) NOT NULL,
    "RoleID" integer NOT NULL,
    "Salt" character varying(255)
);
    DROP TABLE public.users_t;
       public         heap    postgres    false            �            1259    32852    users_t_Id_seq    SEQUENCE     �   ALTER TABLE public.users_t ALTER COLUMN "Id" ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public."users_t_Id_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    215            �          0    32791 
   categories 
   TABLE DATA           2   COPY public.categories ("Id", "Name") FROM stdin;
    public          postgres    false    218   �#       �          0    32822    orders_t 
   TABLE DATA           [   COPY public.orders_t ("Id", "UserID", "ProductKeyID", "DatePurchase", "Count") FROM stdin;
    public          postgres    false    219   �#       �          0    32786 
   products_t 
   TABLE DATA           ~   COPY public.products_t ("Id", "Name", "Description", "DateAdded", "Image", "Price", "CategoryID", "CountInStock") FROM stdin;
    public          postgres    false    217   6$       �          0    32776    roles_t 
   TABLE DATA           3   COPY public.roles_t ("Id", "NameRole") FROM stdin;
    public          postgres    false    216   �$       �          0    32771    users_t 
   TABLE DATA           Z   COPY public.users_t ("Id", "NickName", "Login", "Password", "RoleID", "Salt") FROM stdin;
    public          postgres    false    215   K%       �           0    0    categories_Id_seq    SEQUENCE SET     A   SELECT pg_catalog.setval('public."categories_Id_seq"', 1, true);
          public          postgres    false    222            �           0    0    orders_t_Id_seq    SEQUENCE SET     @   SELECT pg_catalog.setval('public."orders_t_Id_seq"', 16, true);
          public          postgres    false    223            �           0    0    products_t_Id_seq    SEQUENCE SET     A   SELECT pg_catalog.setval('public."products_t_Id_seq"', 7, true);
          public          postgres    false    221            �           0    0    users_t_Id_seq    SEQUENCE SET     ?   SELECT pg_catalog.setval('public."users_t_Id_seq"', 31, true);
          public          postgres    false    220                       2606    32780    roles_t Roles_t_pkey 
   CONSTRAINT     V   ALTER TABLE ONLY public.roles_t
    ADD CONSTRAINT "Roles_t_pkey" PRIMARY KEY ("Id");
 @   ALTER TABLE ONLY public.roles_t DROP CONSTRAINT "Roles_t_pkey";
       public            postgres    false    216                       2606    32797    categories categories_pkey 
   CONSTRAINT     Z   ALTER TABLE ONLY public.categories
    ADD CONSTRAINT categories_pkey PRIMARY KEY ("Id");
 D   ALTER TABLE ONLY public.categories DROP CONSTRAINT categories_pkey;
       public            postgres    false    218                       2606    32826    orders_t orders_t_pkey 
   CONSTRAINT     V   ALTER TABLE ONLY public.orders_t
    ADD CONSTRAINT orders_t_pkey PRIMARY KEY ("Id");
 @   ALTER TABLE ONLY public.orders_t DROP CONSTRAINT orders_t_pkey;
       public            postgres    false    219                       2606    32799    products_t products_t_pkey 
   CONSTRAINT     Z   ALTER TABLE ONLY public.products_t
    ADD CONSTRAINT products_t_pkey PRIMARY KEY ("Id");
 D   ALTER TABLE ONLY public.products_t DROP CONSTRAINT products_t_pkey;
       public            postgres    false    217                       2606    32775    users_t users_t_pkey 
   CONSTRAINT     T   ALTER TABLE ONLY public.users_t
    ADD CONSTRAINT users_t_pkey PRIMARY KEY ("Id");
 >   ALTER TABLE ONLY public.users_t DROP CONSTRAINT users_t_pkey;
       public            postgres    false    215                       2606    40962    orders_t Order/Product/Id_FK    FK CONSTRAINT     �   ALTER TABLE ONLY public.orders_t
    ADD CONSTRAINT "Order/Product/Id_FK" FOREIGN KEY ("ProductKeyID") REFERENCES public.products_t("Id") NOT VALID;
 H   ALTER TABLE ONLY public.orders_t DROP CONSTRAINT "Order/Product/Id_FK";
       public          postgres    false    219    217    4628                       2606    32842    orders_t Order/User/ID_FK    FK CONSTRAINT     �   ALTER TABLE ONLY public.orders_t
    ADD CONSTRAINT "Order/User/ID_FK" FOREIGN KEY ("UserID") REFERENCES public.users_t("Id") NOT VALID;
 E   ALTER TABLE ONLY public.orders_t DROP CONSTRAINT "Order/User/ID_FK";
       public          postgres    false    219    4624    215                       2606    32800 !   products_t Product/Category/ID_FK    FK CONSTRAINT     �   ALTER TABLE ONLY public.products_t
    ADD CONSTRAINT "Product/Category/ID_FK" FOREIGN KEY ("CategoryID") REFERENCES public.categories("Id") NOT VALID;
 M   ALTER TABLE ONLY public.products_t DROP CONSTRAINT "Product/Category/ID_FK";
       public          postgres    false    217    4630    218                       2606    32781    users_t Users/Roles/ID_FK    FK CONSTRAINT     �   ALTER TABLE ONLY public.users_t
    ADD CONSTRAINT "Users/Roles/ID_FK" FOREIGN KEY ("RoleID") REFERENCES public.roles_t("Id") NOT VALID;
 E   ALTER TABLE ONLY public.users_t DROP CONSTRAINT "Users/Roles/ID_FK";
       public          postgres    false    4626    215    216            �      x�3�0�¾���/l���+F��� t	�      �   ?   x�m���0���KP�&�t�9�/�{�J8]�*���p��yRZZ_��ݗ�����H^�0      �   �   x�3漰���^�}aׅ���.6�8�9��Lt�u�8�#��C2�S2�3R���
�9���89M��L(2��D��h���9�.�9{/�(\�|a���/�#�PS��3�0u2.�L�e	1����+F��� N���      �   a   x�u��@@D��U�	�Q���A��M��Zx_G6��2��d����7NkN�$7��\lx6��V��6��c�Y_�0���^[�$�)Sy zO8      �   K  x���;s�@F���a��+,E�%р�I���R��gb46�X�[�3g��4�!�1��8=a0!��`�J��wn%��RY�!��n��"�A6�#��7��J2%A�6Y�:�,��7s��;>Vg�!9�k91V�1�~Jm��͖�Y��܉w��S^/t��_m74mXܚӻD��Mrs�J�J���o�.mh{�P;B���n��������:$���n`Z�ښA'�x3+���/�5:R�����`D�#R���!�=�|�C2�%��w���r^MNbkնǵ�2�%�b0�^U�Lq��\I�9�����A *��     