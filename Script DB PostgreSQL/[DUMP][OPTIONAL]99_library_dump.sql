--
-- PostgreSQL database dump
--

-- Dumped from database version 13.11
-- Dumped by pg_dump version 13.11

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: get_detail_borrower(bigint); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.get_detail_borrower(p_borrower_id bigint) RETURNS TABLE(id bigint, name character varying, nim character varying, phone_number character varying, address text, faculty character varying, major character varying, borrow_date date, return_date date, penalty_amount numeric, created_date timestamp without time zone, updated_date timestamp without time zone, title character varying, writer character varying, isbn character varying, publisher character varying, page integer, synopsis text, dimension character varying, language character varying, date_issue date, image_path text)
    LANGUAGE plpgsql
    AS $$
BEGIN
	
	RETURN query 
	SELECT 
		a.br_id, a.br_name, a.br_nim, a.br_phone_number, a.br_address, a.br_faculty, a.br_major, a.br_borrow_date, a.br_return_date,
		a.br_penalty_amount, a.br_created_date,	a.br_updated_date, c.bk_title, c.bk_writer, c.bk_isbn, c.bk_publisher, c.bk_page, c.bk_synopsis, c.bk_dimension, c.bk_language,
		c.bk_date_issue::date, c.bk_image_path
	FROM
		borrower a
	LEFT JOIN 
		borrower_book_transaction b ON a.br_id = b.brt_br_id
	LEFT JOIN
		books c ON b.brt_bk_id = c.bk_id
	WHERE 
		a.br_id = p_borrower_id;

END
$$;


ALTER FUNCTION public.get_detail_borrower(p_borrower_id bigint) OWNER TO postgres;

--
-- Name: get_list_data_books(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.get_list_data_books() RETURNS TABLE(id bigint, title character varying, writer character varying, isbn character varying, publisher character varying, page integer, synopsis text, dimension character varying, language character varying, date_issue date, quantity integer, image_path text, created_date timestamp without time zone)
    LANGUAGE plpgsql
    AS $$
BEGIN
	
	RETURN query 
	SELECT a.bk_id, a.bk_title, a.bk_writer, a.bk_isbn, a.bk_publisher, a.bk_page, a.bk_synopsis, a.bk_dimension, a.bk_language,
				a.bk_date_issue::date, a.bk_quantity, a.bk_image_path, a.bk_created_date
	FROM
		books a;

END
$$;


ALTER FUNCTION public.get_list_data_books() OWNER TO postgres;

--
-- Name: get_list_data_borrower(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.get_list_data_borrower() RETURNS TABLE(id bigint, name character varying, nim character varying, phone_number character varying, address text, faculty character varying, major character varying, borrow_date date, return_date date, penalty_amount numeric, created_date timestamp without time zone, updated_date timestamp without time zone)
    LANGUAGE plpgsql
    AS $$
BEGIN
	
	RETURN query 
	SELECT 
		a.br_id,
		a.br_name,
		a.br_nim,
		a.br_phone_number,
		a.br_address,
		a.br_faculty, 
		a.br_major,
		a.br_borrow_date,
		a.br_return_date,
		a.br_penalty_amount,
		a.br_created_date,
		a.br_updated_date
	FROM
		borrower a;

END
$$;


ALTER FUNCTION public.get_list_data_borrower() OWNER TO postgres;

--
-- Name: insert_books_data(bigint, character varying, character varying, character varying, character varying, integer, text, character varying, character varying, character varying, integer, text, character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.insert_books_data(p_id bigint, p_title character varying, p_writer character varying, p_isbn character varying, p_publisher character varying, p_page integer, p_synopsis text, p_dimension character varying, p_language character varying, p_date_issue character varying, p_quantity integer, p_image_path text, p_flag character varying) RETURNS void
    LANGUAGE plpgsql
    AS $$
DECLARE
	
BEGIN
	
	IF p_flag = 'INSERT' THEN
	
		INSERT INTO books (
				bk_title, bk_writer, bk_isbn, bk_publisher, bk_page, bk_synopsis, bk_dimension, bk_language, bk_date_issue, bk_quantity,
				bk_image_path, bk_created_date
		)
		VALUES (
				p_title, p_writer, p_isbn, p_publisher, p_page, p_synopsis, p_dimension, p_language, p_date_issue::date, p_quantity,
				p_image_path, CURRENT_TIMESTAMP
		);
		
	ELSIF p_flag = 'UPDATE' THEN
	
		UPDATE books SET
			bk_title = p_title,
			bk_writer = p_writer,
			bk_isbn = p_isbn,
			bk_publisher = p_publisher,
			bk_page = p_page,
			bk_synopsis = p_synopsis,
			bk_dimension = p_dimension,
			bk_language = p_language,
			bk_date_issue = p_date_issue::date,
			bk_quantity = p_quantity,
			bk_image_path = p_image_path
		WHERE
			bk_id = p_id;
	
	ELSE 
	
		DELETE FROM books WHERE bk_id = p_id;
	
	END IF;
	
	END
$$;


ALTER FUNCTION public.insert_books_data(p_id bigint, p_title character varying, p_writer character varying, p_isbn character varying, p_publisher character varying, p_page integer, p_synopsis text, p_dimension character varying, p_language character varying, p_date_issue character varying, p_quantity integer, p_image_path text, p_flag character varying) OWNER TO postgres;

--
-- Name: insert_borrower_book_trx(bigint, bigint, character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.insert_borrower_book_trx(p_br_id bigint, p_bk_id bigint, p_flag character varying) RETURNS void
    LANGUAGE plpgsql
    AS $$
DECLARE
	
BEGIN
	
	IF p_flag = 'INSERT' THEN
	
		INSERT INTO borrower_book_transaction (
				brt_br_id, brt_bk_id, brt_created_date
		)
		VALUES (
				p_br_id, p_bk_id, CURRENT_TIMESTAMP
		);
	
	ELSE 
	
		DELETE FROM borrower_book_transaction WHERE brt_br_id = p_br_id;
			
	END IF;
	
	END
$$;


ALTER FUNCTION public.insert_borrower_book_trx(p_br_id bigint, p_bk_id bigint, p_flag character varying) OWNER TO postgres;

--
-- Name: insert_borrower_data(bigint, character varying, character varying, character varying, text, character varying, character varying, character varying, character varying, numeric, character varying); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.insert_borrower_data(p_id bigint, p_name character varying, p_nim character varying, p_phone_number character varying, p_address text, p_faculty character varying, p_major character varying, p_borrow_date character varying, p_return_date character varying, p_penalty_amount numeric, p_flag character varying) RETURNS TABLE(br_id bigint)
    LANGUAGE plpgsql
    AS $$
DECLARE

borrower_id bigint;
	
BEGIN
	
	IF p_flag = 'INSERT' THEN
	
		INSERT INTO borrower as a(
				br_name, br_nim, br_phone_number, br_address, br_faculty, br_major, br_borrow_date, br_return_date, br_penalty_amount, 
				br_created_date
		)
		VALUES (
				p_name, p_nim, p_phone_number, p_address, p_faculty, p_major, p_borrow_date::date, p_return_date::date, p_penalty_amount,
				CURRENT_TIMESTAMP
		)
		RETURNING a.br_id INTO borrower_id;
		
		RETURN QUERY
		SELECT borrower_id;
		
		
	ELSIF p_flag = 'UPDATE' THEN
	
		UPDATE borrower AS a SET
				br_name = p_name,
				br_nim = p_nim,
				br_phone_number = p_phone_number,
				br_address = p_address,
				br_faculty = p_faculty,
				br_major = p_major,
				br_borrow_date = p_borrow_date::date,
				br_return_date = p_return_date::date,
				br_penalty_amount = p_penalty_amount,
				br_updated_date = CURRENT_TIMESTAMP
		WHERE
				a.br_id = p_id;
				
		RETURN QUERY
		SELECT 0::bigint;
	
	ELSE 
	
	  DELETE FROM borrower a WHERE a.br_id = p_id;
		
		RETURN QUERY
		SELECT -1::bigint;
			
	END IF;
	
	END
$$;


ALTER FUNCTION public.insert_borrower_data(p_id bigint, p_name character varying, p_nim character varying, p_phone_number character varying, p_address text, p_faculty character varying, p_major character varying, p_borrow_date character varying, p_return_date character varying, p_penalty_amount numeric, p_flag character varying) OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: books; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.books (
    bk_id bigint NOT NULL,
    bk_title character varying(255),
    bk_writer character varying(255),
    bk_isbn character varying(255),
    bk_publisher character varying(255),
    bk_page integer,
    bk_synopsis text,
    bk_dimension character varying(255),
    bk_language character varying(50),
    bk_date_issue date,
    bk_quantity integer,
    bk_image_path text,
    bk_created_date timestamp(6) without time zone
);


ALTER TABLE public.books OWNER TO postgres;

--
-- Name: books_bk_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.books_bk_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.books_bk_id_seq OWNER TO postgres;

--
-- Name: books_bk_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.books_bk_id_seq OWNED BY public.books.bk_id;


--
-- Name: borrower; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.borrower (
    br_id bigint NOT NULL,
    br_name character varying(255),
    br_nim character varying(20),
    br_phone_number character varying(15),
    br_address text,
    br_faculty character varying(50),
    br_major character varying(50),
    br_borrow_date date,
    br_return_date date,
    br_penalty_amount numeric(18,0),
    br_created_date timestamp without time zone,
    br_updated_date timestamp without time zone
);


ALTER TABLE public.borrower OWNER TO postgres;

--
-- Name: borrower_book_transaction; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.borrower_book_transaction (
    brt_id bigint NOT NULL,
    brt_br_id bigint,
    brt_bk_id bigint,
    brt_created_date timestamp without time zone
);


ALTER TABLE public.borrower_book_transaction OWNER TO postgres;

--
-- Name: borrower_book_transaction_brt_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.borrower_book_transaction_brt_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.borrower_book_transaction_brt_id_seq OWNER TO postgres;

--
-- Name: borrower_book_transaction_brt_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.borrower_book_transaction_brt_id_seq OWNED BY public.borrower_book_transaction.brt_id;


--
-- Name: borrower_br_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.borrower_br_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.borrower_br_id_seq OWNER TO postgres;

--
-- Name: borrower_br_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.borrower_br_id_seq OWNED BY public.borrower.br_id;


--
-- Name: books bk_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.books ALTER COLUMN bk_id SET DEFAULT nextval('public.books_bk_id_seq'::regclass);


--
-- Name: borrower br_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.borrower ALTER COLUMN br_id SET DEFAULT nextval('public.borrower_br_id_seq'::regclass);


--
-- Name: borrower_book_transaction brt_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.borrower_book_transaction ALTER COLUMN brt_id SET DEFAULT nextval('public.borrower_book_transaction_brt_id_seq'::regclass);


--
-- Data for Name: books; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.books (bk_id, bk_title, bk_writer, bk_isbn, bk_publisher, bk_page, bk_synopsis, bk_dimension, bk_language, bk_date_issue, bk_quantity, bk_image_path, bk_created_date) FROM stdin;
7	Great at Work	Morten T. Honsen	9786020000000	Gramedia Widiasarana Indonesia	378	Data yang dipaparkan dalam buku ini adalah hasil dari riset corporate dan individual performance (evidence–based)selama kurang lebih 5 tahun terhadap kurang lebih 5,000 menejer dan karyawan yang mewakili 15 sektor industri dan 22 jabatan.\r\n\r\nMengapa sebagian orang memiliki performa yang lebih baik ketimbang rekan kerja lainnya? Pertanyaan sederhana yang terdengar iseng ini terus menghantui para profesional di setiap sektor pekerjaan Kini, setelah melakukan riset selama 5 tahun terhadap 5000 manajer dan pegawai, Morten T. Hansen mendapatkan jawabannya. Risetnya yang mendalam telah memunculkan Tujuh Praktik Kerja Cerdas yang dapat diaplikasikan oleh setiap orang yang berhasrat untuk memaksimalkan waktu dan performanya.\r\n\r\nPemikirannya dikuatkan kisah-kisah nyata yang membuka mata--dari seorang pebisnis wanita yang melejitkan perusahaannya justru dengan berkata "tidak" pada pelanggan hingga seorang kepala sekolah yang dengan cerdas menyelamatkan sekolahnya yang dianggap gagal, juga seorang koki sushi tersohor di sebuah warung kecil yang sukses memenangkan 3 Michelin Stars.	15 cm x 23 cm	Indonesia	2022-05-12	10	webapps/image/good_at_work.jpg	2023-08-14 09:38:29.066466
8	Melangkah	J. S Khairen	9786020523316	Gramedia Widiasarana Indonesia	368	Listrik padam di seluruh Jawa dan Bali secara misterius. Ancaman nyata kekuatan baru yang hendak menaklukkan Nusantara. Saat yang sama, empat sahabat mendarat di Sumba, hanya untuk mendapati nasib ratusan juta manusia ada di tangan mereka! Empat mahasiswa ekonomi ini, harus bertarung melawan pasukan berkuda yang bisa melontarkan listrik! Semua dipersulit oleh seorang buronan tingkat tinggi bertopeng pahlawan yang punya rencana mengerikan. Ternyata pesan arwah nenek moyang itu benar-benar terwujud. “Akan datang kegelapan yang berderap, bersama ribuan kuda raksasa di kala malam. Mereka bangun setelah sekian lama, untuk menghancurkan Nusantara. Seorang lelaki dan seorang perempuan ditakdirkan membaurkan air di lautan dan api di pegunungan. Menyatukan tanah yang menghujam, dan udara yang terhampar.”Kisah tentang persahabatan, tentang jurang ego anak dan orangtua, tentang menyeimbangkan logika dan perasaan. Juga tentang melangkah menuju masa depan. Bahwa, apa pun yang menjadi luka masa lalu, biarlah mengering bersama waktu.	13,5 cm x 20 cm	Indonesia	2020-03-22	17	webapps/image/melangkah.jpg	2023-08-14 09:38:29.066466
\.


--
-- Data for Name: borrower; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.borrower (br_id, br_name, br_nim, br_phone_number, br_address, br_faculty, br_major, br_borrow_date, br_return_date, br_penalty_amount, br_created_date, br_updated_date) FROM stdin;
\.


--
-- Data for Name: borrower_book_transaction; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.borrower_book_transaction (brt_id, brt_br_id, brt_bk_id, brt_created_date) FROM stdin;
10	3	7	2023-08-14 09:42:02.109818
11	3	8	2023-08-14 09:42:02.109818
\.


--
-- Name: books_bk_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.books_bk_id_seq', 8, true);


--
-- Name: borrower_book_transaction_brt_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.borrower_book_transaction_brt_id_seq', 11, true);


--
-- Name: borrower_br_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.borrower_br_id_seq', 4, true);


--
-- Name: books books_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.books
    ADD CONSTRAINT books_pkey PRIMARY KEY (bk_id);


--
-- Name: borrower_book_transaction borrower_book_transaction_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.borrower_book_transaction
    ADD CONSTRAINT borrower_book_transaction_pkey PRIMARY KEY (brt_id);


--
-- Name: borrower borrower_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.borrower
    ADD CONSTRAINT borrower_pkey PRIMARY KEY (br_id);


--
-- PostgreSQL database dump complete
--

